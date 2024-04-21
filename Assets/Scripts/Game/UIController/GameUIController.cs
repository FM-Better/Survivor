using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class GameUIController : ViewController
	{
		void Start()
		{
			UIKit.OpenPanel<UIGamePanel>();
		}

		private void OnDestroy()
		{
			UIKit.ClosePanel<UIGamePanel>();
		}
	}
}
