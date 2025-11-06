using System;
using System.Collections.ObjectModel;
using System.Linq;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.ViewModels.ItemViewModels;

public sealed partial class DetilyItemViewModel : ViewModelBase, IItemViewModel<RidDetily>
{
    [ObservableProperty] 
    public partial string Cover { get; set; }

    [ObservableProperty] 
    public partial long Id { get; set; }

    [ObservableProperty] 
    public partial string Title { get; set; }

    [ObservableProperty] 
    public partial string Name { get; set; }

    [ObservableProperty]
    public partial string Money { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<Tag> Tags { get; set; }

    [ObservableProperty]
    public partial bool IsNTFS { get; set; }

    [ObservableProperty]
    public partial string Duration { get; set; }

    public void Dispose()
    {
    }

    public void SetData(RidDetily value)
    {
        this.Cover = value.MainCoverUrl;
        this.Id = value.Id;
        this.Title = value.Title;
        this.Name = value.Name;
        this.Money = value.Price.ToString();
        this.IsNTFS = value.Nsfw;
        var dur = TimeSpan.FromSeconds(value.Duration);
        this.Duration = dur.ToString("c");
        if (value.Tags.Count < 4)
        {
            this.Tags = value.Tags.ToObservable();
        }
        else
        {
            this.Tags = value.Tags.Take(4).ToObservable();
        }
    }

    internal void Disponse()
    {
        this.Tags.Clear();
    }

    [RelayCommand]
    void Invoke()
    {
        ProgramLife
            .ServiceProvider.GetKeyedService<INavigationService>(ProgramLife.ShellNavigationKey)
            .NavigationTo<RidDetilyViewModel>(this.Id.ToString());
    }
}
