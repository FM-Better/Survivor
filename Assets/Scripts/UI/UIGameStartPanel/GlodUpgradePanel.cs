/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/

using System.Collections.Generic;
using QFramework;
using UnityEngine.UI;

namespace Survivor
{
	public partial class GlodUpgradePanel : UIElement,IController
	{
		private void Awake()
		{
			List<GoldUpgradeItem> goldItems = this.GetSystem<GoldUpgradeSystem>().Items;
			
			foreach (var item in goldItems)
			{
				GoldUpgradeItemTemplate.InstantiateWithParent(ItemRoot)
					.Self((self) =>
					{
						var itemCache = item;
						self.transform.GetComponentInChildren<Text>().text = item.Description + $"  {item.Cost}金币";
						self.onClick.AddListener(() =>
						{
							item.Upgrade();
							AudioKit.PlaySound("AbilityLevelUp");
						});
						
						Global.Gold.RegisterWithInitValue((gold) =>
						{
							if (gold >= item.Cost)
							{
								self.interactable = true;
							}
							else
							{
								self.interactable = false;
							}
						}).UnRegisterWhenGameObjectDestroyed(self);
					}).Show();
			}
			
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