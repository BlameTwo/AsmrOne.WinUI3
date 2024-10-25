using Microsoft.UI.Xaml;

namespace AsmrOne.WinUI3.Common;

public partial class AsmrApplication : Application
{
    public Window Window { get; internal set; }

    public SubtitleWindowBase SubtitleWindow { get; internal set; }
}
