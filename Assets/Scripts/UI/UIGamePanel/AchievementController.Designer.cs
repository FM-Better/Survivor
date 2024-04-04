/****************************************************************************
 * 2024.4 LAPTOP-FG35BCEI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	public partial class AchievementController
	{
		[SerializeField] public UnityEngine.UI.Image AchievementItem;
		[SerializeField] public UnityEngine.UI.Text TxtDescription;
		[SerializeField] public UnityEngine.UI.Text TxtTitle;
		[SerializeField] public UnityEngine.UI.Image ImgIcon;

		public void Clear()
		{
			AchievementItem = null;
			TxtDescription = null;
			TxtTitle = null;
			ImgIcon = null;
		}

		public override string ComponentName
		{
			get { return "AchievementController";}
		}
	}
}
