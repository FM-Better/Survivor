using System;
using QAssetBundle;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace Survivor
{
	public partial class Ball : ViewController
	{
		void Start()
		{
			selfRigidbody.velocity = new Vector2(Random.Range(-1f, 0f), Random.Range(0f, 1f)) *
			                         Random.Range(Global.BasktetBallSpeed.Value - 2, Global.BasktetBallSpeed.Value + 2);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			var normal = other.GetContact(0).normal;

			if (normal.x > normal.y)
			{
				selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x,
					Mathf.Sign(selfRigidbody.velocity.y) * Random.Range(0.5f, 1.5f) *
					Random.Range(Global.BasktetBallSpeed.Value - 2, Global.BasktetBallSpeed.Value + 2));
			}
			else
			{
				selfRigidbody.velocity = new Vector2(
					Mathf.Sign(selfRigidbody.velocity.x) * Random.Range(0.5f, 1.5f) *
					Random.Range(Global.BasktetBallSpeed.Value - 2, Global.BasktetBallSpeed.Value + 2),
					selfRigidbody.velocity.y);
			}
			selfRigidbody.angularVelocity = Random.Range(-360, 360);
			
			AudioKit.PlaySound(Sound.BALL);
		}
	}
}
