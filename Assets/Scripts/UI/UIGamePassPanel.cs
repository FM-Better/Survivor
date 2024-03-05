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
			
			// 监听按下空格 重新开始游戏
			ActionKit.OnUpdate.Register(() =>
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					SceneManager.LoadScene("Game");
					this.CloseSelf();
				}
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
