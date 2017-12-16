using System;

using Xamarin.Forms;
using Prism.Navigation;
using Prism.Behaviors;
using Prism.Common;
using Prism.Ioc;
using Prism.Logging;
using Wallet.Core;

namespace Wallet.Forms.Bootstraps.Services
{
    public class CustomNavigationService : PageNavigationService
    {
        public CustomNavigationService(IContainerExtension container, IApplicationProvider applicationProvider, IPageBehaviorFactory pageBehaviorFactory, ILoggerFacade logger) : base(container, applicationProvider, pageBehaviorFactory, logger)
        {
        }

        protected async override System.Threading.Tasks.Task DoPush(Page currentPage, Page page, bool? useModalNavigation, bool animated, bool insertBeforeLast = false, int navigationOffset = 0)
        {
            if (currentPage?.BindingContext is ViewModelBase vm1)
            {
                vm1.Unload(null);
            }

            await base.DoPush(currentPage, page, useModalNavigation, animated, insertBeforeLast, navigationOffset);

            if (page?.BindingContext is ViewModelBase vm2)
            {
                vm2.Load(null);
            }
        }


        protected async override System.Threading.Tasks.Task<bool> GoBackInternal(NavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            var result = await base.GoBackInternal(parameters, useModalNavigation, animated);

            if (result)
            {
                var currentPage = GetCurrentPage();

                if (currentPage?.BindingContext is ViewModelBase vm2)
                {
                    vm2.Load(null);
                }
            }

            return result;
        }

        protected async override System.Threading.Tasks.Task<Page> DoPop(INavigation navigation, bool useModalNavigation, bool animated)
        {
            var page = await base.DoPop(navigation, useModalNavigation, animated);

            if (page.BindingContext is ViewModelBase vm1)
            {
                vm1.Unload(null);
            }

            return page;
        }
    }
}

