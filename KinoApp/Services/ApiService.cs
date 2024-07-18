using KinoApiCache.CacheProvider;
using KinoApiWrapper;
using KinoApp.Helpers;
using KinoTypes.DataProvider;
using System;
using Windows.Storage;

namespace KinoApp.Services
{
    internal static class ApiService
    {
        public static IDataProvider Api { get; }
        public static string DefaultApiKey => "7d3e436f-f59c-46dd-989d-dac71211f263";
        public static string DefaultUrl => "https://kinopoiskapiunofficial.tech";
        public static TimeSpan DefaultCacheLifeTime => TimeSpan.FromHours(24);
        public static ApplicationDataContainer Settings => ApplicationData.Current.LocalSettings;

        static ApiService()
        {
            Api = new DataProvider(GetApiKey(), DefaultUrl, GetConnectionString(), GetCacheLifeTime());
        }

        public static string GetApiKey()
        {
            var key = Settings.Values["apiKey"] as string;
            if (!string.IsNullOrEmpty(key)) 
            {
                return key;
            }
            Settings.SaveString("apiKey", DefaultApiKey);
            return DefaultApiKey;
        }

        public static string GetConnectionString()
        {
            var path = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "\\test.db";
            return $"Data Source={path}";
        }

        public static TimeSpan GetCacheLifeTime()
        {
            var cacheLifetime = Settings.Read<TimeSpan>("cacheLifeTime");
            if (cacheLifetime != default)
                return cacheLifetime;

            Settings.Save("cacheLifeTime", DefaultCacheLifeTime);
            return DefaultCacheLifeTime;
        }

        private class DataProvider : IDataProvider
        {
            public IGenres Genres { get; }
            public IMovies Movies { get; }

            private KinoApi actualDataProvider;
            private CacheApi cachedDataProvider;

            public DataProvider(string apiKey, string url, string connectionString, TimeSpan cacheLife)
            {
                actualDataProvider = new KinoApi(apiKey, url);

                cachedDataProvider = new CacheApi(actualDataProvider.Genres,
                                                  actualDataProvider.Movies,
                                                  connectionString,
                                                  cacheLife);

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
}
