using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Player : ViewController
	{
		[SerializeField] private float moveSpeed;

		private void Start()
		{
			HurtBox.OnTriggerEnter2DEvent((colider) =>
			{
				var hitBox = colider.GetComponent<HitBox>();
				if (hitBox)
				{
					if (hitBox.Owner.CompareTag("Enemy"))
					{
						Global.Hp.Value--;
						if (Global.Hp.Value <= 0)
						{
							AudioKit.PlaySound("Die");
							this.DestroyGameObjGracefully();
							UIKit.OpenPanel<UIGameOverPanel>();	
						}
						else
						{
							AudioKit.PlaySound("Hurt");
						}
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void FixedUpdate()
		{
			var horizontal = Input.GetAxis("Horizontal");
			var vertical = Input.GetAxis("Vertical");
			var direction = new Vector2(horizontal, vertical).normalized;
            
			selfRigidbody.velocity = direction * moveSpeed;
		}
	}
}
