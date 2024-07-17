using KinoApiCache.DataBase;
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
        private readonly DbContextFactory factory;
        private readonly IMapper mapper;

        public CachedGenres(IGenres genres,DbContextFactory factory,IMapper mapper)
        {
            this.genres = genres;
            this.factory = factory;
            this.mapper = mapper;
        }
        public Task<Genre[]> GetGenresAsync()
        {
            throw new NotImplementedException();
        }
    }
}
