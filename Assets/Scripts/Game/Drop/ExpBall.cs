using QAssetBundle;
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
				AudioKit.PlaySound(Sound.EXP);
				Global.Exp.Value++;
				this.DestroyGameObjGracefully();
			}
		}
	}
}
