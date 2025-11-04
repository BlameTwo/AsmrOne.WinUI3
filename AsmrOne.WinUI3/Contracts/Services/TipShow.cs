using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Controls;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Contracts.Services;

public class TipShow : ITipShow
{
    private Panel _owner;

    public Panel Owner
    {
        get { return _owner; }
        set { _owner = value; }
    }

    public void ShowMessage(string message, Symbol icon)
    {
        if (Owner == null)
            return;
        PopupMessage popup = new(message, Owner, icon);
        popup.ShowPopup();
    }
}
