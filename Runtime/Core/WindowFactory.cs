using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UISystem
{
    public class WindowFactory : MonoBehaviour, IWindowFactory
    {
        public async UniTask<TWindow> InstantiateAsync<TWindow>(string windowId, Transform root)
        {
            var go = await Addressables.InstantiateAsync(windowId, root);
            return go.GetComponent<TWindow>();
        } 

        public void DestroyWindow(IClosedWindow window)
            => Addressables.ReleaseInstance(window.gameObject);
        
        /*
         *
         var go = (await Addressables
                .LoadAssetsAsync<GameObject>(
                    new List<string> { windowId, skin },
                    null,
                    Addressables.MergeMode.Intersection)
                .ToUniTask())
            .FirstOrDefault();
         * 
         */
    }
}