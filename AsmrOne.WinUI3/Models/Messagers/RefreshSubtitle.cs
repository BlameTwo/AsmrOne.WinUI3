using static AsmrOne.WinUI3.Common.WebVTTParse;

namespace AsmrOne.WinUI3.Models.Messagers
{
    public class RefreshSubtitle
    {
        public SubtitleItem Item { get; init; }

        public RefreshSubtitle(SubtitleItem item)
        {
            Item = item;
        }
    }
}
