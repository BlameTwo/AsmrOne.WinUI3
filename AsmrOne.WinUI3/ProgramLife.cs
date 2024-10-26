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
                .AddSingleton<IAppSetup<App>, AppSetup<App>>()
                .AddTransient<IPageService, PageService>()
                .AddSingleton<IDialogManager, DialogManager>()
                .AddSingleton<IAsmrClient, AsmrClient>()
                .AddTransient<IDataFactory, DataFactory>()
                .AddTransient<IDataAdaptiveService, DataAdaptiveService>()
                .AddSingleton<IAudioPlayerService, AudioPlayerService>()
                .AddSingleton<ISubtitleService, SubtitleService>()
                #endregion
                #region View And ViewModel
                .AddSingleton<ShellPage>()
                .AddSingleton<ShellViewModel>()
                .AddTransient<SubtitleWindow>()
                .AddTransient<SubtitleViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<RidDetilyViewModel>()
                .AddTransient<SettingViewModel>()
                .AddTransient<FavouritesViewModel>()
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

        public static T GetService<T>()
        {
            if (ServiceProvider.GetRequiredService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} 不存在于注册主机内。");
            }
            return service;
        }
    }
}
