using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Gold : PickUpObject
	{
		protected override Collider2D collider => selfCollider;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				StartPickUpAnim();
			}
		}

		protected override void Excute()
		{
			AudioKit.PlaySound(Sound.GOLD);
			Global.Gold.Value++;
			this.DestroyGameObjGracefully();
		}
	}
}
