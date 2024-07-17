using KinoApiCache.CacheProvider;
using KinoApiWrapper;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoDataProvider
{
    public class DataProvider : IDataProvider
    {
        public IGenres Genres { get; }
        public IMovies Movies { get; }

        private KinoApi actualDataProvider;
        private CacheApi cachedDataProvider;

        public DataProvider(string apiKey, string url, string connectionString, int itemsPerPage)
        {
            actualDataProvider = new KinoApi(apiKey, url);

            cachedDataProvider = new CacheApi(actualDataProvider.Genres,
                                              actualDataProvider.Movies,
                                              connectionString,
                                              itemsPerPage);

            Genres = cachedDataProvider.Genres;
            Movies = cachedDataProvider.Movies;
        }
        public void Dispose()
        {
            cachedDataProvider.Dispose();
            actualDataProvider.Dispose();
        }
    }
}
