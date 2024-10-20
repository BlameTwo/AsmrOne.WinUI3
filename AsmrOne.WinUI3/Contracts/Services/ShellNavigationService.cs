using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Contracts.Services.Navigation;

namespace AsmrOne.WinUI3.Contracts.Services
{
    public class ShellNavigationService : NavigationServiceBase
    {
        public ShellNavigationService(IPageService pageService)
            : base(pageService) { }
    }
}
