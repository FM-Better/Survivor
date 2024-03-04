using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Player : ViewController
	{
		[SerializeField] private float moveSpeed;
		
		private void FixedUpdate()
		{
			var horizontal = Input.GetAxis("Horizontal");
			var vertical = Input.GetAxis("Vertical");
			var direction = new Vector2(horizontal, vertical).normalized;
			selfRigidbody.velocity = direction * moveSpeed;
		}
		
		private void Update()
		{
			
		}
	}
}
