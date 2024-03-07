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
		public float DurationTime;
		public float SpawnCD;
		public GameObject EnemyPrefab;
	}
	
	public partial class EnemySpawner : ViewController
	{
		private Transform playerTrans;
		private float waveTimer = 0f;
		private float spawnTimer = 0f;
		[SerializeField] private float spawnDis;
		[SerializeField] private List<EnemyWave> enemyWaveList = new List<EnemyWave>();
		private int nowWaveCount = 1;

		public static BindableProperty<int> enemyCount = new BindableProperty<int>();
		
		void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform; // 缓存玩家Transform
		}

		private void Update()
		{
			if (!playerTrans)
				return;

			if (nowWaveCount <= enemyWaveList.Count)
			{
				var nowWave = enemyWaveList[nowWaveCount - 1];
				
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
			else
			{
				Global.IsEnemySpawnOver.Value = true;
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
