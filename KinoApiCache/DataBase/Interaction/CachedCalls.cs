using KinoApiCache.DataBase.Tables;
using KinoApiCache.DataBase.Tables.CachedType;
using KinoApiCache.Utils;
using KinoTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.DataBase.Interaction
{
    internal class CachedCalls
    {
        private readonly DbContextFactory factory;
        private readonly IMapper mapper;
        private TimeSpan cacheLife;

        public CachedCalls(DbContextFactory factory, TimeSpan cacheLife, IMapper mapper)
        {
            this.factory = factory;
            this.cacheLife = cacheLife;
            this.mapper = mapper;
        }
        public void UpdateLifeTime(TimeSpan lifetime)
        {
            cacheLife = lifetime;
        }

        //Массив результатов вызова функции с соответсвующими аргументами
        public async Task<TKino[]> GetResult<TKino, TDb>(string funcName, params string[] args)
            where TKino : class
            where TDb : class, ICachedEntity
        {
            //Получение вызова
            var call = await GetCall(funcName, args);
            if (call == null)
                return null;

            //Проверка свежести кэша
            if (call.Date + cacheLife < DateTime.UtcNow)
            {
                await RemoveCall<TDb>(call);
                return default;
            }

            //Возврат массива результатов кэшированного вызова
            return await GetResultsFromDB<TKino, TDb>(call.Results.OrderBy(r => r.IndexId)
                                                                  .Select(r => r.ValueId));
        }
        private async Task<CallDB> GetCall(string funcName, params string[] args)
        {
            //Получаем id нужной функции
            var funcId = TypeIdHelper.GetFuncId(funcName);
            if (funcId < 0)
                return null;

            using (var db = factory.Create())
            {
                //Получаем все вызовы этой функции
                var calls = await db.Calls.Include(c => c.Arguments)
                                          .Include(c => c.Results)
                                          .Where(c => c.FuncId == funcId)
                                          .ToArrayAsync();

                //Получаем самый свежий вызов с подходящими аргументами
                return calls.OrderBy(c => c.Date)
                            .LastOrDefault(c => c.Arguments.OrderBy(a => a.Index)
                                                           .Select(a => a.Value)
                                                           .SequenceEqual(args));
            }
        }

        //Получаем результаты заданного типа TDb по массиву ids
        private async Task<TKino[]> GetResultsFromDB<TKino, TDb>(IEnumerable<int> ids)
            where TKino : class
            where TDb : class, ICachedEntity
        {
            using (var db = factory.Create())
            {
                var set = db.Set<TDb>(); //Набор результатов нужного типа
                var result = await set.Where(e => ids.Contains(e.Id)).ToArrayAsync();
                if (!result.Any())
                    return Array.Empty<TKino>();

                //Конвертируем подходящие результаты в тип TKino и возвращаем их в виде массива 
                return ids.Select(id => mapper.Map<TKino, TDb>(result.Single(e => e.Id == id)))
                          .ToArray();
            }
        }

        //Удаление вызова вместе со всеми параметрами и результатами
        public async Task<bool> RemoveCall<TDb>(CallDB call) where TDb : class, ICachedEntity
        {
            using (var db = factory.Create())
            {
                call = await db.Calls.Include(c => c.Arguments)
                                     .Include(c => c.Results)
                                     .SingleOrDefaultAsync(c => c.Id == call.Id);


                var set = db.Set<TDb>();
                set.RemoveRange(set.Where(a => call.Results.Select(r => r.ValueId)
                                                           .Contains(a.Id)));
                db.Arguments.RemoveRange(call.Arguments);
                db.Results.RemoveRange(call.Results);
                db.Calls.Remove(call);
                return await db.SaveChangesAsync() > 0;
            }
        }

        //Добавление вызова и его результатов в бд
        public async Task<bool> AddCall<TKino, TDb>(TKino[] result, string funcName, params string[] args)
            where TKino : class
            where TDb : class, ICachedEntity
        {
            //Получаем айди функции
            var funcId = TypeIdHelper.GetFuncId(funcName);
            if (funcId < 0)
                return false;

            //Добавляем результаты в таблицу, которая соответствует их типу
            var resultIds = await AddResults<TKino, TDb>(result);
            using (var db = factory.Create())
            {
                //Добавляем вызов функции в бд
                var call = new CallDB()
                {
                    FuncId = funcId,
                    Date = DateTime.UtcNow,
                };
                await db.Calls.AddAsync(call);

                //Добавляет информацию об аргументах вызова
                for (var i = 0; i < args.Length; i++)
                    call.Arguments.Add(new ArgumentDB()
                    {
                        Index = i,
                        Value = args[i],
                    });
                //Добавляем информацию о результатах вызова
                for (int i = 0; i < result.Length; i++)
                    call.Results.Add(new ResultDB()
                    {
                        ValueId = resultIds[i],
                        IndexId = i,
                    });
                return await db.SaveChangesAsync() > 0;
            }
        }

        //Сохранение результатов в бд
        private async Task<int[]> AddResults<TKino, TDb>(TKino[] result)
            where TKino : class
            where TDb : class, ICachedEntity
        {
            var dbItems = new TDb[result.Length];
            using (var db = factory.Create())
            {
                var set = db.Set<TDb>();//Получаем набор с нужным типом данных
                for (int i = 0; i < result.Length; i++)
                {
                    //Конвертируем объекты из типа TKino в TDb
                    dbItems[i] = mapper.ReverseMap<TKino, TDb>(result[i]);

                    await set.AddAsync(dbItems[i]);
                }
                await db.SaveChangesAsync();
            }
            return dbItems.Select(i => i.Id).ToArray();
        }
    }
}
