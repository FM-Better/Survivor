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
		}

		IEnumerator Cou_Hurt(Action onComplete = null)
		{
			float trinkleTime = 1f;
			int count = 0;
			bool isTwinkle = false;
			while (trinkleTime > 0f)
			{
				trinkleTime -= Time.deltaTime;
				count++;
				// 每过一个闪烁周期则闪烁

				if (count % twinkleCircle == 0)
					isTwinkle = !isTwinkle;

				if (isTwinkle)
				{
					Sprite.enabled = false;
				}
				else
				{
					Sprite.enabled = true;
				}

				yield return null;
			}
			
			Sprite.enabled = true; // 结束时打开渲染器
			onComplete?.Invoke(); // 结束的回调
		}
		
		void UpdateHpUI() => HpValue.fillAmount = Global.Hp.Value / (float)Global.MaxHp.Value;

		private bool mIsFaceLeft = true;
        
		private void FixedUpdate()
		{
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			var direction = new Vector2(horizontal, vertical).normalized;

			selfRigidbody.velocity = Vector2.Lerp(selfRigidbody.velocity,
				direction * (moveSpeed * Global.SpeedRate.Value), 1 - Mathf.Exp(-Time.deltaTime * 5));

			if (horizontal == 0 && vertical == 0) // Idle
			{
				if (mWalkSound != null)
				{
					mWalkSound.Stop();
					mWalkSound = null;
				}
				
				if (mIsFaceLeft)
				{
					_animator.Play("PlayerLeftIdle");	
				}
				else
				{
					_animator.Play("PlayerRightIdle");
				}
			}
			else // Walk
			{
				if (mWalkSound == null)
				{
					mWalkSound = AudioKit.PlaySound(Sound.WALK, true);
				}
				
				if (horizontal > 0)
				{
					mIsFaceLeft = false;
				}
				else
				{
					mIsFaceLeft = true;
				}
				
				if (mIsFaceLeft)
				{
					_animator.Play("PlayerLeftWalk");	
				}
				else
				{
					_animator.Play("PlayerRightWalk");
				}
			}
		}
	}
}
