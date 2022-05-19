#if UISYSTEM_ADDRESSABLES
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = NAME, menuName = Constants.CREATE_ASSET_MENU + NAME)]
    public class AddresablePopupDatabase : BasePopupDatabase,IAsyncPopupBase
	{
        [System.Serializable]
        public class AddressableReferenceWindow : ComponentReference<UIBaseWindow>
        {
            public AddressableReferenceWindow(string guid) : base(guid) { }
        }

        [System.Serializable]
        public class AssetReferenceWindow
        {
            public string id;
            public AddressableReferenceWindow window;
        }

        public const string NAME = "AddresablePopupDatabase";
		public List<AssetReferenceWindow> popups;
        
        public override T GetWindow<T>(string windowName, out int index)
        {
            index = GetIndex(windowName);
            var asset = popups[index];
            return asset.window.LoadAssetAsync().WaitForCompletion() as T;
        }

        public async UniTask<(T,int)> GetWindowAsync<T>(string windowName) where T: UIBaseWindow
        {
            int index = GetIndex(windowName);
            var asset = popups[index];
            T w = await asset.window.LoadAssetAsync() as T;
            return (w,index);
        }

        private int GetIndex(string windowName) => popups.FindIndex(x => x.id == windowName);
        
#if UNITY_EDITOR
        [ContextMenu("FillID")]
        private void FillId()
        {
            foreach (var p in popups)
                p.id = p.window.editorAsset.name;
        }
#endif
    }
}
#endif
