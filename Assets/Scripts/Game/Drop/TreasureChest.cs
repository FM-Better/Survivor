using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class TreasureChest : GamePlayObject
	{
		protected override Collider2D collider => selfCollider;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				UIGamePanel.OpenTreasurePanel.Trigger();

				AudioKit.PlaySound(Sound.TREASURECHEST);
				this.DestroyGameObjGracefully();
			}
		}
	}
}
