

using AsmrOne.Models;

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
