using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.NotifyIcon;
using Microsoft.UI.Xaml;

namespace AsmrOne.WinUI3.Contracts
{
    public interface IAppSetup<T>
        where T : AsmrApplication
    {
        public Window MainWindow { get; }
        T App { get; }
        public NotifyIconWinUI NotifyIcon { get; }
        public void RegisterApp(T app);
        public SubtitleWindowBase SubWindow { get; }
        public void Start();

        public void RegisterSubtitleWindow(SubtitleWindowBase window);
        void DisponseSubtitle();
    }
}
