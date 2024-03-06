using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class EnemySpawner : ViewController
	{
		private Transform playerTrans;
		[SerializeField] private float spawnCD;
		private float spawnTimer = 0f;
		[SerializeField] private float spawnDis;

		
		void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform;
		}

		private void Update()
		{
			if (!playerTrans)
				return;

			spawnTimer += Time.deltaTime;
			if (spawnTimer >= spawnCD)
			{
				spawnTimer = 0f;
				
				var spawnPos = CalcSpawnPos(spawnDis);
				
				Enemy.Instantiate()
					.Position(spawnPos)
					.Show();
			}
		}

		private Vector3 CalcSpawnPos(float SpwanDistance)
		{
			var angle = Random.Range(0, 360f);
			var rad = Mathf.Deg2Rad * angle;
			var direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

			return playerTrans.position + SpwanDistance * direction;
		}
	}
}
