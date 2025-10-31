using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.ViewModels;
using CommunityToolkit.WinUI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Views;

public sealed partial class SearchPage : Page,IPage
{
    public SearchPage()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.GetService<SearchViewModel>();

    }
    public SearchViewModel ViewModel { get; }

    public Type PageType => typeof(SearchPage);

    public void Dispose()
    {
        this.ViewModel.Dispose();
    }
}