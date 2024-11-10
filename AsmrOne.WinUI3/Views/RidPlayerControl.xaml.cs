using System.Windows.Input;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Views;

public sealed partial class RidPlayerControl : UserControl
{
    public RidPlayerControl()
    {
        this.InitializeComponent();
    }

    public ICommand CloseBarCommand
    {
        get { return (ICommand)GetValue(CloseBarCommandProperty); }
        set { SetValue(CloseBarCommandProperty, value); }
    }
    public static readonly DependencyProperty CloseBarCommandProperty = DependencyProperty.Register(
        "CloseBar",
        typeof(ICommand),
        typeof(RidPlayerControl),
        new PropertyMetadata(null)
    );

    public RidPlayerViewModel ViewModel { get; internal set; }
}
