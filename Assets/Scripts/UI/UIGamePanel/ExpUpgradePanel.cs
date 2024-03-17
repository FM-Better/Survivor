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
			List<ExpUpgradeItem> expUpgradeItems = this.GetSystem<ExpUpgradeSystem>().Items;
			
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
							}
							else
							{
								selfCache.Hide();
							}
						});

						itemCache.CurrentLevel.RegisterWithInitValue(_ =>
						{
							selfCache.transform.GetComponentInChildren<Text>().text = 
								itemCache.Description;	
						}).UnRegisterWhenGameObjectDestroyed(gameObject);
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