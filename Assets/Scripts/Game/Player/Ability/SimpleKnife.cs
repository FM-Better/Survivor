using System.Linq;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class SimpleKnife : ViewController
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
				
				var enemys = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				if (playerTrans)
				{
					var enemy = enemys.OrderBy(enemy => (enemy.Position() - playerTrans.position).magnitude).FirstOrDefault();
						if (enemy  && (enemy.Position() - playerTrans.position).magnitude <= 6f)
						{
							Knife.Instantiate()
								.Show()
								.Position(this.Position())
								.Self(self =>
								{
									var rigidbody2D = self.GetComponent<Rigidbody2D>();
									rigidbody2D.velocity = (enemy.Position() - playerTrans.position).normalized * 10f;

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
										if (!playerTrans || (playerTrans.position - self.Position()).magnitude > 12)
										{
											self.DestroyGameObjGracefully();
										}
									}).UnRegisterWhenGameObjectDestroyed(self);
								});
					}
				}
			}
		}
	}
}
