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
						.Take(Global.SimpleKnifeCount.Value);

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
									var selfCache = self;
									var enemyCache = enemy;
									var rigidbody2D = self.GetComponent<Rigidbody2D>();
									var dir = enemyCache.NormalizedDirection2DFrom(Player.Default);
									
									selfCache.transform.up = dir;
									rigidbody2D.velocity = dir * 10f;

									var attackCount = 0;
									selfCache.OnTriggerEnter2DEvent((collider) =>
									{
										var hurtBox = collider.GetComponent<HurtBox>();
										if (hurtBox)
										{
											if (hurtBox.Owner.CompareTag("Enemy"))
											{
												hurtBox.Owner.GetComponent<IEnemy>()
													.Hurt(Global.SimpleKnifeDamage.Value);
												attackCount++;

												if (attackCount >= Global.SimpleKnifeAttackCount.Value)
												{
													selfCache.DestroyGameObjGracefully();
												}
											}
										}
									}).UnRegisterWhenGameObjectDestroyed(self);

									ActionKit.OnUpdate.Register(() =>
									{
										if (!Player.Default || selfCache.Distance2D(Player.Default) > 12)
										{
											selfCache.DestroyGameObjGracefully();
										}
									}).UnRegisterWhenGameObjectDestroyed(selfCache);
								});
						}
					}
				}
			}
		}
	}
}
