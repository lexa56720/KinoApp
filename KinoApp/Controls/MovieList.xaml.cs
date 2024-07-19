using CommunityToolkit.Mvvm.Input;
using KinoApp.Services;
using KinoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace KinoApp.Controls
{
    public sealed partial class MovieList : UserControl
    {
        public static readonly DependencyProperty MoviesProperty =
            DependencyProperty.Register(nameof(Movies),
                                        typeof(ObservableCollection<MovieViewModel>),
                                        typeof(MovieList),
                                        new PropertyMetadata(null));

        public ObservableCollection<MovieViewModel> Movies
        {
            get => (ObservableCollection<MovieViewModel>)GetValue(MoviesProperty);
            set => SetValue(MoviesProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title),
                                        typeof(string),
                                        typeof(MovieList),
                                        new PropertyMetadata(null));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }


        public static readonly DependencyProperty OpenMovieCommandProperty =
            DependencyProperty.Register(nameof(OpenMovieCommand),
                                        typeof(ICommand),
                                        typeof(MovieList),
                                        new PropertyMetadata(null));

        public ICommand OpenMovieCommand
        {
            get => (ICommand)GetValue(OpenMovieCommandProperty);
            set => SetValue(OpenMovieCommandProperty, value);
        }


        public static readonly DependencyProperty LoadMoreCommandProperty =
            DependencyProperty.Register(nameof(LoadMoreCommand),
                                        typeof(IAsyncRelayCommand),
                                        typeof(MovieList),
                                        new PropertyMetadata(null));

        public IAsyncRelayCommand LoadMoreCommand
        {
            get => (IAsyncRelayCommand)GetValue(LoadMoreCommandProperty);
            set => SetValue(LoadMoreCommandProperty, value);
        }

        private bool IsLoading = false;
        public MovieList()
        {
            this.InitializeComponent();
            DataContext = this;
        }
        private void FavoriteSwitch(MovieViewModel model)
        {
            if (model.IsFavorite)
                FavoriteService.Remove(model.Movie);
            else
                FavoriteService.Add(model.Movie);
        }
        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Grid grid && grid.DataContext is MovieViewModel movie)
            {
                OpenMovieCommand.Execute(movie);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is MovieViewModel movie)
            {
                FavoriteSwitch(movie);
            }
        }

        private async void ScrollViewer_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            if (sender is ScrollViewer sv && IsLoading == false &&
                IsBottomReached(sv.ExtentHeight, e.NextView.VerticalOffset, sv.ViewportHeight))
            {
                IsLoading = true;
                await LoadMoreCommand.ExecuteAsync(null);
                IsLoading = false;
            }
        }

        private bool IsBottomReached(double extentHeight, double verticalOffset, double viewportHeight)
        {
            var diff = extentHeight - (verticalOffset + viewportHeight);
            return Math.Abs(diff) < viewportHeight / 2;
        }
    }
}
