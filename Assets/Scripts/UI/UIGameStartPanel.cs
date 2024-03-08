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

			Global.Gold.RegisterWithInitValue((gold) =>
			{
				TxtGold.text = "金币：" + gold;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.Gold.Register((gold) =>
			{
				PlayerPrefs.SetInt("Gold", gold);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
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
