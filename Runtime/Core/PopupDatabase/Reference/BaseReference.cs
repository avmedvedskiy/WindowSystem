using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
	//[CreateAssetMenu(menuName = )]
	public abstract class BaseReference<T> : ScriptableObject where T : Object
	{
		public abstract T GetSource();
	}

	public abstract class ReferenceWindow : BaseReference<UIBaseWindow>
	{

#if UNITY_EDITOR && !UISYSTEM_ADDRESSABLES
		[ContextMenu("Open")]
		public void Open()
		{
			if (UIMainController.Instance != null)
				UIMainController.Instance.GetWindow(GetSource().nameId).OpenWindow();
		}
		[ContextMenu("OpenInQueue")]
        public void OpenInQueu()
		{
			if (UIMainController.Instance != null)
			    UIMainController.Instance.GetWindow(GetSource().nameId).OpenWindowInQueue();
		}
#endif
	}

	public class Constants
	{
		public const string CREATE_ASSET_MENU = "Scriptable Objects/References/";
		public const string WINDOW_CONTEXT = "CONTEXT/UIGameWindow/Create Direct References";
	}
}