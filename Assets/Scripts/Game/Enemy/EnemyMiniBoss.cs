using QAssetBundle;
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
        [SerializeField] private Color dissolveColor = Color.yellow;
		
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
			EnemySpawner.enemyCount.Value++; // 计数
			
			// 追逐
			fsm.State(States.Chase)
				.OnFixedUpdate(() =>
				{
					if (Player.Default)
					{
						var dirVector = transform.NormalizedDirection2DTo(Player.Default);
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
					if (Player.Default)
					{
						dashTimer = 0f;
						var dir = transform.NormalizedDirection2DTo(Player.Default);
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
						if (Player.Default)
						{
							if (transform.Distance2D(Player.Default) > dashDistance)
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

		private void Update() => fsm.Update();

		private void FixedUpdate() => fsm.FixedUpdate();
		
		public void Hurt(float damage, bool isCritical = false)
		{
			if (!isDead)
			{
				hp -= damage;
				AudioKit.PlaySound(Sound.HIT);
				FloatingTextController.ShowFloatingText(transform.position + Vector3.up * 0.5f, damage.ToString("0"), isCritical); // 伤害飘字效果
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

		public void PopulateHp(float nowWaveHpScale) => hp *= nowWaveHpScale;

		public void PopulateSpeed(float nowWaveSpeedScale) => chaseSpeed *= chaseSpeed;

		public void Dead()
		{
			DropManager.Default.SpawnDrop(transform.position,true);
			EnemySpawner.enemyCount.Value--;
			FxConrtoller.Play(Sprite, dissolveColor);
			AudioKit.PlaySound(Sound.ENEMYDIE);
			this.DestroyGameObjGracefully();
		}
	}
}
