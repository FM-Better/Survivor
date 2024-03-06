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
				TxtExp.text = "经验值：" + exp;
				
				if (exp >= 5)
				{
					Global.Exp.Value -= 5;
					Global.Level.Value++;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.Level.RegisterWithInitValue((lv) =>
			{
				TxtLv.text = "等级：" + lv;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Level.Register((lv) =>
			{
				Time.timeScale = 0f;
				BtnUpgrade.Show();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Timer.RegisterWithInitValue((nowTime) =>
			{
				if (Time.frameCount % 30 == 0) // 每30帧进行一次UI显示更新，避免浪费性能
				{
					var nowSeconds = Mathf.RoundToInt(nowTime);
					var mintues = nowSeconds / 60;
					var seconds = nowSeconds % 60;
					TxtTimer.text = "时间：" + $"{mintues:00}:{seconds:00}";	
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			BtnUpgrade.onClick.AddListener(() =>
			{
				Global.SimpleAbilityDamage.Value *= 1.5f; // 提升简单能力伤害1.5倍
				Time.timeScale = 1f;
				BtnUpgrade.Hide();
			});

			ActionKit.OnUpdate.Register(() =>
			{
				Global.Timer.Value += Time.deltaTime; // 在Update中计时
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
