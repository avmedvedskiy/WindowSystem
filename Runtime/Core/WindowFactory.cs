using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UISystem
{
    public class WindowFactory : IWindowFactory
    {
        private readonly WindowFactoryConfig _config;
        
        public WindowFactory(WindowFactoryConfig config)
        {
            _config = config;
        }
        
        public async UniTask<TWindow> InstantiateAsync<TWindow>(string windowId, Transform root)
        {
            if(_config.BuildInWindows.Contains(windowId))
                return await InstantiateFromResources<TWindow>(windowId, root);
            return await InstantiateFromAddressable<TWindow>(windowId, root);
        }

        public void DestroyWindow(IClosedWindow window)
        {
            var result = Addressables.ReleaseInstance(window.gameObject);
            if (result == false)
            {
                Object.Destroy(window.gameObject);
            }
        }

        private async UniTask<TWindow> InstantiateFromResources<TWindow>(string windowId, Transform root)
        {
            var folder = string.IsNullOrEmpty(_config.FolderName) ? string.Empty : $"{_config.FolderName}/";
            var asset = await Resources.LoadAsync<GameObject>($"{folder}{windowId}").ToUniTask();
            var go = (GameObject)Object.Instantiate(asset,root);
            return go.GetComponent<TWindow>();
        }

        private async UniTask<TWindow> InstantiateFromAddressable<TWindow>(string windowId, Transform root)
        {
            var go = await Addressables
                .InstantiateAsync(windowId, root)
                .ToUniTask();
            return go.GetComponent<TWindow>();
        }
        
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