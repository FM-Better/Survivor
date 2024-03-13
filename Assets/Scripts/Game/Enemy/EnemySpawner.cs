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
		private Transform playerTrans;
		private float waitTimer = 0f;
		private float spawnTimer = 0f;
		private float waveTimer = 0f;
		[SerializeField] private float spawnDis;
		private List<EnemyWave> enemyWaveList = new List<EnemyWave>();
		private int nowWaveCount = 1;
		private bool isOver;

		public static BindableProperty<int> enemyCount = new BindableProperty<int>();
		
		void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform; // 缓存玩家Transform
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
			if (!playerTrans || Global.IsEnemySpawnOver.Value)
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

						var spawnPos = CalcSpawnPos(spawnDis);
						nowWave.EnemyPrefab.InstantiateWithParent(EnemyRoot)
							.Position(spawnPos)
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

		private Vector3 CalcSpawnPos(float spwanDistance)
		{
			var angle = Random.Range(0, 360f);
			var rad = Mathf.Deg2Rad * angle;
			var direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

			return playerTrans.position + spwanDistance * direction;
		}
	}
}
