using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class PickUpArea : ViewController
	{
		private void Start()
		{
			Global.PickUpAreaRange.RegisterWithInitValue(range =>
			{
				GetComponent<CircleCollider2D>().radius = range;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
	}
}
