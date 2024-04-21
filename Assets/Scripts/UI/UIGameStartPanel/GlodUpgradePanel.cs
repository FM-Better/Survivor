/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/
using System.Collections.Generic;
using System.Linq;
using QAssetBundle;
using QFramework;
using UnityEngine.UI;

namespace Survivor
{
	public partial class GlodUpgradePanel : UIElement,IController
	{
		private List<GoldUpgradeItem> goldItems = new List<GoldUpgradeItem>();
		
		private void Awake()
		{
			goldItems = this.GetSystem<GoldUpgradeSystem>().Items;
			
			var items = goldItems.Where(item => !item.UpgradeFinished);
			foreach (var item in items)
			{
				GoldUpgradeItemTemplate.InstantiateWithParent(ItemRoot)
					.Self((self) =>
					{
						var itemCache = item;
						self.transform.GetComponentInChildren<Text>().text = 
							itemCache.Description + $"  {itemCache.Cost}金币";
						self.onClick.AddListener(() =>
						{
							itemCache.Upgrade();
							AudioKit.PlaySound(Sound.ABILITYLEVELUP);
						});
						
						var selfCache = self;
						itemCache.OnChanged.Register(() =>
						{
							if (itemCache.ConditionCheck())
							{
								selfCache.Show();
							}
							else
							{
								selfCache.Hide();
							}
						}).UnRegisterWhenGameObjectDestroyed(selfCache);
						
						if (itemCache.ConditionCheck())
						{
							selfCache.Show();
						}
						else
						{
							selfCache.Hide();
						}
						
						Global.Gold.RegisterWithInitValue((gold) =>
						{
							if (gold >= item.Cost)
							{
								selfCache.interactable = true;
							}
							else
							{
								selfCache.interactable = false;
							}
						}).UnRegisterWhenGameObjectDestroyed(self);
					});
			}	
			
			#region UI相关
			BtnClose.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sound.BUTTONCLICK);
				this.Hide();
			});
			#endregion

			#region Global相关
			Global.Gold.RegisterWithInitValue((gold) =>
			{
				TxtGold.text = "金币：" + gold;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			#endregion
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}