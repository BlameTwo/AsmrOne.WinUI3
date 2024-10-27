using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using Microsoft.UI.Xaml;

namespace AsmrOne.WinUI3.Controls.ItemsBase;

public partial class DetilyBase : ItemBase<RidDetily, DetilyItemViewModel>
{
    public DetilyItemViewModel VM
    {
        get { return (DetilyItemViewModel)GetValue(VMProperty); }
        set { SetValue(VMProperty, value); }
    }

    public static readonly DependencyProperty VMProperty = DependencyProperty.Register(
        "VM",
        typeof(DetilyItemViewModel),
        typeof(DetilyBase),
        new PropertyMetadata(null)
    );
}
