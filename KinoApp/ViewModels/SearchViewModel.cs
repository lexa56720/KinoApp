using CommunityToolkit.Mvvm.Input;
using KinoApp.Models;
using KinoApp.Services;
using KinoTypes;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public string[] Orders
        {
            get => orders;
            set => SetProperty(ref orders, value);
        }
        private string[] orders;

        public string SelectedGenre
        {
            get => selectedGenre;
            set
            {
                SetProperty(ref selectedGenre, value);
            }
        }
        private string selectedGenre = null;

        public string SelectedOrder
        {
            get => selectedOrder;
            set
            {
                SetProperty(ref selectedOrder, value);
            }
        }
        private string selectedOrder = null;
        public string Keyword
        {
            get => keyword;
            set
            {
                SetProperty(ref keyword, value);
            }
        }
        private string keyword = null;

        public DateTimeOffset FromDate
        {
            get => fromDate;
            set
            {
                SetProperty(ref fromDate, value);
            }
        }
        private DateTimeOffset fromDate = new DateTimeOffset(DateTime.Now);

        public DateTimeOffset ToDate
        {
            get => toDate;
            set
            {
                SetProperty(ref toDate, value);
            }
        }
        private DateTimeOffset toDate = new DateTimeOffset(DateTime.Now);

        public DateTimeOffset MinYear => new DateTimeOffset(new DateTime(1800, 1, 1));

        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                    searchCommand = new AsyncRelayCommand(Search);
                return searchCommand;
            }
        }
        private ICommand searchCommand;

        public ICommand ResetCommand
        {
            get
            {
                if (resetCommand == null)
                    resetCommand = new AsyncRelayCommand(Reset);
                return resetCommand;
            }
        }
        private ICommand resetCommand;

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
            var movies = await model.GetMoviesAsync(GetYear(FromDate),
                                        GetYear(ToDate),
                                        Genres.SingleOrDefault(g => g.Name == SelectedGenre),
                                        SelectedOrder,
                                        Keyword);
            foreach (var movie in movies)
                Movies.Add(new MovieViewModel(movie));
        }
        private int? GetYear(DateTimeOffset date)
        {
            var year = date.Year;
            if (year <= MinYear.Year)
                return null;
            return year;
        }
        private async Task Search()
        {
            foreach (var movie in Movies)
                movie.Dispose();
            Movies.Clear();
            model.Reset();
            await LoadMore();
        }
        private async Task Reset()
        {
            SelectedGenre = null;
            SelectedOrder = null;
            Keyword = null;
            FromDate = new DateTimeOffset(DateTime.Now);
            ToDate = new DateTimeOffset(DateTime.Now);
            await Search();
        }
    }
}
