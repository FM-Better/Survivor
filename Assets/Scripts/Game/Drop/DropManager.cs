using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class DropManager : ViewController
	{
		public static DropManager Default;

		private void Awake()
		{
			Default = this;
		}

		private void OnDestroy()
		{
			Default = null;
		}
	}
}
