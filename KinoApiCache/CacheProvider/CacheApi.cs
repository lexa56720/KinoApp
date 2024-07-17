﻿using KinoApiCache.DataBase;
using KinoApiCache.DataBase.Interaction;
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

        public CacheApi(IGenres genresProvider, IMovies moviesProvider, string connectionString, TimeSpan cacheLife)
        {
            var mapper = new Mapper();

            var contextFactory = new DbContextFactory(connectionString);
            var interactor = new CachedCalls(contextFactory, cacheLife, mapper);
         
            Genres = new CachedGenres(genresProvider, interactor);
            Movies = new CachedMovies(moviesProvider, interactor);
        }
        public void Dispose()
        {
            return;
        }
    }
}
