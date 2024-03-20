using Cinemachine;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Bomb : GamePlayObject
	{
		protected override Collider2D collider => selfCollider;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				Excute();
				this.DestroyGameObjGracefully();
			}
		}

		public static void Excute()
		{
			var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
			foreach (var enemy in enemies)
			{
				if (enemy)
				{
					DamageSystem.CalculateDamage(Global.BombDamage.Value, enemy);	
				}
			}
				
			AudioKit.PlaySound(Sound.BOMB);
			CameraController.ShakeCamera.Trigger(); // 触发震屏事件
			UIGamePanel.FlashScreen.Trigger(); // 触发闪屏事件
		}
	}
}
