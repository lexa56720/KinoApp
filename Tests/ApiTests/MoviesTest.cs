﻿using KinoApiWrapper;
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
        private static string apiKey = "7d3e436f-f59c-46dd-989d-dac71211f263";
        private static string url = "https://kinopoiskapiunofficial.tech";

        [TestMethod]
        public async Task TestGetMovieByIdAsync()
        {
            var api = new KinoApi(apiKey, url);

            var result=await api.Movies.GetMovieByIdAsync(1001);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestGetMovieByYearAsync()
        {
            var api = new KinoApi(apiKey, url);

            var result = await api.Movies.GetMovieByYearAsync(1940);

            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.Length);
        }
    }
}
