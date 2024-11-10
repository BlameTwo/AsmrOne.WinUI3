using System;
using System.Collections.Generic;
using AsmrOne.WinUI3.ViewModels;
using AsmrOne.WinUI3.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Contracts.Services;

public sealed partial class PageService : IPageService
{
    private readonly Dictionary<string, Tuple<Type, Type>> _pages;

    public PageService()
    {
        _pages = new();
        RegisterView<HomePage, HomeViewModel>();
        RegisterView<RidDetilyPage, RidDetilyViewModel>();
        RegisterView<FavouritesPage, FavouritesViewModel>();
        RegisterView<SettingPage, SettingViewModel>();
        RegisterView<PopularPage, PopularViewModel>();
        RegisterView<TestPage, TestViewModel>();
    }

    public Type GetPage(string key)
    {
        _pages.TryGetValue(key, out var page);
        if (page is null)
        {
            return null;
        }
        return page.Item1;
    }

    public void RegisterView<View, ViewModel>()
        where View : Page
        where ViewModel : ObservableObject =>
        _pages.Add(typeof(ViewModel).FullName, new(typeof(View), typeof(ViewModel)));
}
