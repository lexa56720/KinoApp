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
        private bool isFullyLoaded = false;

        public MainModel(IDataProvider dataProvider):base(dataProvider) 
        {
        }

        public async Task<Movie[]> GetMoviesAsync()
        {
            if(isFullyLoaded)
                return Array.Empty<Movie>();

            var result = await dataProvider.Movies.GetBestMoviesAsync(page);
            if (result.Length == 0)
                isFullyLoaded = true;
            else
                Interlocked.Increment(ref page);

            return result;
        }
    }
}
