namespace Wallet.Core
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T property, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (Equals(property, newValue)) return;

            property = newValue;

            RaisePropertyChanged(propertyName);
        }
    }
}

