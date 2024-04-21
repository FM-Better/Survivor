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

			Global.RotateSwordCount.Or(Global.AdditionalFlyCount).Register(() =>
			{
				CreateSwords();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.RotateSwordRange.Register(_ =>
			{
				UpdatePosition();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			CreateSwords();
		}

		void CreateSwords()
		{
			var toAddCount = Global.RotateSwordCount.Value + Global.AdditionalFlyCount.Value - mSwords.Count;
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
									var damageTimes = Global.SuperRotateSword.Value ? Random.Range(2, 4) : 1;
									DamageSystem.CalculateDamage(Global.RotateSwordDamage.Value * damageTimes, hurtBox.Owner.GetComponent<IEnemy>());
									
									if (Random.Range(0, 100) < 50)
									{
										collider.attachedRigidbody.velocity =
											collider.NormalizedDirection2DFrom(selfCache) * 5f +
											collider.NormalizedDirection2DFrom(Player.Default) * 10f;
									}
								}
							}
						}).UnRegisterWhenGameObjectDestroyed(selfCache);
					})
					.Show());	
			}
			
			UpdatePosition();
		}
		
		void UpdatePosition()
		{
			var offsetAngle = 360 / mSwords.Count;
			for (int i = 0; i < mSwords.Count; i++)
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
			var speedTimes = Global.SuperRotateSword.Value ? 10 : 1;
			transform.Rotate(Vector3.forward, -60 * Time.deltaTime * Global.RotateSwordSpeed.Value * speedTimes);
		}
	}
}
