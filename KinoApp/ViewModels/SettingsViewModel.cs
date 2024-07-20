using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinoApp.Helpers;
using KinoApp.Models;
using KinoApp.Services;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KinoApp.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        public string ApiKey
        {
            get { return apiKey; }
            set
            {
                Task.Run(async () => { await UpdateApiKey(value); });
            }
        }
        private string apiKey = ApiService.GetApiKey();

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
                    resetCommand = new AsyncRelayCommand(Reset);
                return resetCommand;
            }
        }
        private ICommand resetCommand;

        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                    clearCommand = new AsyncRelayCommand(Clear);
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

        private readonly SettingsModel model;
        public SettingsViewModel()
        {
            model = new SettingsModel(ApiService.Api);
        }

        public void Initialize()
        {
            VersionDescription = GetVersionDescription();
            ApiKey = ApiService.GetApiKey();
            CacheLife = (int)ApiService.GetCacheLifeTime().TotalHours;
        }
        private async Task Reset()
        {
            ContentDialog resetDialog = new ContentDialog
            {
                Title = "Вы уверены что хотите сбросить настройки?",
                Content = "Это приведёт к потере установленного ключа API и других параметров",
                CloseButtonText = "Отмена",
                PrimaryButtonText = "Подтвердить",
                DefaultButton = ContentDialogButton.Primary
            };

            ContentDialogResult result = await resetDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ApiKey = ApiService.DefaultApiKey;
                CacheLife = (int)ApiService.DefaultCacheLifeTime.TotalHours;
            }
        }
        private async Task Clear()
        {
            ContentDialog clearDialog = new ContentDialog
            {
                Title = "Вы уверены что хотите удалить все данные пользователя?",
                Content = "Это приведёт к потере списка избранных фильмов и других данных",
                CloseButtonText = "Отмена",
                PrimaryButtonText = "Подтвердить",
                DefaultButton = ContentDialogButton.Primary
            };

            ContentDialogResult result = await clearDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                FavoriteService.Clear();
            }
        }

        private string GetVersionDescription()
        {
            var appName = Package.Current.DisplayName;
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
        private async Task UpdateApiKey(string value)
        {
            var currentKey = ApiService.GetApiKey();
            if (value != currentKey && await model.IsKeyValid(value))
            {
                ApiService.SetApiKey(value);
                await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    SetProperty(ref apiKey, value, nameof(ApiKey));
                });
            }
            else
            {
                apiKey = string.Empty;
                await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    SetProperty(ref apiKey, currentKey,nameof(ApiKey));
                });
            }
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
