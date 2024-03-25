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
		private ResLoader loader = ResLoader.Allocate();
		
		private void Awake()
		{
			var expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
			List<ExpUpgradeItem> expUpgradeItems = expUpgradeSystem.Items;
			
			foreach (var item in expUpgradeItems)
			{
				var itemCache = item;
				
				BtnExpUpgradeItemTemplate.InstantiateWithParent(ItemRoot)
					.Self((self) =>
					{
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
								selfCache.transform.Find("ImgIcon").GetComponent<Image>().sprite =
									loader.LoadSync<Sprite>(itemCache.IconName);
								selfCache.Show();
								
								var txtOtherKey = selfCache.transform.Find("TxtOtherKey");
								if (expUpgradeSystem.PairedKeys.TryGetValue(itemCache.Key, out var otherKey))
								{
									var pairedItem = expUpgradeSystem.keyToItems[otherKey];
									if (pairedItem.CurrentLevel.Value > 0 && itemCache.CurrentLevel.Value == 0) // 如果遇到现有技能的未解锁的配对技能
									{
										txtOtherKey.Find("ImgOtherIcon").GetComponent<Image>().sprite =
											loader.LoadSync<Sprite>(pairedItem.IconName); // Todo: 打包图集优化
										txtOtherKey.Show();
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

		protected override void OnBeforeDestroy()
		{
			loader.Recycle2Cache();
			loader = null;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}