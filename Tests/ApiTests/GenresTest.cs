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
    public class GenresTest
    {

        [TestMethod]
        public async Task TestGetGenresAsync()
        {
            var api = new KinoApi(ApiConfig.ApiKey, ApiConfig.Url);

            var result = await api.Genres.GetGenresAsync();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0,result.Length);
        }
    }
}
