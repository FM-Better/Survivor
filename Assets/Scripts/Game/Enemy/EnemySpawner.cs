using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;
using UnityEngine.TextCore.LowLevel;
using Random = UnityEngine.Random;

namespace Survivor
{
	public partial class EnemySpawner : ViewController
	{
		[SerializeField] private LevelConfig LevelConfig;

		[Header("刷怪点的边界")] 
		[SerializeField] private Transform leftBottom;
		[SerializeField] private Transform rightTop;
		
		private float waitTimer = 0f;
		private float spawnTimer = 0f;
		private float waveTimer = 0f;
		private List<EnemyWave> enemyWaveList = new List<EnemyWave>();
		private int nowWaveCount = 1;
		private bool isOver;

		public static BindableProperty<int> enemyCount = new BindableProperty<int>();
		
		void Start()
		{
			isOver = false;
			// 读取关卡的波次配置信息
			foreach (var enemyWaveGroup in LevelConfig.EnemyWaveGroups)
			{
				if (!enemyWaveGroup.Active)
					continue;
				foreach (var wave in enemyWaveGroup.Waves)
				{
					if (wave.Active)
						enemyWaveList.Add(wave);
				}
			}
		}

		private void Update()
		{
			if (!Player.Default || Global.IsEnemySpawnOver.Value)
				return;

			if (nowWaveCount <= enemyWaveList.Count)
			{
				var nowWave = enemyWaveList[nowWaveCount - 1];

				if (!nowWave.isWaited)
				{
					waitTimer += Time.deltaTime;
					if (waitTimer >= nowWave.WaitTime)
					{
						waitTimer = 0f;
						nowWave.isWaited = true;
					}
				}
				else
				{
					spawnTimer += Time.deltaTime;
					if (spawnTimer >= nowWave.SpawnCD)
					{
						spawnTimer = 0f;

						var spawnPos = CalcSpawnPos();
						nowWave.EnemyPrefab.InstantiateWithParent(EnemyRoot)
							.Position(spawnPos)
							.Self((self) =>
							{
								var enemy = self.GetComponent<IEnemy>(); 
								enemy.PopulateHp(nowWave.HpScale);
								enemy.PopulateSpeed(nowWave.SpeedScale);
							})
							.Show();
					}

					waveTimer += Time.deltaTime;
					if (waveTimer >= nowWave.DurationTime)
					{
						waveTimer = 0f;

						nowWaveCount++;
					}
				}
			}
			else
			{
				if (!isOver)
				{
					Global.IsEnemySpawnOver.Value = true;
					isOver = true;
				}
			}
		}

		private Vector3 CalcSpawnPos()
		{
			var spwanForX = RandomUtility.Choose(true, false);
			float randomX;
			float randomY;
			if (spwanForX)
			{
				randomX = Random.Range(leftBottom.PositionX(), rightTop.PositionX());
				randomY = RandomUtility.Choose(leftBottom.PositionY(), rightTop.PositionY());
			}
			else
			{
				randomX = RandomUtility.Choose(leftBottom.PositionX(), rightTop.PositionX());
				randomY = Random.Range(leftBottom.PositionY(), rightTop.PositionY());
			}

			return new Vector3(randomX, randomY, 0f);
		}
	}
}
