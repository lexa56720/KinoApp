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
            //arrange
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);

            //act
            var result = await api.Movies.GetMovieByIdAsync(1001);

            //assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task TestGetMovieByIdManyAsync()
        {            
            //arrange
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);
            var rand = new Random();
            var ids = new int[10];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = rand.Next(10000, 50000);

            //act
            var result = await api.Movies.GetMovieByIdAsync(ids);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ids.Length, result.Length);
        }

        [TestMethod]
        public async Task TestGetMoviesFilteredAsync()
        {
            //arrange
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);

            //act
            var result = await api.Movies.GetMoviesFilteredAsync(2000, 2020, null, null, "Мстители", 1);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.Length);
        }

        [TestMethod]
        public async Task TestGetBestMovies()
        {
            //arrange
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);
            
            //act
            var result = await api.Movies.GetBestMoviesAsync(1);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.Length);
        }
    }
}
