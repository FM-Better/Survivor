/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/

using System.Collections.Generic;
using System.Linq;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace Survivor
{
	public partial class GlodUpgradePanel : UIElement,IController
	{
		private List<GoldUpgradeItem> goldItems = new List<GoldUpgradeItem>();

		
		
		private void Refresh()
		{
			ItemRoot.DestroyChildren();

			var items = goldItems.Where(item => item.ConditionCheck());
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
							AudioKit.PlaySound("AbilityLevelUp");
						});

						var selfCache = self;
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
					}).Show();
			}	
		}
		
		private void Awake()
		{
			goldItems = this.GetSystem<GoldUpgradeSystem>().Items;

			GoldUpgradeSystem.OnGoldUpgradeSystemChanged.Register(() =>
			{
				Refresh();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Refresh();
			
			#region UI相关
			BtnClose.onClick.AddListener(() =>
			{
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