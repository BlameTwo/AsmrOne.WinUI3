using AsmrOne.WinUI3.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Selector;

public partial class AudioSelector : DataTemplateSelector
{
    //protected override DataTemplate SelectTemplatei(object item, DependencyObject container)
    //{
    //    if (item is FolderWrapper)
    //    {
    //        return this.FolderTemplate;
    //    }
    //    if (item is ImageWrapper)
    //    {
    //        return this.ImageTemplate;
    //    }
    //    if (item is AudioWrapper)
    //    {
    //        return this.AudioTemplate;
    //    }
    //    if (item is TextWrapper)
    //    {
    //        return this.TextTemplate;
    //    }
    //    if (item is SubtitleWrapper)
    //    {
    //        return SubTextTemplate;
    //    }
    //    return base.SelectTemplatei(item, container);
    //}

    protected override DataTemplate SelectTemplateCore(object item)
    {
        if (item is FolderWrapper)
        {
            return this.FolderTemplate;
        }
        if (item is ImageWrapper)
        {
            return this.ImageTemplate;
        }
        if (item is AudioWrapper)
        {
            return this.AudioTemplate;
        }
        if (item is TextWrapper)
        {
            return this.TextTemplate;
        }
        if (item is SubtitleWrapper)
        {
            return SubTextTemplate;
        }
        return base.SelectTemplateCore(item);
    }

    public DataTemplate FolderTemplate { get; set; }

    public DataTemplate AudioTemplate { get; set; }

    public DataTemplate ImageTemplate { get; set; }

    public DataTemplate TextTemplate { get; set; }

    public DataTemplate SubTextTemplate { get; set; }
}
