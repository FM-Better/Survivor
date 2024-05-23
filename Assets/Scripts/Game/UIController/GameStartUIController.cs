using QFramework;
using UnityEngine;

namespace Survivor
{
	public partial class GameStartUIController : ViewController
	{
		void Start()
		{
			UIKit.OpenPanel<UIGameStartPanel>();
		}

		private void OnDestroy()
		{
			UIKit.ClosePanel<UIGameStartPanel>();
		}
	}
}
