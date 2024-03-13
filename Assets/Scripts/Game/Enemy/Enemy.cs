using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Enemy : ViewController, IEnemy
	{
		[SerializeField] private float moveSpeed;
		private Transform playerTrans;
		public float Hp => hp;
		[SerializeField] private float hp;
		[SerializeField] private float hurtDurationTime;
		private bool isDead;
		
		void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform; // 缓存玩家tarnsfrom
			EnemySpawner.enemyCount.Value++; // 计数
			isDead = false;
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

		public void Hurt(float damage)
		{
			if (!isDead)
			{
                hp -= damage;
                AudioKit.PlaySound("Hit");
                FloatingTextController.ShowFloatingText(transform.position + Vector3.up * 0.5f, damage.ToString()); // 伤害飘字效果
                this.Sprite.color = Color.red;

                ActionKit.Delay((hurtDurationTime), () =>
                {
                    this.Sprite.color = Color.white;
                }).Start(this);

                if (hp <= 0)
                {
					isDead = true;
                    Dead();
                }
            }
		}

		public void PopulateHp(float nowWaveHpScale)
		{
			hp *= nowWaveHpScale;
		}

		public void PopulateSpeed(float nowWaveSpeedScale)
		{
			moveSpeed *= nowWaveSpeedScale;
		}

		public void Dead()
		{
			DropManager.Default.SpawnDrop(transform.position);
			EnemySpawner.enemyCount.Value--;
			
			this.DestroyGameObjGracefully();
		}
	}
}
