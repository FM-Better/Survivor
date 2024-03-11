/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	public partial class GlodUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Text TxtGold;
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public UnityEngine.UI.Button BtnExpUpgrade;
		[SerializeField] public UnityEngine.UI.Button BtnGoldUpgrade;
		[SerializeField] public UnityEngine.UI.Button BtnMaxHpUpgrade;
		[SerializeField] public RectTransform ItemRoot;
		[SerializeField] public UnityEngine.UI.Button GoldUpgradeItemTemplate;

		public void Clear()
		{
			TxtGold = null;
			BtnClose = null;
			BtnExpUpgrade = null;
			BtnGoldUpgrade = null;
			BtnMaxHpUpgrade = null;
			ItemRoot = null;
			GoldUpgradeItemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "GlodUpgradePanel";}
		}
	}
}
