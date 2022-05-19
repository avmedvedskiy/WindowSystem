#if UISYSTEM_ADDRESSABLES
using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IAsyncPopupBase
    {
        UniTask<(T, int)> GetWindowAsync<T>(string windowName) where T : UIBaseWindow;
    }
}

#endif