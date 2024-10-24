using AsmrOne.WinUI3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Views;

public sealed partial class SubtitleWindow : Page
{
    public SubtitleWindow()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.ServiceProvider.GetService<SubtitleViewModel>();
    }

    public SubtitleViewModel ViewModel { get; }
}
