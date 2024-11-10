using Microsoft.UI.Xaml;

namespace AsmrOne.WinUI3.Contracts.Services;

public class AppThemeService : IAppThemeService
{
    public AppThemeService(IAppSetup<App> appSetup)
    {
        AppSetup = appSetup;
    }

    public IAppSetup<App> AppSetup { get; }

    public bool SetTheme(string themeName)
    {
        if (themeName == "Default")
        {
            (AppSetup.MainWindow.Content as FrameworkElement).RequestedTheme = ElementTheme.Default;
        }
        if (themeName == "Dark")
        {
            (AppSetup.MainWindow.Content as FrameworkElement).RequestedTheme = ElementTheme.Dark;
        }
        if (themeName == "Light")
        {
            (AppSetup.MainWindow.Content as FrameworkElement).RequestedTheme = ElementTheme.Light;
        }
        return true;
    }
}
