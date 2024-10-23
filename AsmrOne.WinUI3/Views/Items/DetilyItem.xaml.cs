using AsmrOne.WinUI3.Common.Bases.ItemsBase;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace AsmrOne.WinUI3.Views.Items
{
    public sealed partial class DetilyItem : DetilyBase
    {
        public DetilyItem()
        {
            this.InitializeComponent();
            VisualStateManager.GoToState(this, "Normal", true);
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
