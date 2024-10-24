using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Radios;

namespace AsmrOne.WinUI3.Contracts.Services.Adaptives;

public class DataAdaptiveService : IDataAdaptiveService
{
    public ObservableCollection<WorkWrapper> GetWrapperData(List<RidDetily> wors)
    {
        ObservableCollection<WorkWrapper> workWrapper = new ObservableCollection<WorkWrapper>();
        foreach (var item in wors)
        {
            workWrapper.Add(new() { Data = item });
        }

        return workWrapper;
    }

    public List<IAudioDataWrapper> GetSubTitle(ObservableCollection<IAudioDataWrapper> list)
    {
        foreach (var item in list)
        {
            var subTitle = FindSubtitle(list);
            return subTitle;
        }
        return null;
    }

    public List<IAudioDataWrapper> FindSubtitle(ObservableCollection<IAudioDataWrapper> list)
    {
        List<IAudioDataWrapper> result = new();
        foreach (var item in list)
        {
            if (item is FolderWrapper folders)
            {
                foreach (var folder in folders.Datas)
                {
                    if (folder is FolderWrapper folderWrapper)
                    {
                        return FindSubtitle(list);
                    }
                    if (folder.Type == "Text" || folder.Type == "Subtitle")
                    {
                        result.Add(folder);
                    }
                }
            }
            if (item is TextWrapper text)
            {
                result.Add(item);
            }
            if (item is SubtitleWrapper subtitle)
            {
                result.Add(item);
            }
        }
        return result;
    }

    public ObservableCollection<IAudioDataWrapper> GetAudioData(List<Child> video, RidDetily work)
    {
        ObservableCollection<IAudioDataWrapper> wrappers = new();
        foreach (var item in video)
        {
            if (item.Type == "folder")
            {
                wrappers.Add(FindFolder(item, work));
            }
            else if (item.Type == "audio")
            {
                wrappers.Add(GetAudio(item, work));
            }
            else if (item.Type == "text")
            {
                if (item.Duration > 0)
                {
                    wrappers.Add(GetSubtitle(item));
                }
                else
                {
                    wrappers.Add(GetText(item));
                }
            }
            else if (item.Type == "image")
            {
                wrappers.Add(GetImage(item));
            }
        }
        AssignSubTitlesToAudioWrappers(wrappers);
        return wrappers;
    }

    private void AssignSubTitlesToAudioWrappers(ObservableCollection<IAudioDataWrapper> wrappers)
    {
        foreach (var wrapper in wrappers)
        {
            if (wrapper is FolderWrapper folder)
            {
                AssignSubTitlesToAudioWrappers(folder.Datas);
            }
            else if (wrapper is AudioWrapper audio)
            {
                audio.SubTitles = GetSubTitle(wrappers);
            }
        }
    }

    public IAudioDataWrapper GetAudio(Child child, RidDetily work)
    {
        return new AudioWrapper()
        {
            DownloadPath = child.MediaDownloadUrl,
            Duration = (double)child.Duration,
            FileName = child.Title,
            MediaStreamUrl = child.MediaStreamUrl,
            Size = (double)child.Size,
            StreamLowQualityUrl = child.StreamLowQualityUrl,
            Child = child,
            Work = work,
        };
    }

    public IAudioDataWrapper GetImage(Child child)
    {
        return new ImageWrapper()
        {
            DownloadPath = child.MediaDownloadUrl,
            FileName = child.Title,
            MediaStreamUrl = child.MediaStreamUrl,
            Size = (double)child.Size,
            Child = child,
        };
    }

    public IAudioDataWrapper GetSubtitle(Child child)
    {
        return new SubtitleWrapper()
        {
            DownloadPath = child.MediaDownloadUrl,
            FileName = child.Title,
            MediaStreamUrl = child.MediaStreamUrl,
            Size = (double)child.Size,
            Duration = (double)child.Duration,
            Child = child,
        };
    }

    public IAudioDataWrapper GetText(Child child)
    {
        return new TextWrapper()
        {
            DownloadPath = child.MediaDownloadUrl,
            FileName = child.Title,
            MediaStreamUrl = child.MediaStreamUrl,
            Size = (double)child.Size,
            Child = child,
        };
    }

    public IAudioDataWrapper FindFolder(Child rid, RidDetily work)
    {
        List<IAudioDataWrapper> wrappers = new();
        foreach (var item in rid.Children)
        {
            if (item.Type == "folder")
            {
                wrappers.Add(FindFolder(item, work));
            }
            else if (item.Type == "audio")
            {
                wrappers.Add(GetAudio(item, work));
            }
            else if (item.Type == "text")
            {
                if (item.Duration > 0)
                {
                    wrappers.Add(GetSubtitle(item));
                }
                else
                {
                    wrappers.Add(GetText(item));
                }
            }
            else if (item.Type == "image")
            {
                wrappers.Add(GetImage(item));
            }
        }
        var result = new FolderWrapper() { Name = rid.Title, Datas = wrappers.ToObservable() };
        return result;
    }
}
