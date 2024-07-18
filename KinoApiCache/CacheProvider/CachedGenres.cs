using KinoApiCache.DataBase;
using KinoApiCache.DataBase.Interaction;
using KinoApiCache.DataBase.Tables;
using KinoApiCache.Utils;
using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.CacheProvider
{
    internal class CachedGenres : IGenres
    {
        private readonly IGenres genres;
        private readonly CachedCalls interactor;


        public CachedGenres(IGenres genres, CachedCalls interactor)
        {
            this.genres = genres;
            this.interactor = interactor;
        }
        public async Task<Genre[]> GetGenresAsync()
        {
            var cached = await interactor.GetResult<Genre,GenreDB>(nameof(GetGenresAsync));
            if (cached != null)
                return cached;

            var result = await genres.GetGenresAsync();
            if (result == null)
                return Array.Empty<Genre>();
            await interactor.AddCall<Genre, GenreDB>(result, nameof(GetGenresAsync));
            return result;
        }
    }
}
