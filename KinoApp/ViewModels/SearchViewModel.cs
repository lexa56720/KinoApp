using KinoApp.Models;
using KinoApp.Services;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.ViewModels
{
    public class SearchViewModel : BaseMovieListViewModel<SearchModel>
    {
        public Genre[] Genres
        {
            get => genres;
            set => SetProperty(ref genres, value);
        }
        private Genre[] genres = Array.Empty<Genre>();

        public string SelectedGenre
        {
            get => selectedGenre;
            set => SetProperty(ref selectedGenre, value);
        }
        private string selectedGenre;

        public string SelectedOrder
        {
            get => selectedOrder;
            set => SetProperty(ref selectedOrder, value);
        }
        private string selectedOrder;
        public string Keyword
        {
            get => keyword;
            set => SetProperty(ref keyword, value);
        }
        private string keyword;
        public string[] Orders
        {
            get => orders;
            set => SetProperty(ref orders, value);
        }
        private string[] orders;

        public DateTimeOffset FromDate
        {
            get => fromDate;
            set => SetProperty(ref fromDate, value);
        }
        private DateTimeOffset fromDate;

        public DateTimeOffset ToDate
        {
            get => toDate;
            set => SetProperty(ref toDate, value);
        }
        private DateTimeOffset toDate;

        public DateTimeOffset MinYear => new DateTimeOffset(new DateTime(1800, 1, 1));

        internal override async Task InitAsync()
        {
            await base.InitAsync();
            Genres = await model.GetGenresAsync();
            Orders = model.GetOrderNames();
        }

        protected override SearchModel CreateModel()
        {
            return new SearchModel(ApiService.Api);
        }
        protected override async Task LoadMore()
        {
            await model.GetMoviesAsync(FromDate.Year,
                                       ToDate.Year,
                                       Genres.SingleOrDefault(g => g.Name == SelectedGenre),
                                       SelectedOrder,
                                       Keyword);
        }
    }
}
