using System;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace Survivor
{
	public partial class DropManager : ViewController
	{
		public static DropManager Default;

		private void Awake()
		{
			Default = this;
		}

		private void OnDestroy()
		{
			Default = null;
		}
		
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
			if (randomNum < Global.HpItemDropRate.Value) // 掉落回血道具
			{
				HpItem.InstantiateWithParent(DropRoot)
					.Position(spawnPosition + RandomInCircle())
					.Show();
			}
			if (Global.BombUnlocked.Value && randomNum < Global.BombDropRate.Value) // 掉落炸弹
			{
				Bomb.InstantiateWithParent(Default.DropRoot)
					.Position(spawnPosition + RandomInCircle())
					.Show();
			}
			if (randomNum < Global.GetAllExpDropRate.Value) // 掉落获得当前所有经验的道具
			{
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
