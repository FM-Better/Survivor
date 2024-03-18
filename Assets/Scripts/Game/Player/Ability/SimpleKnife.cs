using System.Linq;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class SimpleKnife : ViewController
	{
		private float mTimer = 0f;
		private Transform playerTrans;

		private void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform;
		}

		private void Update()
		{
			mTimer += Time.deltaTime;

			if (mTimer >= Global.SimpleKnifeCD.Value)
			{
				mTimer = 0f;
				
				if (playerTrans)
				{
					var enemys = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
						.OrderBy(enemy => (enemy.Direction2DTo(playerTrans).magnitude))
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
									var dir = enemyCache.NormalizedDirection2DFrom(playerTrans);
									
									selfCache.transform.up = dir;
									rigidbody2D.velocity = dir * 10f;

									selfCache.OnTriggerEnter2DEvent((collider) =>
									{
										var hurtBox = collider.GetComponent<HurtBox>();
										if (hurtBox)
										{
											if (hurtBox.Owner.CompareTag("Enemy"))
											{
												hurtBox.Owner.GetComponent<IEnemy>()
													.Hurt(Global.SimpleKnifeDamage.Value);
											}
										}
									}).UnRegisterWhenGameObjectDestroyed(self);

									ActionKit.OnUpdate.Register(() =>
									{
										if (!playerTrans || selfCache.Direction2DTo(playerTrans).magnitude > 12)
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
