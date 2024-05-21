using System;
using System.Collections;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Player : ViewController
	{
		[SerializeField] private float moveSpeed;
		[SerializeField] private Animator _animator;
		[SerializeField] private int twinkleCircle;

		public static Player Default;
		private AudioPlayer mWalkSound;

		private void Awake()
		{
			Default = this;
		}

		private void Start()
		{
			HurtBox.OnTriggerEnter2DEvent((colider) =>
			{
				var hitBox = colider.GetComponent<EnemyCollider>();
				if (hitBox)
				{
					if (hitBox.Owner.CompareTag("Enemy"))
					{
						Global.Hp.Value--;
						if (Global.Hp.Value <= 0)
						{
							AudioKit.PlaySound(Sound.DIE);
							this.DestroyGameObjGracefully();
							UIKit.OpenPanel<UIGameOverPanel>();	
						}
						else
						{
							AudioKit.PlaySound(Sound.HURT);
							HurtBox.enabled = false;
							StartCoroutine(Cou_Hurt(() =>
							{
								HurtBox.enabled = true;
							}));
						}
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Hp.RegisterWithInitValue((hp) =>
			{
				UpdateHpUI();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.MaxHp.RegisterWithInitValue((hp) =>
			{
				UpdateHpUI();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.IsMarked.Register((isMarked) =>
			{
				if (isMarked)
				{
					StartCoroutine(Cou_UpdateDirection()); // 开启方向指示的每帧更新
					DirectionRoot.gameObject.SetActive(true); // 打开方向指示
				}
				else
				{
					StopCoroutine(Cou_UpdateDirection()); // 关闭方向指示的每帧更新
					DirectionRoot.gameObject.SetActive(false); // 关闭方向指示
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		IEnumerator Cou_Hurt(Action onComplete = null)
		{
			float trinkleTime = 1f;
			int frameCount = 0;
			bool isTwinkle = false;
			while (trinkleTime > 0f)
			{
				trinkleTime -= Time.deltaTime;
				frameCount++;
				if (frameCount % twinkleCircle == 0) // 每过一个闪烁周期则闪烁
					isTwinkle = !isTwinkle;
				Sprite.enabled = isTwinkle;
				yield return null;
			}
			
			Sprite.enabled = true; // 结束时打开渲染器
			onComplete?.Invoke(); // 结束的回调
		}
		
		void UpdateHpUI() => HpValue.fillAmount = Global.Hp.Value / (float)Global.MaxHp.Value;

		private bool mIsFaceLeft = true;

		IEnumerator Cou_UpdateDirection()
		{
			while (true)
			{
				var forward2D = DirectionRoot.rotation * Vector2.up; // 2D的面朝向
				var dir = ((Vector3)Global.MarkPos - transform.position).normalized; // 目标向量
				var cosValue = Vector3.Dot(dir, forward2D);

				if (cosValue < 1) // 需要旋转时
				{
					var angle = Mathf.Acos(cosValue) * Mathf.Rad2Deg; // 夹角

					angle = Vector3.Cross(dir, forward2D).z < 0 ? angle : -angle; // 是否在左侧
					DirectionRoot.rotation *= Quaternion.AngleAxis(angle, Vector3.forward); // 旋转
					DirectionRoot.localPosition = dir * 2f; // 更新位置	
				}

				yield return null;
			}
		}
		
		private void FixedUpdate()
		{
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			var direction = new Vector2(horizontal, vertical).normalized;
			var cilpName = string.Empty;
				
			selfRigidbody.velocity = Vector2.Lerp(selfRigidbody.velocity,
				direction * (moveSpeed * Global.SpeedRate.Value), 1 - Mathf.Exp(-Time.deltaTime * 5));

			if (horizontal == 0 && vertical == 0) // Idle
			{
				if (mWalkSound != null)
				{
					mWalkSound.Stop();
					mWalkSound = null;
				}

				cilpName = mIsFaceLeft ? "PlayerLeftIdle" : "PlayerRightIdle";
			}
			else // Walk
			{
				if (mWalkSound == null)
				{
					mWalkSound = AudioKit.PlaySound(Sound.WALK, true);
				}

				mIsFaceLeft = !(horizontal > 0);
				cilpName = mIsFaceLeft ? "PlayerLeftWalk" : "PlayerRightWalk";
			}
			_animator.Play(cilpName);
		}
	}
}
