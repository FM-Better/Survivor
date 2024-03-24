/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	public partial class UnLockedIconPanel
	{
		[SerializeField] public UnityEngine.UI.Image ImgIconTemplate;
		[SerializeField] public RectTransform IconRoot;

		public void Clear()
		{
			ImgIconTemplate = null;
			IconRoot = null;
		}

		public override string ComponentName
		{
			get { return "UnLockedIconPanel";}
		}
	}
}
