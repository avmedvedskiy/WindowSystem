using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UISystem
{
    [CreateAssetMenu(fileName = NAME, menuName = "Scriptable Objects/UI/" + NAME)]
    public class PopupDatabase : ScriptableObject
    {
        private const string NAME = "PopupDatabase";

        [System.Serializable]
        public class AssetReferenceWindow
        {
            [SerializeField] private string _id;
            [SerializeField] private ComponentReference<UIBaseWindow> _window;

            public string Id
            {
                get => _id;
                set => _id = value;
            }

            public ComponentReference<UIBaseWindow> Window => _window;
        }

        [SerializeField] private List<AssetReferenceWindow> _popups;

        public (T, int) GetWindow<T>(string windowName) where T : UIBaseWindow
        {
            var index = GetIndex(windowName);
            var asset = _popups[index];
            var w = asset.Window.LoadAssetAsync().WaitForCompletion();
            return (w as T, index);
        }

        public async UniTask<(T, int)> GetWindowAsync<T>(string windowName) where T : UIBaseWindow
        {
            int index = GetIndex(windowName);
            var asset = _popups[index];
            var w = await asset.Window.LoadAssetAsync();
            return (w as T, index);
        }

        private int GetIndex(string windowName) => _popups.FindIndex(x => x.Id == windowName);

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var p in _popups)
                p.Id = p.Window.editorAsset?.name;
        }
#endif
    }
}