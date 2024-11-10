using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace AsmrOne.WinUI3.Controls
{
    public partial class DesktopAcrylicBackdrop : SystemBackdrop
    {
        public DesktopAcrylicKind Kind { get; set; } = DesktopAcrylicKind.Default;
        private DesktopAcrylicController acrylicController;
        public SystemBackdropConfiguration BackdropConfiguration { get; private set; }

        protected override void OnTargetConnected(
            ICompositionSupportsSystemBackdrop connectedTarget,
            XamlRoot xamlRoot
        )
        {
            base.OnTargetConnected(connectedTarget, xamlRoot);
            acrylicController = new DesktopAcrylicController() { Kind = this.Kind };
            acrylicController.AddSystemBackdropTarget(connectedTarget);
            BackdropConfiguration = GetDefaultSystemBackdropConfiguration(
                connectedTarget,
                xamlRoot
            );
            acrylicController.SetSystemBackdropConfiguration(BackdropConfiguration);
        }

        protected override void OnTargetDisconnected(
            ICompositionSupportsSystemBackdrop disconnectedTarget
        )
        {
            base.OnTargetDisconnected(disconnectedTarget);

            if (acrylicController is not null)
            {
                acrylicController.RemoveSystemBackdropTarget(disconnectedTarget);
                acrylicController = null;
            }
        }

        protected override void OnDefaultSystemBackdropConfigurationChanged(
            ICompositionSupportsSystemBackdrop target,
            XamlRoot xamlRoot
        )
        {
            if (target != null)
                base.OnDefaultSystemBackdropConfigurationChanged(target, xamlRoot);
        }

        public DesktopAcrylicBackdrop(DesktopAcrylicKind desktopAcrylicKind)
        {
            this.Kind = desktopAcrylicKind;
        }
    }
}
