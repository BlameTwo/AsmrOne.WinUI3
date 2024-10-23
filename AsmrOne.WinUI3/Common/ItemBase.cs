using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Common;

public partial class ItemBase<T, VM> : UserControl, IItem<T, VM>
    where VM : IItemViewModel<T>
{
    public VM ViewModel { get; set; }
}
