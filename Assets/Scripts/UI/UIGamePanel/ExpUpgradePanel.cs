/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/
using System.Collections.Generic;
using QAssetBundle;
using QFramework;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Survivor
{
	public partial class ExpUpgradePanel : UIElement, IController
	{
		private ResLoader mLoader = ResLoader.Allocate();
		private SpriteAtlas gameAtlas = null;
		
		private void Awake()
		{
			var expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
			List<ExpUpgradeItem> expUpgradeItems = expUpgradeSystem.Items;
			gameAtlas = mLoader.LoadSync<SpriteAtlas>("Game");
			
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
									gameAtlas.GetSprite(itemCache.IconName);
								selfCache.Show();
								
								var txtOtherKey = selfCache.transform.Find("TxtOtherKey");
								if (expUpgradeSystem.PairedKeys.TryGetValue(itemCache.Key, out var otherKey))
								{
									var pairedItem = expUpgradeSystem.keyToItems[otherKey];
									if (pairedItem.CurrentLevel.Value > 0 && itemCache.CurrentLevel.Value == 0) // 如果遇到现有技能的未解锁的配对技能
									{
										txtOtherKey.Find("ImgOtherIcon").GetComponent<Image>().sprite =
											gameAtlas.GetSprite(pairedItem.IconName);
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
			gameAtlas = null;
			mLoader.Recycle2Cache();
			mLoader = null;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}