using System.Linq;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class SimpleSword : ViewController
	{
		private float mtimer;
		
		private void Update()
		{
			mtimer += Time.deltaTime;	

			if (mtimer >= Global.SimpleSwordCD.Value)
			{
				var enemies = FindObjectsOfType<Enemy>(false)
					.OrderBy(enemy => enemy.Distance2D(Player.Default)) // 根据距离排序
					.Where(enemy => enemy.Distance2D(Player.Default) <= Global.SimpleSwordRange.Value) // 挑选在攻击范围内的
					.Take(Global.SimpleSwordCount.Value + Global.AdditionalFlyCount.Value); // 选取攻击数量
				foreach (var enemy in enemies)
				{
					Sword.Instantiate()
						.Position(enemy.Position() + Vector3.left * 0.2f)
						.Show()
						.Self(self =>
						{
							var selfCache = self;
							selfCache.OnTriggerEnter2DEvent(collider =>
							{
								var hurtBox = collider.GetComponent<HurtBox>();
								if (hurtBox)
								{
									if (hurtBox.Owner.CompareTag("Enemy"))
									{
										DamageSystem.CalculateDamage(Global.SimpleSwordDamage.Value, hurtBox.Owner.GetComponent<IEnemy>());
									}
								}
							}).UnRegisterWhenGameObjectDestroyed(selfCache);
							
							ActionKit.Sequence()
								.Callback(() => { selfCache.enabled = false; })
								.Parallel(p =>
								{
									p.Lerp(0, 10, 0.2f, z => { selfCache.LocalEulerAnglesZ(z); });

									p.Append(ActionKit.Sequence()
										.Lerp(0f, 1.25f, 0.1f, scale => { selfCache.LocalScale(scale); })
										.Lerp(1.25f, 1f, 0.1f, scale => { selfCache.LocalScale(scale); })
									);
								})
								.Callback(() => { selfCache.enabled = true; })
								.Parallel((p) =>
								{
									p.Lerp(10, -180, 0.2f, z => { selfCache.LocalEulerAnglesZ(z); });
									p.Append(ActionKit.Sequence()
										.Lerp(0f, 1.25f, 0.1f, scale => { selfCache.LocalScale(scale); })
										.Lerp(1.25f, 1f, 0.1f, scale => { selfCache.LocalScale(scale); })
									);
								})
								.Callback(() => { selfCache.enabled = false; })
								.Lerp(-180, 0, 0.3f, z =>
								{
									selfCache.LocalEulerAnglesZ(z)
										.LocalScale(z.Abs() / 180);
								})
								.Start(this, () => { selfCache.DestroyGameObjGracefully(); });
						});
				}
				mtimer = 0f;
			}
		}
	}
}
