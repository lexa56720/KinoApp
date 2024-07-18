using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    internal class MainModel
    {
        private readonly IDataProvider dataProvider;

        public MainModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }


        public async Task<Movie[]> GetMoviesAsync()
        {
            return await dataProvider.Movies.GetMovieByYearAsync(1944);
        }
    }
}
