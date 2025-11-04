using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;


namespace AsmrOne.WinUI3.Views
{
    public sealed partial class DownloadPage : Page,IPage
    {
        public DownloadPage()
        {
            InitializeComponent();
            this.ViewModel = ProgramLife.GetService<DownloadViewModel>();
        }

        public Type PageType => typeof(DownloadPage);

        public DownloadViewModel ViewModel { get; }

        public void Dispose()
        {

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.Dispose();
            GC.Collect();
            base.OnNavigatedFrom(e);
        }
    }
}
