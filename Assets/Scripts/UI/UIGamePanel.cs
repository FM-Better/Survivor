using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	public class UIGamePanelData : UIPanelData
	{
	}
	public partial class UIGamePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();
			BtnUpgrade.Hide();
			
			Global.Exp.RegisterWithInitValue((exp) =>
			{
				if (exp >= 5)
				{
					Global.Exp.Value -= 5;
					Global.Lv.Value++;
				}
				TxtExp.text = "经验值：" + exp;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.Lv.RegisterWithInitValue((lv) =>
			{
				TxtLv.text = "等级：" + lv;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Lv.Register((lv) =>
			{
				Time.timeScale = 0f;
				BtnUpgrade.Show();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			BtnUpgrade.onClick.AddListener(() =>
			{
				Time.timeScale = 1f;
				BtnUpgrade.Hide();
			});
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
