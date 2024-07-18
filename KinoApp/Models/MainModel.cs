using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    internal class MainModel
    {
        private readonly IDataProvider dataProvider;

        private int page = 1;

        private bool IsFullyLoaded = false;

        public MainModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }


        public async Task<Movie[]> GetMoviesAsync()
        {
            if(IsFullyLoaded)
                return Array.Empty<Movie>();
            var result = await dataProvider.Movies.GetMovieByYearAsync(1944, Order.RATING, page);
            if (result.Length == 0)
                IsFullyLoaded = true;
            else
                Interlocked.Increment(ref page);
            return result;
        }
    }
}
