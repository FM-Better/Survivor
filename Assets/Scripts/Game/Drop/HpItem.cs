using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class HpItem : ViewController
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				if (Global.Hp.Value < Global.MaxHp.Value)
				{
					AudioKit.PlaySound(Sound.HPITEM);
					Global.Hp.Value++;
					this.DestroyGameObjGracefully();	
				}
			}
		}
	}
}
