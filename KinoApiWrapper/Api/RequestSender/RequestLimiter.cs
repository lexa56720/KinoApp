using System;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api.RequestSender
{
    internal class RequestLimiter
    {
        //Вызов асинхронных функций в паралелльном режиме, но не чаше чем N раз в секунду
        public static async Task<T[]> Call<T>(Func<int, Task<T>>[] funcs, int maxRequestPerSecond)
        {
            var result = new Task<T>[funcs.Length];

            //Если количество функций больше чем ограничение, то вызываем их по частям
            if (funcs.Length >= maxRequestPerSecond)
                return await ExecuteByParts(funcs, maxRequestPerSecond);

            //Иначе в параллельном режиме
            for (int i = 0; i < funcs.Length; i++)
            {
                result[i] = funcs[i](i);
            }
            return await Task.WhenAll(result);
        }

        private static async Task<T[]> ExecuteByParts<T>(Func<int, Task<T>>[] funcs, int maxRequestPerSecond)
        {
            var result = new T[funcs.Length];

            int executed = 0;
            while (executed < funcs.Length)
            {
                //Паралельный вызов части функций, с размером части не более maxRequestPerSecond 
                var tasks = new Task<T>[maxRequestPerSecond];
                var timer = Task.Delay(1000);
                for (int i = 0; i < maxRequestPerSecond && executed + i < funcs.Length; i++)
                {
                    tasks[i] = funcs[executed + i](executed + i);
                }
                //Ожидание завершение вызова функций И прошествия 1 секунды с момента первого вызова
                var execution = Task.WhenAll(tasks);
                await Task.WhenAll(Task.WhenAll(tasks), timer);

                //Сохранение результатов
                for (int i = 0; i < maxRequestPerSecond; i++)
                {
                    result[i + executed] = tasks[i].Result;
                }
                executed += maxRequestPerSecond;
            }
            return result;
        }
    }
}
