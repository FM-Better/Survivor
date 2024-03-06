using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	// Generate Id:20000a5e-ec26-4091-8403-0592dfcd57bc
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TxtExp;
		[SerializeField]
		public UnityEngine.UI.Text TxtLv;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgrade;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TxtExp = null;
			TxtLv = null;
			BtnUpgrade = null;
			
			mData = null;
		}
		
		public UIGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
