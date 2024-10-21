using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases.ItemsBase;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AsmrOne.WinUI3.Views.Items
{
    public sealed partial class DetilyItem : DetilyBase
    {
        public DetilyItem()
        {
            this.InitializeComponent();
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "PointerOver", true);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }
    }
}
