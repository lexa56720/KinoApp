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
        private readonly Dictionary<string, Order> Orders;
        private int page = 1;
        private bool IsFullyLoaded = false;

        public SearchModel(IDataProvider dataProvider) : base(dataProvider)
        {
            Orders = new Dictionary<string, Order>()
            {
                { "Рейтингу",Order.RATING },
                { "Году",Order.YEAR },
                { "Кол-ву оценок",Order.NUM_VOTE}
            };
        }

        public async Task<Movie[]> GetMoviesAsync(int? yearFrom, int? yearTo, Genre genre, string order, string keyword)
        {
            if (IsFullyLoaded)
                return Array.Empty<Movie>();

            SortDate(ref yearFrom, ref yearTo);

            Order? queryOrder = null;
            if (order != null && Orders.TryGetValue(order, out var res))
                queryOrder = res;

            var movies = await dataProvider.Movies.GetMoviesFilteredAsync(yearFrom, yearTo, genre, queryOrder, keyword, page);
            if (movies.Length < 20)
                IsFullyLoaded = true;
            else
                Interlocked.Increment(ref page);
            return movies;
        }

        public void SortDate(ref int? from, ref int? to)
        {
            if (from != null && to != null && from > to)
                (to, from) = (from, to);
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
