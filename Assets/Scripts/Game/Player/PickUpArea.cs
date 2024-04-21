using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class PickUpArea : ViewController
	{
		private float mInitRadius;
		
		private void Awake()
		{
			mInitRadius = mInitRadius = GetComponent<CircleCollider2D>().radius;
		}

		private void Start()
		{
			Global.PickUpAreaRange.Register(range =>
			{
				GetComponent<CircleCollider2D>().radius = mInitRadius * range;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
	}
}
