using System;
using Microsoft.UI.Xaml.Data;

namespace AsmrOne.WinUI3.Converters;

public partial class DoubleTimeSpanToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double val)
        {
            try
            {
                return TimeSpan.FromSeconds(val).ToString(@"hh\:mm\:ss");
            }
            catch
            {
                return TimeSpan.FromMilliseconds(val).ToString(@"hh\:mm\:ss");
            }
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
