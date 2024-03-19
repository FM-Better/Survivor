using Cinemachine;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Bomb : GamePlayObject
	{
		private CinemachineImpulseSource impulseSource; // 脉冲源
		protected override Collider2D collider => selfCollider;
		
		private void Start()
		{
			impulseSource = GameObject.FindWithTag("CameraController").GetComponent<CinemachineImpulseSource>(); // 缓存脉冲源 用作相机抖动效果
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (var enemy in enemies)
				{
					enemy.Hurt(Global.BombDamage.Value);
				}
				
				AudioKit.PlaySound(Sound.BOMB);
				impulseSource.GenerateImpulse(); // 发生脉冲信号
				UIGamePanel.FlashScreen.Trigger(); // 触发闪屏事件
				this.DestroyGameObjGracefully();
			}
		}
	}
}
