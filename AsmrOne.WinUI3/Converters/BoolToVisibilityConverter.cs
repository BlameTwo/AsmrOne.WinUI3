using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace AsmrOne.WinUI3.Converters
{
    public partial class BoolToVisibilityConverter : IValueConverter
    {
        public bool Reverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool result)
            {
                if (Reverse)
                {
                    if (result)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    if (result)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
