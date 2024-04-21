/****************************************************************************
 * 2024.4 LAPTOP-FG35BCEI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	public partial class ExpUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnExpUpgradeItemTemplate;
		[SerializeField] public RectTransform ItemRoot;

		public void Clear()
		{
			BtnExpUpgradeItemTemplate = null;
			ItemRoot = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
