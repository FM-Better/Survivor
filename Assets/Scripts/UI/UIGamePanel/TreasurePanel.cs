/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/

using System.Linq;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class TreasurePanel : UIElement, IController
	{
		private ResLoader mLoader = ResLoader.Allocate();
		
		private void Awake()
		{
				BtnSure.onClick.AddListener(() =>
				{
					Time.timeScale = 1f;
					this.Hide();
				});
		}

		private void OnEnable()
		{
			var expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
			// 检测是否有可合成的超级武器
			var canCombineItems = expUpgradeSystem.Items.Where(item =>
			{
				if (item.CurrentLevel.Value >= 7)
				{
					var isInCombine = expUpgradeSystem.PairedKeys.ContainsKey(item.Key);
					if (isInCombine)
					{ 
						var superIsUnlocked = expUpgradeSystem.keyToSuperIsAlive[item.Key].Value;
						if (!superIsUnlocked)
						{
							var otherItemKey = expUpgradeSystem.PairedKeys[item.Key];
							var otherIsUnlocked = expUpgradeSystem.keyToItems[otherItemKey].CurrentLevel.Value > 0;
							if (otherIsUnlocked)
							{
								return true;
							}
						}
					}
				}

				return false;
			});

			if (canCombineItems.Any())
			{
				var item = canCombineItems.ToList().GetRandomItem();
				while (!item.UpgradeFinished)
				{
					item.Upgrade();
				}

				ImgIcon.sprite = mLoader.LoadSync<Sprite>(item.PairedIconName);
				ImgIcon.Show();
				TxtContent.text = $"<b>{item.PairedName}</b>\n{item.PairedDescription}";
				
				expUpgradeSystem.keyToSuperIsAlive[item.Key].Value = true;
			}
			else
			{
				var items = expUpgradeSystem.Items.Where(item => item.CurrentLevel.Value > 0 && !item.UpgradeFinished);
			
				if (items.Any())
				{
					var item = items.ToList().GetRandomItem();
					ImgIcon.sprite = mLoader.LoadSync<Sprite>(item.IconName);
					ImgIcon.Show();
					TxtContent.text = item.Description;
					item.Upgrade();
				}
				else
				{
					if (Global.Hp.Value < Global.MaxHp.Value)
					{
						if (Random.Range(0, 100) < 20)
						{
							Global.Hp.Value++;
							TxtContent.text = "恢复1点血量";
							AudioKit.PlaySound(Sound.HPITEM);
							ImgIcon.Hide();
							return;
						}	
					}
				
					TxtContent.text = "增加50金币";
					Global.Gold.Value += 50;
					ImgIcon.Hide();
				}	
			}
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