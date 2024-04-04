using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class HpItem : PickUpObject
	{
		protected override Collider2D collider => selfCollider;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				if (Global.Hp.Value < Global.MaxHp.Value)
				{
					StartPickUpAnim();
				}
			}
		}

		protected override void Excute()
		{
			AudioKit.PlaySound(Sound.HPITEM);
			Global.Hp.Value++;
			DropManager.s_HpItemCount--;
			this.DestroyGameObjGracefully();
		}
	}
}
