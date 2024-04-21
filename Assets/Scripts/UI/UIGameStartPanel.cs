using QAssetBundle;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace Survivor
{
	public class UIGameStartPanelData : UIPanelData
	{
	}
	public partial class UIGameStartPanel : UIPanel, IController
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();

			#region UI相关
			BtnStart.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sound.BUTTONCLICK);
				SceneManager.LoadScene("Game");
			});
			
			BtnUpgrade.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sound.BUTTONCLICK);
				GlodUpgradePanel.Show();
			});
			
			BtnAchievement.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sound.BUTTONCLICK);
				AchievementPanel.Show();
			});
			#endregion

			#region Global相关
			Global.Gold.Register((gold) =>
			{
				PlayerPrefs.SetInt("Gold", gold);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.MaxHp.Register((maxHp) =>
			{
				PlayerPrefs.SetInt("MaxHp", maxHp);
			});
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

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
