using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Enemy : ViewController
	{
		[SerializeField] private float moveSpeed;
		private Transform playerTrans;
		[SerializeField] private float hp;
		
		void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform; // 缓存玩家tarnsfrom
			EnemySpawner.enemyCount.Value++; // 计数
		}

		private void Update()
		{
			if (playerTrans)
			{
				var direction = (playerTrans.position - transform.position).normalized;
				selfRigidbody.velocity = direction * moveSpeed;
			}
			else
			{
				selfRigidbody.velocity = Vector2.zero;
			}
		}

		public void Hurt(float damage, float durationTime)
		{
			hp -= damage;
			AudioKit.PlaySound("Hit");
			this.Sprite.color = Color.red;
			
			ActionKit.Delay((durationTime),() =>
			{
				this.Sprite.color = Color.white;
			}).Start(this);
			
			if (hp <= 0)
			{
				Dead();
			}
		}

		public void Dead()
		{
			Global.SpawnDrop(transform.position);
			EnemySpawner.enemyCount.Value--;
			
			this.DestroyGameObjGracefully();
		}
	}
}
