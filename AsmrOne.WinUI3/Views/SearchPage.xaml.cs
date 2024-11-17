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

public sealed partial class SearchPage : Page
{


    public SearchPage()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.GetService<SearchViewModel>();

    }

    public SearchViewModel ViewModel { get; }


    private void suggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (ViewModel.CacheTag == null)
            return;
        ViewModel.CacheTag = this.ViewModel.SourceTags.Where(x => x.DisplayName.ToLower().StartsWith(sender.Text.ToLower())).ToObservable();
    }

    private void suggestBox_TokenItemAdding(TokenizingTextBox sender, TokenItemAddingEventArgs args)
    {
        args.Cancel = true;
    }
}