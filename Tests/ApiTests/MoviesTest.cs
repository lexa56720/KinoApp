using KinoApiWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Tests.ApiTests
{
    [TestClass]
    public class MoviesTest
    {

        [TestMethod]
        public async Task TestGetMovieByIdAsync()
        {
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);

            var result = await api.Movies.GetMovieByIdAsync(1001);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task TestGetMovieByIdManyAsync()
        {
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);
            var rand = new Random();
            var ids = new int[10];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = rand.Next(10000, 50000);

            var result = await api.Movies.GetMovieByIdAsync(ids);

            Assert.IsNotNull(result);
            Assert.AreEqual(ids.Length, result.Length);
        }

        [TestMethod]
        public async Task TestGetMoviesFilteredAsync()
        {
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);

            var result = await api.Movies.GetMoviesFilteredAsync(2000, 2020, null, null, "Мстители", 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.Length);
        }

        [TestMethod]
        public async Task TestGetBestMovies()
        {
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);

            var result = await api.Movies.GetBestMoviesAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.Length);
        }
    }
}
