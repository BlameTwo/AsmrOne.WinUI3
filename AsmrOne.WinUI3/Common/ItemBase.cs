using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Common;

public partial class ItemBase<T, VM> : UserControl, IItem<T, VM>
    where VM : IItemViewModel<T>
{
    public VM ViewModel
    {
        get { return (VM)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        "ViewModel",
        typeof(VM),
        typeof(ItemBase<T, VM>),
        new PropertyMetadata(null)
    );
}
