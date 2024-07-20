using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinoApp.Models;
using KinoApp.Services;
using KinoTypes;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace KinoApp.ViewModels
{
    public class MovieDetailViewModel : ObservableObject
    {
        public MovieInfo Movie
        {
            get => movie;
            private set => SetProperty(ref movie, value);
        }
        private MovieInfo movie;

        public Movie BriefMovie
        {
            get => briefMovie;
            set
            {
                SetProperty(ref briefMovie, value);
            }
        }
        private Movie briefMovie;

        public bool IsFavorite
        {
            get => isFavorite;
            private set => SetProperty(ref isFavorite, value);
        }
        private bool isFavorite = false;

        public ICommand FavoriteSwitchCommand
        {
            get
            {
                if (favoriteSwitchCommand == null)
                    favoriteSwitchCommand = new RelayCommand(FavoriteSwitch);
                return favoriteSwitchCommand;
            }
        }
        private ICommand favoriteSwitchCommand;

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                    loadCommand = new RelayCommand(Loaded);
                return loadCommand;
            }
        }
        private ICommand loadCommand;

        public ICommand UnLoadCommand
        {
            get
            {
                if (unLoadCommand == null)
                    unLoadCommand = new RelayCommand(UnLoaded);
                return unLoadCommand;
            }
        }
        private ICommand unLoadCommand;

        public ICommand ImageTappedCommand
        {
            get
            {
                if (imageTappedCommand == null)
                    imageTappedCommand = new RelayCommand<UIElement>(OpenImage);
                return imageTappedCommand;
            }
        }
        private ICommand imageTappedCommand;

        public ICommand ImagePointerEnteredCommand
        {
            get
            {
                if (imagePointerEnteredCommand == null)
                    imagePointerEnteredCommand = new RelayCommand(() =>
                    {
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
                    });
                return imagePointerEnteredCommand;
            }
        }
        private ICommand imagePointerEnteredCommand;

        public ICommand ImagePointerExitedCommand
        {
            get
            {
                if (imagePointerExitedCommand == null)
                    imagePointerExitedCommand = new RelayCommand(() =>
                    {
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
                    });
                return imagePointerExitedCommand;
            }
        }
        private ICommand imagePointerExitedCommand;


        private readonly MovieDetailModel model;
        public MovieDetailViewModel()
        {
            model = new MovieDetailModel(ApiService.Api);
        }

        public async Task InitAsync(MovieViewModel movie)
        {
            BriefMovie = movie.Movie;

            Movie = await model.GetMovieInfo(movie.Movie);
        }
        private void OpenImage(UIElement element)
        {
            FlyoutBase.ShowAttachedFlyout(NavigationService.Frame.Content as FrameworkElement);
        }
        private void UnLoaded()
        {
            FavoriteService.FavoriteChanged -= OnFavoriteChanged;
        }

        private void Loaded()
        {
            IsFavorite = FavoriteService.IsContains(BriefMovie);
            FavoriteService.FavoriteChanged += OnFavoriteChanged;
        }

        private void OnFavoriteChanged(object sender, FavoriteChangedEventArgs e)
        {
            if (e.Id == BriefMovie.KinopoiskId && e.IsAdded)
                IsFavorite = true;
            else if (e.Id == BriefMovie.KinopoiskId && !e.IsAdded)
                IsFavorite = false;
        }

        private void FavoriteSwitch()
        {
            if (IsFavorite)
                FavoriteService.Remove(Movie);
            else
                FavoriteService.Add(Movie);
        }
    }
}
