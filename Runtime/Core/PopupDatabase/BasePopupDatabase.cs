using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public abstract class BasePopupDatabase : ScriptableObject
	{
		public abstract T GetWindow<T>(string name, out int index) where T : UIBaseWindow;
	}
}
