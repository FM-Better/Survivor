using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class SuperBomb : ViewController
	{
		private float mTimer = 0f;

		private void Update()
		{
			mTimer += Time.deltaTime;
			if (mTimer >= 15)
			{
				mTimer = 0f;
				Bomb.Excute();
			}
		}
	}
}
