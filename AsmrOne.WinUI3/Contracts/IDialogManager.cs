using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Contracts;

public interface IDialogManager
{
    public void SetRoot(XamlRoot root);

    public Task ShowDialogAsync<T, Value>(Value value)
        where T : ContentDialog, IDialogPage<Value>;

    #region Show Dialog

    public Task ShowRegisterAsync();
    #endregion

    public void Hide();
}
