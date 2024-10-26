using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace AsmrOne.WinUI3.Converters
{
    public partial class BoolToBrushConverter : DependencyObject, IValueConverter
    {
        public Brush TrueColor
        {
            get { return (Brush)GetValue(TrueColorProperty); }
            set { SetValue(TrueColorProperty, value); }
        }

        public static readonly DependencyProperty TrueColorProperty = DependencyProperty.Register(
            "TrueColor",
            typeof(Brush),
            typeof(BoolToBrushConverter),
            new PropertyMetadata(null)
        );

        public Brush FalseColor
        {
            get { return (Brush)GetValue(FalseColorProperty); }
            set { SetValue(FalseColorProperty, value); }
        }

        public static readonly DependencyProperty FalseColorProperty = DependencyProperty.Register(
            "FalseColor",
            typeof(Brush),
            typeof(BoolToBrushConverter),
            new PropertyMetadata(null)
        );

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool result)
            {
                if (result)
                    return TrueColor;
                else
                {
                    return FalseColor;
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
