using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.ViewModels.DialogViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Views.Dialogs;

public sealed partial class RegisterDialog : ContentDialog, IDialogPage<string>
{
    public RegisterDialog()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.ServiceProvider.GetService<RegisterViewModel>();
    }

    public RegisterViewModel ViewModel { get; }

    public void SetData(string value) { }
}
