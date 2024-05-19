using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Player : ViewController
	{
		[SerializeField] private float moveSpeed;

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
					Sprite.Play("PlayerLeftIdle");	
				}
				else
				{
					Sprite.Play("PlayerRightIdle");
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
					Sprite.Play("PlayerLeftWalk");	
				}
				else
				{
					Sprite.Play("PlayerRightWalk");
				}
			}
		}
	}
}
