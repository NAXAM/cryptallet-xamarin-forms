namespace Wallet.Core
{
    public abstract class ViewModelBase : ModelBase
    {
        bool _ShouldShowNavigationBar = true;
        public bool ShouldShowNavigationBar
        {
            get => _ShouldShowNavigationBar;
            set => SetProperty(ref _ShouldShowNavigationBar, value);
        }

        public virtual void Load(object data) { }
        public virtual void Unload(object data) { }
    }
}