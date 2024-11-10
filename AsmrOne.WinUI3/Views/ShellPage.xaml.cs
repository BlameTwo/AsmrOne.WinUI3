using System;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.Views
{
    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            this.InitializeComponent();
            this.ViewModel = ProgramLife.ServiceProvider.GetService<ShellViewModel>();
            this.ViewModel.ShellNavigationService.RegisterView(this.frame);
            this.ViewModel.ShellNavigationViewService.Register(this.view);
            _progressSlider.AddHandler(
                UIElement.PointerPressedEvent,
                new PointerEventHandler(Progress_PointerPressed),
                true
            );
            _progressSlider.AddHandler(
                UIElement.PointerReleasedEvent,
                new PointerEventHandler(Progress_OnPointerReleased),
                true
            );
        }

        private void PlaybackSession_PositionChanged(MediaPlaybackSession sender, object args)
        {
            if (this.DispatcherQueue == null)
                return;
            this.DispatcherQueue.TryEnqueue(() =>
            {
                if (ViewModel.AudioPlayerService.IsDrag)
                    return;
                _progressSlider.Value = sender.Position.TotalSeconds;
                this.nowDuration.Text = sender.Position.ToString("hh\\:mm\\:ss");
            });
        }

        private void Progress_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.AudioPlayerService.IsDrag = false;
            if (ViewModel.AudioPlayerService.Element.MediaPlayer == null)
                return;
            ViewModel.AudioPlayerService.Element.MediaPlayer.Position = new TimeSpan(
                0,
                0,
                (int)(sender as Slider).Value
            );
        }

        private void Progress_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.AudioPlayerService.IsDrag = true;
        }

        private void ShellPage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ProgramLife.ServiceProvider.GetService<IDialogManager>().SetRoot(this.XamlRoot);
            this.ViewModel.AudioPlayerService.RegisterElement(this.element);

            this.ViewModel.AudioPlayerService.Element.MediaPlayer.PlaybackSession.PositionChanged +=
                PlaybackSession_PositionChanged;
        }

        public ShellViewModel ViewModel { get; }

        private void Slider_ValueChanged(
            object sender,
            Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e
        )
        {
            double newValue = (double)e.NewValue;
            double minValue = 1;
            double maxValue = 100;
            double mappedMinValue = 0;
            double mappedMaxValue = 1.0;
            double mappedValue =
                mappedMinValue
                + (mappedMaxValue - mappedMinValue) * (newValue - minValue) / (maxValue - minValue);
            ViewModel.AudioPlayerService.Element.MediaPlayer.Volume = (double)mappedValue;
        }

        private void view_DisplayModeChanged(
            NavigationView sender,
            NavigationViewDisplayModeChangedEventArgs args
        )
        {
            if (args.DisplayMode == NavigationViewDisplayMode.Minimal)
            {
                ToggleMenu.Visibility = Visibility.Visible;
            }
            else
            {
                ToggleMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            view.IsPaneOpen = !view.IsPaneOpen;
        }

        private async void converButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Resources["ShowRidBar"] as Storyboard).Begin();
            await this.ViewModel.RidPlayerViewModel.RefreshAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.Resources["CloseRidBar"] as Storyboard).Begin();
        }
    }
}
