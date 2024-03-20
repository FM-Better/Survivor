/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	public partial class TreasurePanel
	{
		[SerializeField] public UnityEngine.UI.Text TxtContent;
		[SerializeField] public UnityEngine.UI.Button BtnSure;

		public void Clear()
		{
			TxtContent = null;
			BtnSure = null;
		}

		public override string ComponentName
		{
			get { return "TreasurePanel";}
		}
	}
}
