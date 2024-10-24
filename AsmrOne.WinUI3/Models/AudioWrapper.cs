using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.Models
{
    public interface IAudioDataWrapper
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
        Child child;

        [ObservableProperty]
        double size;
    }

    public sealed partial class ImageWrapper : FileWrapper, IAudioDataWrapper
    {
        public string Type => "Image";

        [ObservableProperty]
        string mediaDownloadUrl;

        [ObservableProperty]
        Child child;

        [ObservableProperty]
        string mediaStreamUrl;
    }

    public partial class SubtitleWrapper : FileWrapper, IAudioDataWrapper
    {
        [ObservableProperty]
        double duration;
        public string Type => "Subtitle";

        [ObservableProperty]
        string mediaStreamUrl;

        [ObservableProperty]
        Child child;

        [ObservableProperty]
        string mediaTitlemediaDownloadUrl;
    }

    public partial class TextWrapper : FileWrapper, IAudioDataWrapper
    {
        public string Type => "Text";

        [ObservableProperty]
        string mediaStreamUrl;

        [ObservableProperty]
        Child child;

        [ObservableProperty]
        string mediaTitlemediaDownloadUrl;
    }

    public partial class AudioWrapper : FileWrapper, IAudioDataWrapper
    {
        public string Type => "Audio";

        [ObservableProperty]
        string mediaStreamUrl;

        [ObservableProperty]
        string streamLowQualityUrl;

        [ObservableProperty]
        Child child;

        [ObservableProperty]
        double duration;

        [ObservableProperty]
        RidDetily work;

        [ObservableProperty]
        List<IAudioDataWrapper> subTitles;

        [RelayCommand]
        async Task Play()
        {
            await ProgramLife
                .ServiceProvider.GetService<IAudioPlayerService>()
                .PlayerAsync(this, Work);
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
    }
}
