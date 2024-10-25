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

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e) { }

    private void titleBar_DoubleTapped(
        object sender,
        Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e
    )
    {
        e.Handled = true;
    }
}
