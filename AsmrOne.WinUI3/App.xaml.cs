using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Contracts;

namespace AsmrOne.WinUI3;

public partial class App : AsmrApplication
{
    public App()
    {
        ProgramLife.InitService();
        this.InitializeComponent();
        this.UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(
        object sender,
        Microsoft.UI.Xaml.UnhandledExceptionEventArgs e
    )
    {
        e.Handled = true;
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        ProgramLife.GetService<IAppSetup<App>>().RegisterApp(this);
        ProgramLife.GetService<IAppSetup<App>>().Start();
    }
}
