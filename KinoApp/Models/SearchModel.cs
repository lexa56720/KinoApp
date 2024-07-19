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
    public class SearchModel : BaseMovieListModel
    {
        private readonly Dictionary<string, Order> Orders = new Dictionary<string, Order>();
        private int page = 1;
        private bool IsFullyLoaded = false;

        public SearchModel(IDataProvider dataProvider) : base(dataProvider)
        {
            var names = Enum.GetNames(typeof(Order));
            for (int i = 0; i < names.Length; i++)
                Orders.Add(names[i], (Order)i);
        }

        public async Task<Movie[]> GetMoviesAsync(int? yearFrom, int? yearTo, Genre genre, string order, string keyword)
        {
            if (IsFullyLoaded)
                return Array.Empty<Movie>();

            Order? queryOrder = null;
            if (Enum.TryParse(typeof(Order), order, true, out var result))
                 queryOrder = (Order)result;

            var movies = await dataProvider.Movies.GetMoviesFilteredAsync(yearFrom, yearTo, genre, queryOrder, keyword, page);
            if (movies.Length < 20)
                IsFullyLoaded = true;
            else
                Interlocked.Increment(ref page);
            return movies;
        }
        public async Task<Genre[]> GetGenresAsync()
        {
            return await dataProvider.Genres.GetGenresAsync();
        }
        public string[] GetOrderNames()
        {
            return Orders.Keys.ToArray();
        }
        public void Reset()
        {
            IsFullyLoaded = false;
            page = 1;
        }
    }
}
