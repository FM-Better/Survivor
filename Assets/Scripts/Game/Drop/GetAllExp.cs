using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class GetAllExp : ViewController
	{
		[Header("掉落物移动速度")]
		[SerializeField] private float moveSpeed;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				var expBalls = FindObjectsByType<ExpBall>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (var expBall in expBalls)
				{
					ActionKit.OnUpdate.Register(() =>
					{
						if (Player.Default)
						{
							var direction = expBall.NormalizedDirection2DTo(Player.Default);
							expBall.transform.Translate(direction * (moveSpeed * Time.deltaTime));	
						}
					}).UnRegisterWhenGameObjectDestroyed(expBall);
				}
				
				AudioKit.PlaySound(Sound.GETALLEXP);
				this.DestroyGameObjGracefully();
			}
		}
	}
}
