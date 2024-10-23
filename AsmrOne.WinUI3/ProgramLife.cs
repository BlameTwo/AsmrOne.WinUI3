using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Contracts.Services;
using AsmrOne.WinUI3.Contracts.Services.Adaptives;
using AsmrOne.WinUI3.ViewModels;
using AsmrOne.WinUI3.ViewModels.DialogViewModels;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using AsmrOne.WinUI3.Views;
using AsmrOne.WinUI3.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3
{
    public class ProgramLife
    {
        public const string ShellNavigationKey = "ShellNavigationKey";
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void InitService()
        {
            ServiceProvider = new ServiceCollection()
                #region Navigation
                .AddKeyedSingleton<INavigationViewService, ShellNavigaionViewService>(
                    ProgramLife.ShellNavigationKey
                )
                .AddKeyedSingleton<INavigationService, ShellNavigationService>(
                    ProgramLife.ShellNavigationKey
                )
                .AddTransient<IPageService, PageService>()
                .AddSingleton<IDialogManager, DialogManager>()
                .AddSingleton<IAsmrClient, AsmrClient>()
                .AddTransient<IDataFactory, DataFactory>()
                .AddTransient<IDataAdaptiveService, DataAdaptiveService>()
                .AddSingleton<IAudioPlayerService, AudioPlayerService>()
                .AddSingleton<ISubtitleService, SubtitleService>()
                #endregion
                #region View And ViewModel
                .AddTransient<ShellPage>()
                .AddTransient<ShellViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<RidDetilyViewModel>()
                .AddTransient<SettingViewModel>()
                #endregion
                #region ItemVM
                .AddTransient<DetilyItemViewModel>()
                #endregion
                #region Dialog
                .AddTransient<RegisterDialog>()
                .AddTransient<RegisterViewModel>()
                #endregion
                .BuildServiceProvider();
        }
    }
}
