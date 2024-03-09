using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Bomb : ViewController
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (var enemy in enemies)
				{
					enemy.Hurt(enemy.Hp);
				}
				
				AudioKit.PlaySound("Bomb");
				this.DestroyGameObjGracefully();
			}
		}
	}
}
