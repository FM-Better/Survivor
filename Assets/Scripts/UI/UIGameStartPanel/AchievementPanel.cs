/****************************************************************************
 * 2024.4 LAPTOP-FG35BCEI
 ****************************************************************************/

using System.Linq;
using QAssetBundle;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor
{
	public partial class AchievementPanel : UIElement, IController
	{
		private ResLoader mloader = ResLoader.Allocate();
		
		private void Awake()
		{
			var items = this.GetSystem<AchievementSystem>().Items;
			
			foreach (var item in items.OrderByDescending(item => item.Unlocked))
			{
				AchievementItemTemplate.InstantiateWithParent(ItemRoot)
					.Self(template =>
						{
							template.GetComponentInChildren<Text>().text =
								$"<b>{item.Name}{(item.Unlocked ? "<color=green>[已完成]</color>" : "")}</b>\n{item.Description}";
							var sprite = mloader.LoadSync<Sprite>(item.IconName);
							template.transform.Find("Icon").GetComponent<Image>().sprite = sprite;
						})
					.Show();
			}
			
			BtnClose.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sound.BUTTONCLICK);
				this.Hide();
			});
		}

		protected override void OnBeforeDestroy()
		{
			mloader.Recycle2Cache();
			mloader = null;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}