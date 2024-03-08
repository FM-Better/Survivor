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

			#region UI相关
			BtnSimpleDamage.onClick.AddListener(() =>
			{
				Global.SimpleAbilityDamage.Value *= 1.5f; // 简单能力伤害提升1.5倍
				Time.timeScale = 1f;
				UpgradeRoot.Hide();
			});
			
			BtnSimpleCD.onClick.AddListener(() =>
			{
				Global.SimpleAbilityCD.Value *= 0.5f; // 简单能力时间间隔缩短0.5倍
				Time.timeScale = 1f;
				UpgradeRoot.Hide();
			});
			#endregion
			
			#region Gloal相关
			Global.Exp.RegisterWithInitValue((exp) =>
			{
				TxtExp.text = $"经验值：({Global.Exp.Value}/{Global.CurrentLevelExp()})";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.Exp.Register((exp) =>
			{
				if (exp >= Global.CurrentLevelExp())
				{
					Global.Exp.Value -= Global.CurrentLevelExp();
					Global.Level.Value++;
				}
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
				Time.timeScale = 0f;
				UpgradeRoot.Show();
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
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			#endregion
			
			#region ActionKit相关
			ActionKit.OnUpdate.Register(() =>
			{
				Global.Timer.Value += Time.deltaTime; // 在Update中计时
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
