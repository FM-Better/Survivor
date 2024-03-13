using System;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace Survivor
{
	public partial class EnemyMiniBoss : ViewController, IEnemy
	{
		[SerializeField] private float chaseSpeed;
		[SerializeField] private float warningTime;
		private float warningTimer;
		private Text warningText;
		[SerializeField] private float dashSpeed;
		[SerializeField] private float dashDistance;
		[SerializeField] private float dashTime;
		private float dashTimer;
		[SerializeField] private float waitTime;
		private float waitTimer;
		public float Hp => hp;
		[SerializeField] private float hp;
		[SerializeField] private float hurtDurationTime;
        private bool isDead;

        private Transform playerTrans;
		
		enum States
		{
			Chase, // 追逐
			Warning, // 警戒
			Dash, // 冲刺
			Wait, // 等待
		}

		private FSM<States> fsm = new FSM<States>(); 
			
		void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform; // 缓存玩家tarnsfrom
			EnemySpawner.enemyCount.Value++; // 计数
			
			// 追逐
			fsm.State(States.Chase)
				.OnFixedUpdate(() =>
				{
					if (playerTrans)
					{
						var dirVector = playerTrans.position - transform.position;
						if ((dirVector).magnitude > dashDistance)
						{
							selfRigidbody.velocity = dirVector.normalized * chaseSpeed;
						}
						else
						{
							fsm.ChangeState(States.Warning);
						}
					}
					else
					{
						selfRigidbody.velocity = Vector2.zero;
					}
				});
			// 警戒（前摇）
			fsm.State(States.Warning)
				.OnEnter(() =>
				{
					warningTimer = 0f;
					selfRigidbody.velocity = Vector2.zero;
					warningText = FloatingTextController.ShowWarning(gameObject, Vector2.up);
				})
				.OnUpdate(() =>
				{
					warningTimer += Time.deltaTime;
					if (warningTimer >= warningTime)
					{
						warningTimer = 0f;
						warningText.DestroyGameObjGracefully();
						warningText = null;
						fsm.ChangeState(States.Dash);
					}
				});
			// 冲刺
			fsm.State(States.Dash)
				.OnEnter(() =>
				{
					if (playerTrans)
					{
						dashTimer = 0f;
						var dir = (playerTrans.position - transform.position).normalized;
						selfRigidbody.velocity = dir * dashSpeed;
					}
				})
				.OnUpdate(() =>
				{
					dashTimer += Time.deltaTime;
					if (dashTimer >= dashTime)
					{
						dashTimer = 0f;
						fsm.ChangeState(States.Wait);
					}
				});
			// 等待
			fsm.State(States.Wait)
				.OnEnter(() =>
				{
					waitTimer = 0f;
					selfRigidbody.velocity = Vector2.zero;
				})
				.OnUpdate(() =>
				{
					waitTimer += Time.deltaTime;
					if (waitTimer >= waitTime)
					{
						waitTimer = 0f;
						if (playerTrans)
						{
							if ((playerTrans.position - transform.position).magnitude > dashDistance)
							{
								fsm.ChangeState(States.Chase);
							}
							else
							{
								fsm.ChangeState(States.Warning);
							}
						}
						else
						{
							selfRigidbody.velocity = Vector2.zero;
						}
					}
				});
			
			fsm.StartState(States.Chase);
		}		
		
		private void Update()
		{
			fsm.Update();
		}

		private void FixedUpdate()
		{
			fsm.FixedUpdate();
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
			chaseSpeed *= chaseSpeed;
		}

		public void Dead()
		{
			DropManager.Default.SpawnDrop(transform.position);
			EnemySpawner.enemyCount.Value--;
			
			this.DestroyGameObjGracefully();
		}
	}
}
