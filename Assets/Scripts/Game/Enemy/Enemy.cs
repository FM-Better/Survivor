using System;
using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
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
				transform.Translate(direction * (moveSpeed * Time.deltaTime));	
			}
		}

		public void Hurt(float damage, float durationTime)
		{
			hp -= damage;
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
			this.DestroyGameObjGracefully();
			EnemySpawner.enemyCount.Value--;
			Global.Exp.Value++;
		}
	}
}
