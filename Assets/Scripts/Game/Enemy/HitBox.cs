using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class HitBox : ViewController
	{
		public GameObject Owner;
		
		void Start()
		{
			if (!Owner)
			{
				Owner = transform.parent.gameObject;
			}
		}
	}
}
