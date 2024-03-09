using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace Survivor
{
	public class UIGameStartPanelData : UIPanelData
	{
	}
	public partial class UIGameStartPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();
			
			Global.InitGameData();

			#region UI相关
			BtnUpgrade.onClick.AddListener(() =>
			{
				GlodUpgradePanel.Show();
			});
			
			BtnStart.onClick.AddListener(() =>
			{
				SceneManager.LoadScene("Game");
			});
			
			BtnClose.onClick.AddListener(() =>
			{
				GlodUpgradePanel.Hide();
			});
			
			BtnExpUpgrade.onClick.AddListener(() =>
			{
				Global.ExpBallDropRate.Value += 1;
				Global.Gold.Value -= 5;
				AudioKit.PlaySound("AbilityLevelUp");
			});
			
			BtnGoldUpgrade.onClick.AddListener(() =>
			{
				Global.GoldDropRate.Value += 1;
				Global.Gold.Value -= 5;
				AudioKit.PlaySound("AbilityLevelUp");
			});
			#endregion

			#region Global相关
			Global.Gold.RegisterWithInitValue((gold) =>
			{
				TxtGold.text = "金币：" + gold;

				if (gold >= 5)
				{
					BtnExpUpgrade.Show();
					BtnGoldUpgrade.Show();
				}
				else
				{
					BtnExpUpgrade.Hide();
					BtnGoldUpgrade.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.Gold.Register((gold) =>
			{
				PlayerPrefs.SetInt("Gold", gold);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			#endregion
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
