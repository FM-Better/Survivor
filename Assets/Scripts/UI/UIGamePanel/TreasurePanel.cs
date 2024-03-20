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
					var isInCombine = expUpgradeSystem.Combines.ContainsKey(item.Key);
					if (isInCombine)
					{ 
						var superIsUnlocked = expUpgradeSystem.keyToSuperIsAlive[item.Key].Value;
						if (!superIsUnlocked)
						{
							var otherItemKey = expUpgradeSystem.Combines[item.Key];
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
				TxtContent.text = $"<b>合成后的{item.Key}</b>\n";
				while (!item.UpgradeFinished)
				{
					item.Upgrade();
				}
				
				expUpgradeSystem.keyToSuperIsAlive[item.Key].Value = true;
			}
			else
			{
				var items = expUpgradeSystem.Items.Where(item => item.CurrentLevel.Value > 0 && !item.UpgradeFinished);
			
				if (items.Any())
				{
					var item = items.ToList().GetRandomItem();
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
							return;
						}	
					}
				
					TxtContent.text = "增加50金币";
					Global.Gold.Value += 50;
				}	
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}