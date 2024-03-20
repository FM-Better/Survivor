using System;
using QAssetBundle;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace Survivor
{
	public partial class Ball : ViewController
	{
		void Start()
		{
			selfRigidbody.velocity = new Vector2(Random.Range(-1f, 0f), Random.Range(0f, 1f)) *
			                         Random.Range(Global.BasktetBallSpeed.Value - 2, Global.BasktetBallSpeed.Value + 2);

			Global.SuperBasketBall.Register((isUnlocked) =>
			{
				if (isUnlocked)
				{
					this.LocalScale(3);
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			HitBox.OnTriggerEnter2DEvent(collider =>
			{
				var hurtBox = collider.GetComponent<HurtBox>();
				if (hurtBox)
				{
					if (hurtBox.Owner.CompareTag("Enemy"))
					{
						var enemy = hurtBox.Owner.GetComponent<IEnemy>();
						var damageTimes = Global.SuperBasketBall.Value ? Random.Range(2, 4) : 1;
						DamageSystem.CalculateDamage(Global.BasketBallDamage.Value * damageTimes, enemy);
						
						if (Random.Range(0, 100) < 50 && collider && collider.attachedRigidbody && Player.Default)
						{
							collider.attachedRigidbody.velocity =
								collider.NormalizedDirection2DFrom(this) * 5f +
								collider.NormalizedDirection2DFrom(Player.Default) * 10f;
						}
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			var normal = other.GetContact(0).normal;

			if (normal.x > normal.y)
			{
				selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x,
					Mathf.Sign(selfRigidbody.velocity.y) * Random.Range(0.5f, 1.5f) *
					Random.Range(Global.BasktetBallSpeed.Value - 2, Global.BasktetBallSpeed.Value + 2));
			}
			else
			{
				selfRigidbody.velocity = new Vector2(
					Mathf.Sign(selfRigidbody.velocity.x) * Random.Range(0.5f, 1.5f) *
					Random.Range(Global.BasktetBallSpeed.Value - 2, Global.BasktetBallSpeed.Value + 2),
					selfRigidbody.velocity.y);
			}
			selfRigidbody.angularVelocity = Random.Range(-360, 360);
			
			AudioKit.PlaySound(Sound.BALL);
		}
	}
}
