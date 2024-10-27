using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Common;

public partial class ItemBase<T, VM> : Control, IItem<T, VM>
    where VM : IItemViewModel<T>
{
    public VM ViewModel
    {
        get { return (VM)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }

    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        "ViewModel",
        typeof(VM),
        typeof(ItemBase<T, VM>),
        new PropertyMetadata(null)
    );
}
