using System.Collections.Generic;
using System.Windows.Input;

namespace AsmrOne.WinUI3.Common.NotifyIcon;

public class NotifyIconMenu
{
    public NotifyIconMenu()
    {
        Items = new();
    }

    public List<NotifyIconMenuItem> Items { get; set; }
}

public class NotifyIconMenuItem
{
    public ICommand Command { get; set; }

    public string Header { get; set; }
}
