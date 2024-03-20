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
			var items = expUpgradeSystem.Items.Where(item => item.CurrentLevel.Value > 0 && !item.UpgradeFinished).ToList();
			
			if (items.Count > 0)
			{
				var item = items.GetRandomItem();
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

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}