using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IWindowFactory
    {
        UniTask<TWindow> InstantiateAsync<TWindow>(string windowId, Transform root);
        void DestroyWindow(IClosedWindow window);
    }
}