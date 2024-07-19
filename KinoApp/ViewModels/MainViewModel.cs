using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinoApp.Models;
using KinoApp.Services;
using KinoApp.Views;
using KinoTypes;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;


namespace KinoApp.ViewModels
{
    public class MainViewModel : BaseMovieListViewModel<MainModel>
    {
        protected override MainModel CreateModel()
        {
            return new MainModel(ApiService.Api);
        }
    }
}
