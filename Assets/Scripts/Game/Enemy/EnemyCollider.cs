using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class EnemyCollider : ViewController
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
