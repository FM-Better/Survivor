using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class SimpleCircle : ViewController
	{
		[SerializeField] private float radius;
		
		void Start()
		{
			Circle.OnTriggerEnter2DEvent((collider) =>
			{
				var hurtBox = collider.GetComponent<HurtBox>();
				if (hurtBox)
				{
					if (hurtBox.Owner.CompareTag("Enemy"))
					{
						hurtBox.Owner.GetComponent<IEnemy>().Hurt(2);
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			transform.Rotate(Vector3.forward, 180 * Time.deltaTime);
		}
	}
}
