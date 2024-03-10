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
		private Dictionary<string, GoldUpgradeItem> mItemDataDic = new Dictionary<string, GoldUpgradeItem>();
		private Dictionary<Button, string> mItemButtonDic = new Dictionary<Button, string>();
		
		private void Awake()
		{
			List<GoldUpgradeItem> goldItems = this.GetSystem<GoldUpgradeSystem>().Items;
			
			foreach (var item in goldItems)
			{
				GoldUpgradeItemTemplate.InstantiateWithParent(ItemRoot)
					.Self((self) =>
					{
						var itemCache = item;
						self.transform.GetComponentInChildren<Text>().text = item.Description + $"  {item.Cost}";
						self.onClick.AddListener(() =>
						{
							item.Upgrade();
							AudioKit.PlaySound("AbilityLevelUp");
						});
						
						mItemDataDic.Add(item.Key, item);
						mItemButtonDic.Add(self, item.Key);
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
				foreach (var button in ItemRoot.GetComponentsInChildren<Button>())
				{
					if (mItemButtonDic.ContainsKey(button))
					{
						string key = mItemButtonDic[button];
						if (mItemDataDic.ContainsKey(key))
						{
							if (gold >= mItemDataDic[key].Cost)
							{
								button.interactable = true;
							}
							else
							{
								button.interactable = false;
							}
						}
					}
				}
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