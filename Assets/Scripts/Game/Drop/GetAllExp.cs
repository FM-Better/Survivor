using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class GetAllExp : ViewController
	{
		private Transform playerTrans;
		[Header("掉落物移动速度")]
		[SerializeField] private float moveSpeed;
		
		private void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<PickUpArea>())
			{
				var expBalls = FindObjectsByType<ExpBall>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (var expBall in expBalls)
				{
					ActionKit.OnUpdate.Register(() =>
					{
						if (playerTrans)
						{
							var direction = (playerTrans.position - expBall.transform.position).normalized;
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
