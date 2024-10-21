using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace AsmrOne.WinUI3.Controls
{
    public sealed partial class HeaderTile : ContentControl
    {
        protected override void OnApplyTemplate()
        {
            Button = (HyperlinkButton)GetTemplateChild("button");
            base.OnApplyTemplate();
        }

        public ICommand InvokeCommand
        {
            get { return (ICommand)GetValue(InvokeCommandProperty); }
            set { SetValue(InvokeCommandProperty, value); }
        }

        public HyperlinkButton Button { get; private set; }

        public static readonly DependencyProperty InvokeCommandProperty =
            DependencyProperty.Register(
                "InvokeCommand",
                typeof(ICommand),
                typeof(HeaderTile),
                new PropertyMetadata(null)
            );
    }
}
