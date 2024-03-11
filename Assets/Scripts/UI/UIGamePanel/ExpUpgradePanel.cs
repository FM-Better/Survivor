/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor
{
	public partial class ExpUpgradePanel : UIElement, IController
	{
		private void Awake()
		{
			List<ExpUpgradeItem> expUpgradeItems = this.GetSystem<ExpUpgradeSystem>().Items;
			
			foreach (var item in expUpgradeItems)
			{
				BtnExpUpgradeItemTemplate.InstantiateWithParent(ItemRoot)
					.Self((self) =>
					{
						var itemCache = item;
						self.transform.GetComponentInChildren<Text>().text = 
							itemCache.Description;
						self.onClick.AddListener(() =>
						{
							itemCache.Upgrade();
							Time.timeScale = 1f;
							this.Hide();
							AudioKit.PlaySound("AbilityLevelUp");
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
					});
			}	
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