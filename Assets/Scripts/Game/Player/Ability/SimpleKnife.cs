using System.Linq;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class SimpleKnife : ViewController
	{
		private float mTimer = 0f;

		private void Update()
		{
			mTimer += Time.deltaTime;

			if (mTimer >= Global.SimpleKnifeCD.Value)
			{
				mTimer = 0f;
				
				if (Player.Default)
				{
					var enemys = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
						.OrderBy(enemy => (enemy.Distance2D(Player.Default)))
						.Take(Global.SimpleKnifeCount.Value + Global.AdditionalFlyCount.Value);

					var i = 0;
					foreach (var enemy in enemys)
					{
						if (i < 4)
						{
							ActionKit.DelayFrame(10 * i, () => AudioKit.PlaySound(Sound.KNIFE))
								.StartGlobal();
							i++;
						}
						
						if (enemy)
						{
							Knife.Instantiate()
								.Show()
								.Position(this.Position())
								.Self(self =>
								{
									var enemyCache = enemy;
									var rigidbody2D = self.GetComponent<Rigidbody2D>();
									var dir = enemyCache.NormalizedDirection2DFrom(Player.Default);
									
									self.transform.up = dir;
									rigidbody2D.velocity = dir * 10f;

									var attackCount = 0;
									self.OnTriggerEnter2DEvent((collider) =>
									{
										var enemyCollider = collider.GetComponent<EnemyCollider>();
										if (enemyCollider)
										{
											if (enemyCollider.Owner.CompareTag("Enemy"))
											{
												var damageTimes = Global.SuperKnife.Value ? Random.Range(2, 4) : 1;
												DamageSystem.CalculateDamage(Global.SimpleKnifeDamage.Value * damageTimes, enemyCollider.Owner.GetComponent<IEnemy>());
												attackCount++;

												if (attackCount >= Global.SimpleKnifeAttackCount.Value)
												{
													self.DestroyGameObjGracefully();
												}
											}
										}
									}).UnRegisterWhenGameObjectDestroyed(self);
									
									//  到相机外则销毁
									self.OnBecameInvisibleEvent(() =>
									{
										self.DestroyGameObjGracefully();
									}).UnRegisterWhenGameObjectDestroyed(self);
								});
						}
					}
				}
			}
		}
	}
}
