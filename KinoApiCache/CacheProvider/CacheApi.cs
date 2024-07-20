using KinoApiCache.DataBase;
using KinoApiCache.DataBase.Interaction;
using KinoApiCache.Utils;
using KinoTypes.DataProvider;
using System;

namespace KinoApiCache.CacheProvider
{
    public class CacheApi : IDataProvider
    {
        public IGenres Genres { get; }
        public IMovies Movies { get; }
        public IApiInfo ApiInfo { get; }


        private readonly CachedCalls interactor;

        public CacheApi(IDataProvider actualDataProvider, string connectionString, TimeSpan cacheLife)
        {
            var mapper = new Mapper();

            var contextFactory = new DbContextFactory(connectionString);
            interactor = new CachedCalls(contextFactory, cacheLife, mapper);

            Genres = new CachedGenres(actualDataProvider.Genres, interactor);
            Movies = new CachedMovies(actualDataProvider.Movies, interactor);
            ApiInfo = actualDataProvider.ApiInfo;
        }
        public void UpdateCacheLife(TimeSpan lifeTime)
        {
            interactor.UpdateLifeTime(lifeTime);
        }
        public void Dispose()
        {
            return;
        }
    }
}
