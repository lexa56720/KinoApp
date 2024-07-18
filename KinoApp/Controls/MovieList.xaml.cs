using CommunityToolkit.Mvvm.Input;
using KinoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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



        public ICommand FavoriteClick
        {
            get
            {
                if (favoriteClick == null)
                    favoriteClick = new RelayCommand<MovieViewModel>(FavoriteSwitch);
                return favoriteClick;
            }
        }

        private void FavoriteSwitch(MovieViewModel model)
        {
            model.IsFavorite = !model.IsFavorite;
        }

        private ICommand favoriteClick;

        public MovieList()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }
    }
}
