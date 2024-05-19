/****************************************************************************
 * 2024.4 LAPTOP-FG35BCEI
 ****************************************************************************/

using System.Linq;
using QAssetBundle;
using QFramework;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Survivor
{
	public partial class AchievementPanel : UIElement, IController
	{
		private ResLoader mLoader = ResLoader.Allocate();
		private SpriteAtlas gameAtlas = null;
		
		private void Awake()
		{
			var items = this.GetSystem<AchievementSystem>().Items;
			gameAtlas = mLoader.LoadSync<SpriteAtlas>("Game");
			
			foreach (var item in items.OrderByDescending(item => item.Unlocked))
			{
				AchievementItemTemplate.InstantiateWithParent(ItemRoot)
					.Self(template =>
						{
							template.GetComponentInChildren<Text>().text =
								$"<b>{item.Name}{(item.Unlocked ? "<color=green>[已完成]</color>" : "")}</b>\n{item.Description}";
							template.transform.Find("Icon").GetComponent<Image>().sprite = 
								gameAtlas.GetSprite(item.IconName);
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
			mLoader.Recycle2Cache();
			mLoader = null;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}