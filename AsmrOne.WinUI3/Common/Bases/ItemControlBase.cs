using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Common.Bases
{
    public abstract class ItemControlBase : Control
    {
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(object),
            typeof(ItemControlBase),
            new PropertyMetadata(default)
        );

        public object Paramter { get; private set; }

        public ICommand AddDataCommand { get; set; }

        //public ICommand AddDataCommand
        //{
        //    get { return (ICommand)GetValue(AddDataCommandProperty); }
        //    set { SetValue(AddDataCommandProperty, value); }
        //}

        //public static readonly DependencyProperty AddDataCommandProperty =
        //    DependencyProperty.Register(
        //        "AddDataCommand",
        //        typeof(ICommand),
        //        typeof(ItemControlBase),
        //        new PropertyMetadata(default(ICommand))
        //    );

        protected override void OnApplyTemplate()
        {
            this.Viewer = (ScrollView)GetTemplateChild("PART_ScrollView");
            Viewer.ViewChanged += _viewer_ViewChanged;
            base.OnApplyTemplate();
        }

        /// <summary>
        /// 滚动到顶部
        /// </summary>
        public void GoScrollTop()
        {
            _viewer.ScrollTo(_viewer.HorizontalOffset, 0);
        }

        /// <summary>
        /// 滚动到底部
        /// </summary>
        public void GoScrollBootom()
        {
            _viewer.ScrollTo(_viewer.HorizontalOffset, _viewer.ExtentHeight);
        }

        private async void _viewer_ViewChanged(ScrollView sender, object args)
        {
            double verticalOffset = _viewer.VerticalOffset;
            Viewer.ViewChanged -= _viewer_ViewChanged;
            if (sender.VerticalOffset > 10)
                Flage = true;
            else
                Flage = false;
            if (IsRuning)
                return;
            var flage = sender.VerticalOffset + sender.ViewportHeight;
            if (sender.ExtentHeight - flage < 5 && sender.ViewportHeight != 0)
            {
                if (AddDataCommand is AsyncRelayCommand asynccommand)
                {
                    this.IsRuning = true;
                    asynccommand.Execute(parameter: this.Paramter);
                    this.IsRuning = false;
                }
                await Task.Delay(1000);
            }
            Viewer.ViewChanged += _viewer_ViewChanged;
        }

        private ScrollView _viewer;

        public ScrollView Viewer
        {
            get => _viewer;
            set { _viewer = value; }
        }

        public bool IsRuning { get; private set; }

        public abstract void ChangedViewer();

        bool Flage = false;
    }
}
