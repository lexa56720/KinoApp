using KinoApiWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task TestGetMovieByYearAsync()
        {
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);

            var result = await api.Movies.GetMovieByYearAsync(1940, 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.Length);
        }

        [TestMethod]
        public async Task TestGetMovieByGenreAsync()
        {
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);

            var result = await api.Movies.GetMovieByGenreAsync(new KinoTypes.Genre() { Id = 15 }, 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.Length);
        }

        [TestMethod]
        public async Task TestGetMovieByKeywordAsync()
        {
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);

            var result = await api.Movies.GetMoviesByKeywordAsync("мстители", 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.Length);
        }
    }
}
