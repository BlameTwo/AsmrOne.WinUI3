using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Messagers.ItemMessangers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.Models
{
    public interface IAudioDataWrapper : IDisposable
    {
        public string Type { get; }

        public Child Child { get; set; }
    }

    public partial class FileWrapper : ObservableObject
    {
        [ObservableProperty]
        string downloadPath;

        [ObservableProperty]
        string fileName;

        [ObservableProperty]
        double size;

        [ObservableProperty]
        Child child;
    }

    public sealed partial class ImageWrapper : FileWrapper, IAudioDataWrapper
    {
        public string Type => "Image";

        [ObservableProperty]
        string mediaDownloadUrl;

        [ObservableProperty]
        string mediaStreamUrl;

        public void Dispose()
        {
            this.Child = null;
            this.MediaDownloadUrl = null;
            this.MediaStreamUrl = null;
        }
    }

    public partial class SubtitleWrapper : FileWrapper, IAudioDataWrapper
    {
        [ObservableProperty]
        double duration;
        public string Type => "Subtitle";

        [ObservableProperty]
        string mediaStreamUrl;

        [ObservableProperty]
        string mediaTitlemediaDownloadUrl;

        public void Dispose()
        {
            this.Child = null;
            this.MediaStreamUrl = null;
            this.MediaTitlemediaDownloadUrl = null;
        }
    }

    public partial class TextWrapper : FileWrapper, IAudioDataWrapper
    {
        public string Type => "Text";

        [ObservableProperty]
        string mediaStreamUrl;

        [ObservableProperty]
        string mediaTitlemediaDownloadUrl;

        public void Dispose()
        {
            this.Child = null;
            this.MediaStreamUrl = null;
            this.MediaTitlemediaDownloadUrl = null;
        }
    }

    public partial class AudioWrapper : FileWrapper, IAudioDataWrapper
    {
        public string Type => "Audio";

        [ObservableProperty]
        string mediaStreamUrl;

        [ObservableProperty]
        string streamLowQualityUrl;

        [ObservableProperty]
        double duration;

        [ObservableProperty]
        RidDetily work;

        [ObservableProperty]
        List<IAudioDataWrapper> subTitles;

        [RelayCommand]
        void Play()
        {
            WeakReferenceMessenger.Default.Send<RidDetilySendPlayAudio>(new(this));
        }

        public void Dispose()
        {
            this.SubTitles.Clear();
            this.Work = null;
            this.Duration = 0;
            this.StreamLowQualityUrl = null;
            this.MediaStreamUrl = null;
        }
    }

    public partial class FolderWrapper : ObservableObject, IAudioDataWrapper
    {
        public string Type => "Folder";

        [ObservableProperty]
        string name;

        [ObservableProperty]
        Child child;

        [ObservableProperty]
        ObservableCollection<IAudioDataWrapper> datas;

        public void Dispose()
        {
            this.Datas.Clear();
        }
    }
}
