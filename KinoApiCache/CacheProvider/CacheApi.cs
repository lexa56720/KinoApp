using KinoApiCache.DataBase;
using KinoApiCache.Utils;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.CacheProvider
{
    public class CacheApi : IDataProvider
    {
        public IGenres Genres { get; }

        public IMovies Movies { get; }

        public CacheApi(string connectionString,int itemsPerPage)
        {
            var contextFactory = new DbContextFactory(connectionString);
            var mapper = new Mapper();
            Genres = new CachedGenres(contextFactory,mapper);
            Movies = new CachedMovies(itemsPerPage,contextFactory, mapper);
        }
        public void Dispose()
        {
            return;
        }
    }
}
