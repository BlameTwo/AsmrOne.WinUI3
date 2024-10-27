using System;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Views;

public sealed partial class PopularPage : Page, IPage
{
    public PopularPage()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.GetService<PopularViewModel>();
    }

    public Type PageType => typeof(PopularPage);

    public PopularViewModel ViewModel { get; }

    public void Dispose()
    {
        this.ViewModel.Dispose();
    }
}
