using System.Collections.Generic;
using System.Threading.Tasks;
namespace Wallet.Services
{
    public interface INavigationService
    {
        Task NavigateAsync(string key, IDictionary<string, string> parameters = null);
    }
}
