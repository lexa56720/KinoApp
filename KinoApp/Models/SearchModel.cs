using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    public class SearchModel : BaseMovieListModel
    {
        private readonly Dictionary<string, Order> Orders = new Dictionary<string, Order>();
        private int page = 1;
        public SearchModel(IDataProvider dataProvider) : base(dataProvider)
        {
            var names = Enum.GetNames(typeof(Order));
            for (int i = 0; i < names.Length; i++)
                Orders.Add(names[i], (Order)i);
        }

        public async Task<Movie[]> GetMoviesAsync(int yearFrom, int yearTo, Genre genre, string order, string keyword)
        {
            if (yearTo == 1800)
                yearTo = 3000;

            int genreId = -1;
            if(genre!=null)
                genreId=genre.Id;

            if (!Enum.TryParse(typeof(Order),order,true,out var result) || !(result is Order queryOrder))
                queryOrder = Order.RATING;


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
            page = 1;
        }
    }
}
