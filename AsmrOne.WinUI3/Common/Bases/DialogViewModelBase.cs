using AsmrOne.WinUI3.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AsmrOne.WinUI3.Common.Bases;

public partial class DialogViewModelBase : ObservableObject
{
    public DialogViewModelBase(IDialogManager dialogManager)
    {
        DialogManager = dialogManager;
    }

    public IDialogManager DialogManager { get; }

    public void Close()
    {
        DialogManager.Hide();
    }

    [RelayCommand]
    void Hide()
    {
        Close();
    }
}
