/****************************************************************************
 * 2024.4 LAPTOP-FG35BCEI
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
		[SerializeField] public RectTransform ItemRoot;
		[SerializeField] public UnityEngine.UI.Button GoldUpgradeItemTemplate;

		public void Clear()
		{
			TxtGold = null;
			BtnClose = null;
			ItemRoot = null;
			GoldUpgradeItemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "GlodUpgradePanel";}
		}
	}
}
