using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AsmrOne.WinUI3.Models.Messagers
{
    public class RefreshAudio
    {
        public RefreshAudio(RidDetily detily, Child child)
        {
            Detily = detily;
            Child = child;
        }

        public RidDetily Detily { get; }
        public Child Child { get; }
    }

    public class RefreshAudioMessage : RequestMessage<RefreshAudio> { }
}
