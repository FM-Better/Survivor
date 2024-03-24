/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/
using System.Collections.Generic;
using QAssetBundle;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor
{
	public partial class ExpUpgradePanel : UIElement, IController
	{
		private void Awake()
		{
			ResLoader loader = ResLoader.Allocate();
			var expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
			List<ExpUpgradeItem> expUpgradeItems = expUpgradeSystem.Items;
			
			foreach (var item in expUpgradeItems)
			{
				BtnExpUpgradeItemTemplate.InstantiateWithParent(ItemRoot)
					.Self((self) =>
					{
						var itemCache = item;
						var selfCache = self;
						
						selfCache.onClick.AddListener(() =>
						{
							itemCache.Upgrade();
							Time.timeScale = 1f;
							this.Hide();
							AudioKit.PlaySound(Sound.ABILITYLEVELUP);
						});
						
						itemCache.Visible.RegisterWithInitValue((visible) =>
						{
							if (visible)
							{
								selfCache.Show();
								selfCache.transform.Find("ImgIcon").GetComponent<Image>().sprite =
									loader.LoadSync<Sprite>(itemCache.IconName);
								
								var txtOtherKey = selfCache.transform.Find("TxtOtherKey");
								if (expUpgradeSystem.Combines.TryGetValue(itemCache.Key, out var otherKey))
								{
									var otherItem = expUpgradeSystem.keyToItems[otherKey];
									if (otherItem.CurrentLevel.Value > 0 && itemCache.CurrentLevel.Value == 0) // 如果遇到现有技能的未解锁的配对技能
									{
										txtOtherKey.Show();
										txtOtherKey.Find("ImgOtherIcon").GetComponent<Image>().sprite =
											loader.LoadSync<Sprite>(otherItem.IconName);
									}
									else
									{
										selfCache.transform.Find("TxtOtherKey").Hide();	
									}
								}
								else
								{
									selfCache.transform.Find("TxtOtherKey").Hide();
								}
							}
							else
							{
								selfCache.Hide();
							}
						});

						itemCache.CurrentLevel.RegisterWithInitValue(_ =>
						{
							selfCache.transform.Find("TxtDescription").GetComponent<Text>().text = 
								itemCache.Description;
						}).UnRegisterWhenGameObjectDestroyed(gameObject);
					});
			}	
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}