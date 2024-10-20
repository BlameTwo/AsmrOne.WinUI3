using System;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Contracts.Services
{
    public class DialogManager : IDialogManager
    {
        private ContentDialog dialog;

        public XamlRoot Root { get; private set; }

        public void Hide()
        {
            if (dialog != null)
            {
                dialog.Hide();
                dialog = null;
            }
        }

        public async Task ShowRegisterAsync()
        {
            await ShowDialogAsync<RegisterDialog, string>("");
        }

        public void SetRoot(XamlRoot root)
        {
            this.Root = root;
        }

        public async Task ShowDialogAsync<T, Value>(Value value)
            where T : ContentDialog, IDialogPage<Value>
        {
            if (dialog != null)
                return;
            var result = ProgramLife.ServiceProvider.GetService<T>();
            result.SetData(value);
            result.XamlRoot = this.Root;
            this.dialog = result;
            await result.ShowAsync();
        }
    }
}
