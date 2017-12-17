using Prism.Navigation;
namespace Wallet.Core
{
    public abstract class ViewModelBase : ModelBase, INavigationAware
    {
        bool _ShouldShowNavigationBar = true;
        public bool ShouldShowNavigationBar
        {
            get => _ShouldShowNavigationBar;
            set => SetProperty(ref _ShouldShowNavigationBar, value);
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}