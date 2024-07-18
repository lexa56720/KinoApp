using CommunityToolkit.Mvvm.ComponentModel;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.ViewModels
{
    public class MovieViewModel:ObservableObject
    {
        public bool IsFavorite
        {
            get => isFavorite;
            set => SetProperty(ref isFavorite, value);
        }
        private bool isFavorite = false;

        public Movie Movie { get; }

        public MovieViewModel(Movie movie)
        {
            Movie = movie;
        }
    }
}
