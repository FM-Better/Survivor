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
						hurtBox.Owner.GetComponent<Enemy>().Hurt(2);
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			// var angle = Time.frameCount % 360;
			// var rad = angle * Mathf.Deg2Rad;
			// var pos = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
			transform.Rotate(Vector3.forward, 180 * Time.deltaTime);
		}
	}
}
