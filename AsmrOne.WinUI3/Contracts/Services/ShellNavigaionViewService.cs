using AsmrOne.WinUI3.Contracts.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.Contracts.Services
{
    public class ShellNavigaionViewService : NavigationViewServiceBase
    {
        public ShellNavigaionViewService(
            [FromKeyedServices(ProgramLife.ShellNavigationKey)]
                INavigationService navigationService,
            IPageService pageService
        )
            : base(navigationService, pageService) { }
    }
}
