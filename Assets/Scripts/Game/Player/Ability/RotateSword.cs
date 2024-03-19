using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class RotateSword : ViewController
	{
		private List<Collider2D> mSwords = new List<Collider2D>();
		
		void Start()
		{
			Sword.transform.up = Sword.NormalizedDirection2DFrom(Player.Default);

			Global.RotateSwordCount.RegisterWithInitValue(count =>
			{
				var toAddCount = count - mSwords.Count;
				for (int i = 0; i < toAddCount; i++)
				{
					mSwords.Add(Sword.InstantiateWithParent(transform)
						.Self(self =>
						{
							var selfCache = self;
							selfCache.OnTriggerEnter2DEvent((collider) =>
							{
								var hurtBox = collider.GetComponent<HurtBox>();
								if (hurtBox)
								{
									if (hurtBox.Owner.CompareTag("Enemy"))
									{
										DamageSystem.CalculateDamage(Global.RotateSwordDamage.Value, hurtBox.Owner.GetComponent<IEnemy>());
									}

									if (Random.Range(0, 100) < 50)
									{
										collider.attachedRigidbody.velocity =
											collider.NormalizedDirection2DFrom(selfCache) * 5f +
											collider.NormalizedDirection2DFrom(Player.Default) * 10f;
									}
								}
							}).UnRegisterWhenGameObjectDestroyed(selfCache);
						})
						.Show());			
				}

				UpdatePosition();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.RotateSwordRange.Register(_ =>
			{
				UpdatePosition();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		void UpdatePosition()
		{
			var offsetAngle = 360 / Global.RotateSwordCount.Value;
			for (int i = 0; i < Global.RotateSwordCount.Value; i++)
			{
				var rad = offsetAngle * i * Mathf.Deg2Rad;
				var x = Mathf.Cos(rad) * Global.RotateSwordRange.Value;
				var y = Mathf.Sin(rad) * Global.RotateSwordRange.Value;
				mSwords[i].LocalPosition(x, y);
				mSwords[i].transform.up = mSwords[i].NormalizedDirection2DFrom(Player.Default);
			}
		}
		
		private void Update()
		{
			transform.Rotate(Vector3.forward, -60 * Time.deltaTime * Global.RotateSwordSpeed.Value);
		}
	}
}
