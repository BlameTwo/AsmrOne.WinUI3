using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Data;

namespace AsmrOne.WinUI3.Models
{
    [Bindable]
    public partial class PingResult : ObservableObject
    {
        [ObservableProperty]
        string hostName;

        [ObservableProperty]
        long time;
    }
}
