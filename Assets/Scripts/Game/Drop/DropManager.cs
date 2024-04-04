using System;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace Survivor
{
	public partial class DropManager : ViewController
	{
		public static DropManager Default;
		public static int s_HpItemCount = 0;
		public static int s_GetAllExpCount = 0;
		public static int s_BombCount = 0;

		private void Awake()
		{
			Default = this;
		}

		private void OnDestroy()
		{
			Default = null;
		}

#if UNITY_EDITOR
		private void OnGUI()
		{
			var guiStyle = new GUIStyle();
			guiStyle.fontSize = 20;
			guiStyle.normal.textColor = Color.white;
			GUI.Label(new Rect(10, 80, 150, 20), $"回血道具： {s_HpItemCount}", guiStyle);
			GUI.Label(new Rect(10, 110, 150, 20), $"拾取经验道具： {s_GetAllExpCount}", guiStyle);
			GUI.Label(new Rect(10, 140, 150, 20), $"炸弹： {s_BombCount}", guiStyle);
		}
#endif
		
		public void SpawnDrop(Vector3 spawnPosition, bool isSpawnTreasure)
		{
			if (isSpawnTreasure)
			{
				TreasureChest.InstantiateWithParent(DropRoot)
					.Position(spawnPosition + RandomInCircle())
					.Show();
			}
			
			var randomNum = Random.Range(0, 100);
			if (randomNum < Global.ExpBallDropRate.Value + Global.AdditionalExpRate.Value) // 掉落经验球
			{
				ExpBall.InstantiateWithParent(DropRoot)
					.Position(spawnPosition + RandomInCircle())
					.Show();
			}
			if (randomNum < Global.GoldDropRate.Value) // 掉落金币
			{
				Gold.InstantiateWithParent(DropRoot)
					.Position(spawnPosition + RandomInCircle())
					.Show();
			}
			if (s_HpItemCount < 2 && randomNum < Global.HpItemDropRate.Value) // 掉落回血道具
			{
				s_HpItemCount++;
				HpItem.InstantiateWithParent(DropRoot)
					.Position(spawnPosition + RandomInCircle())
					.Show();
			}
			if (s_BombCount < 4 && Global.BombUnlocked.Value && randomNum < Global.BombDropRate.Value) // 掉落炸弹
			{
				s_BombCount++;
				Bomb.InstantiateWithParent(Default.DropRoot)
					.Position(spawnPosition + RandomInCircle())
					.Show();
			}
			if (s_GetAllExpCount < 2 && randomNum < Global.GetAllExpDropRate.Value) // 掉落获得当前所有经验的道具
			{
				s_GetAllExpCount++;
				GetAllExp.InstantiateWithParent(DropRoot)
					.Position(spawnPosition + RandomInCircle())
					.Show();
			}
		}
        
		private Vector3 RandomInCircle()
		{
			var angle = Random.Range(0, 360f);
			var rad = angle * Mathf.Deg2Rad;
			return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * 0.3f;
		}
	}
}
