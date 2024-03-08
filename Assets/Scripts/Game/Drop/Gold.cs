using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Gold : ViewController
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				AudioKit.PlaySound("Gold");
				Global.Gold.Value++;
				this.DestroyGameObjGracefully();
			}
		}
	}
}
