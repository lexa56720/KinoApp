using KinoApiCache.CacheProvider;
using KinoApiWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tests.ApiTests;

namespace Tests.DbTests
{
    [TestClass]
    public class CacheTest
    {
        [TestMethod]
        public async Task TestGetMovieByIdCache()
        {
            var path = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "\\test.db";
            var cachePath = $"Data Source={path}";
            var actualDataProvider = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);
            var cachedDataProvider = new CacheApi(actualDataProvider, cachePath, TimeSpan.FromHours(24));


            File.Delete(path);
            var ApiMovie = await actualDataProvider.Movies.GetMovieByIdAsync(1001);
            var CacheMovie = await cachedDataProvider.Movies.GetMovieByIdAsync(1001);

            Assert.IsNotNull(ApiMovie);
            Assert.IsNotNull(CacheMovie);
            Assert.AreEqual(JsonSerializer.Serialize(ApiMovie), JsonSerializer.Serialize(CacheMovie));
        }


        [TestMethod]
        public async Task TestGetMovieByIdManyCache()
        {
            var path = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "\\test.db";
            var cachePath = $"Data Source={path}";
            var actualDataProvider = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);
            var cachedDataProvider = new CacheApi(actualDataProvider, cachePath, TimeSpan.FromHours(24));

            var rand = new Random();
            var ids = new int[10];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = rand.Next(10000, 50000);

            File.Delete(path);
            var ApiMovie = await actualDataProvider.Movies.GetMovieByIdAsync(ids);
            await cachedDataProvider.Movies.GetMovieByIdAsync(ids.Take(5).ToArray());
            var CacheMovie = await cachedDataProvider.Movies.GetMovieByIdAsync(ids.ToArray());

            Assert.IsNotNull(ApiMovie);
            Assert.IsNotNull(CacheMovie);
            Assert.AreEqual(JsonSerializer.Serialize(ApiMovie), JsonSerializer.Serialize(CacheMovie));
        }

        [TestMethod]
        public async Task TestGetMovieByYear()
        {
            var path = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "\\test.db";
            var cachePath = $"Data Source={path}";
            var actualDataProvider = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);
            var cachedDataProvider = new CacheApi(actualDataProvider, cachePath, TimeSpan.FromHours(24));

            File.Delete(path);
            var ApiMovie = await actualDataProvider.Movies.GetMoviesFilteredAsync(1944, 1944, null, KinoTypes.Order.NUM_VOTE, null, 2);
            var CacheMovie = await cachedDataProvider.Movies.GetMoviesFilteredAsync(1944, 1944, null, KinoTypes.Order.NUM_VOTE, null, 2);

            Assert.IsNotNull(ApiMovie);
            Assert.IsNotNull(CacheMovie);
            Assert.AreEqual(JsonSerializer.Serialize(ApiMovie), JsonSerializer.Serialize(CacheMovie));
        }
    }
}
