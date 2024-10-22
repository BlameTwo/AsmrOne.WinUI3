using System;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace AsmrOne.WinUI3.Controls
{
    [TemplatePart(Name = "PRPA_Image", Type = typeof(Image))]
    [TemplatePart(Name = "border", Type = typeof(Border))]
    [TemplateVisualState(GroupName = "CommonStates", Name = "HideStates")]
    [TemplateVisualState(GroupName = "CommonStates", Name = "ShowStates")]
    public partial class ImageControl : Control
    {
        private Image _imageBase;
        private Border _border;

        public bool? IsHideCover
        {
            get => (bool?)GetValue(IsHideCoverProperty);
            set => SetValue(IsHideCoverProperty, value);
        }

        public static readonly DependencyProperty IsHideCoverProperty = DependencyProperty.Register(
            "IsHideCover",
            typeof(bool?),
            typeof(ImageControl),
            new PropertyMetadata(false, OnIsHideCoverChanged)
        );

        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source",
            typeof(ImageSource),
            typeof(ImageControl),
            new PropertyMetadata(null)
        );

        public Stretch Stretch
        {
            get => (Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
            "Stretch",
            typeof(Stretch),
            typeof(ImageControl),
            new PropertyMetadata(Stretch.Uniform)
        );

        private static void OnIsHideCoverChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is ImageControl control)
            {
                Debug.WriteLine($"IsHideCover changed from {e.OldValue} to {e.NewValue}.");
                control.Update();
            }
        }

        private void Update()
        {
            var state = IsHideCover == true ? "HideStates" : "ShowStates";
            Debug.WriteLine($"Going to state: {state}");

            if (this.IsLoaded)
            {
                bool result = VisualStateManager.GoToState(this, state, true);
                Debug.WriteLine($"GoToState result: {result}");
            }
            else
            {
                Debug.WriteLine("Control is not loaded yet.");
                this.Loaded += (sender, args) => Update();
            }
        }

        protected override void OnApplyTemplate()
        {
            _imageBase = GetTemplateChild("PRPA_Image") as Image;
            _border = GetTemplateChild("border") as Border;

            if (_imageBase == null)
            {
                Debug.WriteLine("PRPA_Image not found in template.");
            }
            else
            {
                Debug.WriteLine("PRPA_Image found.");
            }

            if (_border == null)
            {
                Debug.WriteLine("border not found in template.");
            }
            else
            {
                Debug.WriteLine("border found.");
            }

            base.OnApplyTemplate();

            // 初始状态设置
            Update();
        }

        public ImageControl()
        {
            this.DefaultStyleKey = typeof(ImageControl);
            this.Loaded += (sender, args) => Update();
        }
    }
}
