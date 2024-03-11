using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;
using UnityEngine.TextCore.LowLevel;
using Random = UnityEngine.Random;

namespace Survivor
{
	[Serializable]
	public class EnemyWave
	{
		public float DurationTime; // 持续时间
		public float SpawnCD; // 刷怪的CD
		public bool isWaited; // 是否等待完毕
		public float WaitTime; // 等待下一波的时间
		public GameObject EnemyPrefab;
	}
	
	public partial class EnemySpawner : ViewController
	{
		private Transform playerTrans;
		private float waitTimer = 0f;
		private float spawnTimer = 0f;
		private float waveTimer = 0f;
		[SerializeField] private float spawnDis;
		[SerializeField] private List<EnemyWave> enemyWaveList = new List<EnemyWave>();
		private int nowWaveCount = 1;

		public static BindableProperty<int> enemyCount = new BindableProperty<int>();
		private bool isOver;
		
		void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform; // 缓存玩家Transform
			isOver = false;
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
						nowWave.EnemyPrefab.Instantiate()
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
