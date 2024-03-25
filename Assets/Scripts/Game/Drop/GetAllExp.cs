using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class GetAllExp : GamePlayObject
	{
		[Header("掉落物移动速度")]
		[SerializeField] private float moveSpeed;

		protected override Collider2D collider => selfCollider;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				var expBalls = FindObjectsByType<ExpBall>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (var expBall in expBalls)
				{
					var expCache = expBall;
					ActionKit.OnUpdate.Register(() =>
					{
						if (Player.Default)
						{
							var direction = expCache.NormalizedDirection2DTo(Player.Default);
							expCache.transform.Translate(direction * (moveSpeed * Time.deltaTime));	
						}
					}).UnRegisterWhenGameObjectDestroyed(expCache);
				}
				
				var golds = FindObjectsByType<Gold>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (var gold in golds)
				{
					var goldCache = gold;
					ActionKit.OnUpdate.Register(() =>
					{
						if (Player.Default)
						{
							var direction = goldCache.NormalizedDirection2DTo(Player.Default);
							goldCache.transform.Translate(direction * (moveSpeed * Time.deltaTime));	
						}
					}).UnRegisterWhenGameObjectDestroyed(goldCache);
				}
				
				AudioKit.PlaySound(Sound.GETALLEXP);
				this.DestroyGameObjGracefully();
			}
		}
	}
}
