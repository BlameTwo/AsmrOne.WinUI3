using System.Collections.ObjectModel;

namespace AsmrOne.WinUI3.Models.Messagers;

public class RefreshSubtitleSource
{
    public ObservableCollection<SubtitleWrapper> Subtitles { get; init; }

    public RefreshSubtitleSource(
        ObservableCollection<SubtitleWrapper> subtitles,
        ObservableCollection<TextWrapper> texts
    )
    {
        Subtitles = subtitles;
        Texts = texts;
    }

    public ObservableCollection<TextWrapper> Texts { get; init; }
}
