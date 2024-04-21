using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class ExpBall : PickUpObject
	{
		protected override Collider2D collider => selfCollider;
		public bool IsForced = false; // 是否强行拾取 即不进行后退再飞行的动画
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				if (IsForced)
				{
					Excute();
				}
				else
				{
					StartPickUpAnim();	
				}
			}
		}

		protected override void Excute()
		{
			AudioKit.PlaySound(Sound.EXP);
			Global.Exp.Value++;
			this.DestroyGameObjGracefully();
		}
	}
}
