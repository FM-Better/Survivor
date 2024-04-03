/****************************************************************************
 * 2024.4 LAPTOP-FG35BCEI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	public partial class AchievementPanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public RectTransform ItemRoot;
		[SerializeField] public UnityEngine.UI.Image AchievementItemTemplate;

		public void Clear()
		{
			BtnClose = null;
			ItemRoot = null;
			AchievementItemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "AchievementPanel";}
		}
	}
}
