namespace Wallet.Core
{
    public abstract class ViewModelBase : ModelBase
    {
        public virtual void Load(object data) { }
        public virtual void Unload(object data) { }
    }
}