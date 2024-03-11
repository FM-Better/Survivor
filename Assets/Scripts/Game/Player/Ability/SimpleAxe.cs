using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class SimpleAxe : ViewController
	{
		[SerializeField] private float spawnCD;
		private float mTimer = 0f;
		private Transform playerTrans;

		private void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform;
		}

		private void Update()
		{
			mTimer += Time.deltaTime;

			if (mTimer >= spawnCD)
			{
				mTimer = 0f;
				
				Axe.Instantiate()
					.Show()
					.Position(this.Position())
					.Self(self =>
					{
						var rigidbody2D = self.GetComponent<Rigidbody2D>();
						var randomX = RandomUtility.Choose(-7, -5, -3, 3, 5, 7);
						var randomY = RandomUtility.Choose(3, 5, 7);
						rigidbody2D.velocity = new Vector2(randomX, randomY);

						self.OnTriggerEnter2DEvent((collider) =>
						{
							var hurtBox = collider.GetComponent<HurtBox>();
							if (hurtBox)
							{
								if (hurtBox.Owner.CompareTag("Enemy"))
								{
									hurtBox.Owner.GetComponent<Enemy>().Hurt(2);
								}
							}
						}).UnRegisterWhenGameObjectDestroyed(self);
						
						ActionKit.OnUpdate.Register(() =>
						{
							if (!playerTrans || playerTrans.position.y - self.Position().y > 15)
							{
								self.DestroyGameObjGracefully();
							}
						}).UnRegisterWhenGameObjectDestroyed(self);
					});
			}
		}
	}
}
