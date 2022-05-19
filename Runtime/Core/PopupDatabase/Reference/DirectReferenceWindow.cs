using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
	[CreateAssetMenu(menuName = Constants.CREATE_ASSET_MENU + "DirectReferenceWindow")]
	public class DirectReferenceWindow : ReferenceWindow
	{
		public UIBaseWindow asset;

		public override UIBaseWindow GetSource()
		{
			return asset;
		}
	}
}
