using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Views
{
    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            this.InitializeComponent();
            this.ViewModel = ProgramLife.ServiceProvider.GetService<ShellViewModel>();
            this.ViewModel.ShellNavigationService.RegisterView(this.frame);
            this.ViewModel.ShellNavigationViewService.Register(this.view);
        }

        private void ShellPage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ProgramLife.ServiceProvider.GetService<IDialogManager>().SetRoot(this.XamlRoot);
        }

        public ShellViewModel ViewModel { get; }
    }
}
