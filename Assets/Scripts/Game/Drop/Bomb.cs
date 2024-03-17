using Cinemachine;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Bomb : ViewController
	{
		private CinemachineImpulseSource impulseSource; // 脉冲源
		public static float Damage = 1;
			
		private void Start()
		{
			impulseSource = GameObject.FindWithTag("CameraController").GetComponent<CinemachineImpulseSource>(); // 缓存脉冲源用作相机抖动效果
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (var enemy in enemies)
				{
					enemy.Hurt(Damage);
				}
				
				AudioKit.PlaySound(Sound.BOMB);
				impulseSource.GenerateImpulse(); // 发生脉冲信号
				this.DestroyGameObjGracefully();
			}
		}
	}
}
