using System;
using Microsoft.UI.Xaml.Data;

namespace AsmrOne.WinUI3.Converters;

public partial class ProgressConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return ((TimeSpan)value).TotalSeconds;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return TimeSpan.FromSeconds((double)value);
    }
}
