using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class ExpBall : ViewController
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				Global.Exp.Value++;
				this.DestroyGameObjGracefully();
			}
		}
	}
}
