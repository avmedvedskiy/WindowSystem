using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{

    [CreateAssetMenu(fileName = NAME, menuName = Constants.CREATE_ASSET_MENU + NAME)]
	public class PopupDatabase : BasePopupDatabase
	{
		public const string NAME = "PopupDatabase";
		public List<ReferenceWindow> popups;

		public override T GetWindow<T>(string name, out int index)
		{
			index = popups.FindIndex(x => x != null && x.name == name);
			if (index != -1)
				return popups[index].GetSource() as T;
			else
				return null;
		}

		//public override void AddAdditionalWindows(List<ReferenceWindow> additionalWindows)
		//{
		//	popups.AddRange(additionalWindows);
		//}

#if UNITY_EDITOR

		public static void Create(UIBaseWindow window)
		{
			var so = CreateInstance<DirectReferenceWindow>();
			so.asset = window;
			UnityEditor.AssetDatabase.CreateAsset(so, string.Format("Assets/Resources/UIReferences/{0}.asset", window.nameId));
		}
#endif

	}
}
