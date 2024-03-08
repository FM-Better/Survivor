using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace Survivor
{
	public class UIGamePassPanelData : UIPanelData
	{
	}
	public partial class UIGamePassPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePassPanelData ?? new UIGamePassPanelData();
			
			BtnBackStart.onClick.AddListener(() =>
			{
				this.CloseSelf();
				SceneManager.LoadScene("GameStart");
			});
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			AudioKit.PlaySound("GamePass");
			Time.timeScale = 0f; // 防止玩家和敌人移动
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
