using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Basketball : ViewController
	{
		private List<Ball> mBalls = new List<Ball>();
		
		void Start()
		{
			Global.BasketBallCount.Or(Global.AdditionalFlyCount).Register(() =>
			{
				CreatBalls();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			CreatBalls();
		}

		void CreatBalls()
		{
			var toAddCount = Global.BasketBallCount.Value + Global.AdditionalFlyCount.Value - mBalls.Count;
			for (int i = 0; i < toAddCount; i++)
			{
				mBalls.Add(
					Ball.Instantiate()
						.SyncPositionFrom(this)
						.Show());
			}
		}
	}
}
