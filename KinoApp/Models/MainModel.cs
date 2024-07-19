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
    public class MainModel:BaseMovieListModel
    {
        private int page = 1;

        public MainModel(IDataProvider dataProvider):base(dataProvider) 
        {
        }

        public override async Task<Movie[]> GetMoviesAsync()
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
