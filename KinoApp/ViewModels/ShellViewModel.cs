﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinoApp.Helpers;
using KinoApp.Services;
using KinoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace KinoApp.ViewModels
{
    public class ShellViewModel : ObservableObject
    {
        public bool IsBackEnabled
        {
            get { return isBackEnabled; }
            set { SetProperty(ref isBackEnabled, value); }
        }
        private bool isBackEnabled;

        public WinUI.NavigationViewItem Selected
        {
            get { return selected; }
            set { SetProperty(ref selected, value); }
        }
        private WinUI.NavigationViewItem selected;

        public ICommand ItemInvokedCommand
        {
            get
            {
                if (itemInvokedCommand == null)
                    itemInvokedCommand = new RelayCommand<WinUI.NavigationViewItemInvokedEventArgs>(OnItemInvoked);
                return itemInvokedCommand;
            }
        }
        private ICommand itemInvokedCommand;


        private WinUI.NavigationView navigationView;

        public ShellViewModel()
        {
        }

        public void Initialize(Frame frame, WinUI.NavigationView navigationView)
        {
            this.navigationView = navigationView;
            NavigationService.Frame = frame;
            NavigationService.NavigationFailed += FrameNavigationFailed;
            NavigationService.Navigated += FrameNavigated;
            this.navigationView.BackRequested += OnBackRequested;
        }

        private void OnItemInvoked(WinUI.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationService.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
                return;
            }

            if (args.InvokedItemContainer is WinUI.NavigationViewItem selectedItem &&
                selectedItem?.GetValue(NavHelper.NavigateToProperty) is Type pageType)
            {
                NavigationService.Navigate(pageType, null, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void OnBackRequested(WinUI.NavigationView sender, WinUI.NavigationViewBackRequestedEventArgs args)
        {
            NavigationService.GoBack();
        }

        private void FrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw e.Exception;
        }

        //Изменение выбранного элемента в меню навигации при навигации
        private void FrameNavigated(object sender, NavigationEventArgs e)
        {
            IsBackEnabled = NavigationService.CanGoBack;
            if (e.SourcePageType == typeof(SettingsPage))
            {
                Selected = navigationView.SettingsItem as WinUI.NavigationViewItem;
                return;
            }

            var selectedItem = GetSelectedItem(navigationView.MenuItems, e.SourcePageType);
            if (selectedItem != null)
            {
                Selected = selectedItem;
            }
        }

        //Получение элемента из меню навигации с соответсвующим типом страницы
        private WinUI.NavigationViewItem GetSelectedItem(IEnumerable<object> menuItems, Type pageType)
        {
            foreach (var item in menuItems.OfType<WinUI.NavigationViewItem>())
            {
                if (IsMenuItemForPageType(item, pageType))
                {
                    return item;
                }

                var selectedChild = GetSelectedItem(item.MenuItems, pageType);
                if (selectedChild != null)
                {
                    return selectedChild;
                }
            }
            return null;
        }

        private bool IsMenuItemForPageType(WinUI.NavigationViewItem menuItem, Type sourcePageType)
        {
            //Проверка ссылается ли menuItem на страницу типа sourcePageType
            var pageType = menuItem.GetValue(NavHelper.NavigateToProperty) as Type;
            return pageType == sourcePageType;
        }
    }
}
