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
		public static EasyEvent FlashScreen = new EasyEvent();
		public static EasyEvent OpenTreasurePanel = new EasyEvent();
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();
			
			#region Gloal相关
			Global.Exp.RegisterWithInitValue((exp) =>
			{
				if (exp >= Global.CurrentLevelExp())
				{
					Global.Exp.Value -= Global.CurrentLevelExp();
					Global.Level.Value++;
				}
				ExpValue.fillAmount = Global.Exp.Value / (float)Global.CurrentLevelExp();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.Gold.RegisterWithInitValue((gold) =>
			{
				TxtGold.text = "金币：" + gold;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.Gold.Register((gold) =>
			{
				PlayerPrefs.SetInt("Gold", gold);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.Level.RegisterWithInitValue((lv) =>
			{
				TxtLv.text = "等级：" + lv;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Level.Register((lv) =>
			{
				if (!ExpUpgradeSystem.AllUnlockedFinish) // 能力未全部升级 才能打开升级面板
				{
					Time.timeScale = 0f;
					ExpUpgradePanel.Show();	
				}
				
				AudioKit.PlaySound("LevelUp");
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
			#endregion

			#region EnemySpawner相关
			EnemySpawner.enemyCount.RegisterWithInitValue((count) =>
			{
				TxtEnemy.text = "敌人：" + count;
				
				if (count == 0)
				{
					if (Global.IsEnemySpawnOver.Value)
					{
						UIKit.OpenPanel<UIGamePassPanel>();
					}
				}

				if (count < 0)
				{
					Debug.LogError("!!!");
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			#endregion
			
			#region ActionKit相关
			ActionKit.OnUpdate.Register(() =>
			{
				Global.Timer.Value += Time.deltaTime; // 在Update中计时
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			#endregion

			#region 事件相关
			FlashScreen.Register(() =>
			{
				ActionKit.Sequence()
					.Lerp(0f, 0.5f, 0.1f, alpha =>
					{
						ScreenColor.ColorAlpha(alpha);
					})
					.Lerp(0.5f, 0f, 0.2f, alpha =>
					{
						ScreenColor.ColorAlpha(alpha);
					}, () => ScreenColor.ColorAlpha(0f))
					.Start(this);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			OpenTreasurePanel.Register(() =>
			{
				Time.timeScale = 0f;
				TreasurePanel.Show();
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
