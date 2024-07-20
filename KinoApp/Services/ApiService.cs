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
        public static string DefaultApiKey => "7bf960ec-a7f8-4d62-839d-c5cd9e59c114";
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
        public static TimeSpan GetCacheLifeTime()
        {
            var cacheLifetime = Settings.Read<TimeSpan>("cacheLifeTime");
            if (cacheLifetime != default)
                return cacheLifetime;

            Settings.Save("cacheLifeTime", DefaultCacheLifeTime);
            return DefaultCacheLifeTime;
        }

        public static void SetApiKey(string key)
        {
            Settings.SaveString("apiKey", key);
            ((DataProvider)Api).UpdateApiKey(key);
        }
        public static void SetCacheLifeTime(TimeSpan lifetime)
        {
            Settings.Save("cacheLifeTime", lifetime);
            ((DataProvider)Api).UpdateCacheLifeTime(lifetime);
        }
        private static string GetConnectionString()
        {
            var path = ApplicationData.Current.LocalCacheFolder.Path + "\\cache.db";
            return $"Data Source={path}";
        }

        private class DataProvider : IDataProvider
        {
            public IGenres Genres { get; }
            public IMovies Movies { get; }
            public IApiInfo ApiInfo { get; }

            private KinoApi actualDataProvider;
            private CacheApi cachedDataProvider;

            public DataProvider(string apiKey, string url, string connectionString, TimeSpan cacheLife)
            {
                actualDataProvider = new KinoApi(apiKey, url);

                cachedDataProvider = new CacheApi(actualDataProvider,
                                                  connectionString,
                                                  cacheLife);

                Genres = cachedDataProvider.Genres;
                Movies = cachedDataProvider.Movies;
                ApiInfo = cachedDataProvider.ApiInfo;
            }
            public void Dispose()
            {
                cachedDataProvider.Dispose();
                actualDataProvider.Dispose();
            }

            public void UpdateApiKey(string apiKey)
            {
                actualDataProvider.UpdateApiKey(apiKey);
            }
            public void UpdateCacheLifeTime(TimeSpan lifeTime)
            {
                cachedDataProvider.UpdateCacheLife(lifeTime);
            }
        }
    }
}
