/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	public partial class BtnUpgradeItem
	{
		[SerializeField] public UnityEngine.UI.Text TxtOtherKey;

		public void Clear()
		{
			TxtOtherKey = null;
		}

		public override string ComponentName
		{
			get { return "BtnUpgradeItem";}
		}
	}
}
