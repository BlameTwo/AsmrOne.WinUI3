using System;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Views;

public sealed partial class SubtitleWindow : Page
{
    public SubtitleWindow(SubtitleViewModel viewModel)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
    }

    public SubtitleViewModel ViewModel { get; private set; }

    internal void Dispose()
    {
        this.ViewModel.Dispose();
        this.ViewModel = null;
    }
}
