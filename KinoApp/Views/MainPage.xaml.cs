using System;

using KinoApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace KinoApp.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
