using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinoApp.Helpers;
using KinoApp.Services;



using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace KinoApp.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        public string ApiKey
        {
            get { return apiKey; }
            set
            {
                if (UpdateApiKey(value))
                    SetProperty(ref apiKey, value);
            }
        }
        private string apiKey;

        public int CacheLife
        {
            get => cacheLife;
            set
            {
                SetProperty(ref cacheLife, value);
                UpdateCacheLife(value);
            }
        }
        private int cacheLife;

        public ICommand ResetCommand
        {
            get
            {
                if (resetCommand == null)
                    resetCommand = new RelayCommand(Reset);
                return resetCommand;
            }
        }
        private ICommand resetCommand;

        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                    clearCommand = new RelayCommand(Clear);
                return clearCommand;
            }
        }
        private ICommand clearCommand;

        public string VersionDescription
        {
            get { return versionDescription; }
            set { SetProperty(ref versionDescription, value); }
        }
        private string versionDescription;
        public SettingsViewModel()
        {
        }

        public void Initialize()
        {
            VersionDescription = GetVersionDescription();
            ApiKey = ApiService.GetApiKey();
            CacheLife = (int)ApiService.GetCacheLifeTime().TotalHours;
        }
        private void Reset()
        {
            ApiKey = ApiService.DefaultApiKey;
            CacheLife = (int)ApiService.DefaultCacheLifeTime.TotalHours;
        }
        private void Clear()
        {
            throw new NotImplementedException();
        }
        private string GetVersionDescription()
        {
            var appName = Package.Current.DisplayName;
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private bool UpdateApiKey(string value)
        {
            throw new NotImplementedException();
        }

        private void UpdateCacheLife(int value)
        {
            if (value < 0)
                ApiService.SetCacheLifeTime(TimeSpan.MaxValue);
            else
                ApiService.SetCacheLifeTime(TimeSpan.FromHours(value));
        }
    }
}
