﻿using KinoApp.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            var path = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path+ "\\test.db";
            var cachePath = $"Data Source={path}";
            var dataProvider = new DataProvider(ApiConfig.ApiKey, ApiConfig.Url, cachePath, TimeSpan.FromHours(24));

            File.Delete(path);
            var ApiMovie = await dataProvider.Movies.GetMovieByIdAsync(1001);
            var CacheMovie = await dataProvider.Movies.GetMovieByIdAsync(1001);

            Assert.IsNotNull(ApiMovie);
            Assert.IsNotNull(CacheMovie);
            Assert.AreEqual(JsonSerializer.Serialize(ApiMovie), JsonSerializer.Serialize(CacheMovie));
        }

        [TestMethod]
        public async Task TestGetMovieByYear()
        {
            var path = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "\\test.db";
            var cachePath = $"Data Source={path}";
            var dataProvider = new DataProvider(ApiConfig.ApiKey, ApiConfig.Url, cachePath, TimeSpan.FromHours(24));

            File.Delete(path);
            var ApiMovie = await dataProvider.Movies.GetMovieByYearAsync(1944,KinoTypes.Order.NUM_VOTE,2);
            var CacheMovie = await dataProvider.Movies.GetMovieByYearAsync(1944, KinoTypes.Order.NUM_VOTE, 2);

            Assert.IsNotNull(ApiMovie);
            Assert.IsNotNull(CacheMovie);
            Assert.AreEqual(JsonSerializer.Serialize(ApiMovie), JsonSerializer.Serialize(CacheMovie));
        }
    }
}