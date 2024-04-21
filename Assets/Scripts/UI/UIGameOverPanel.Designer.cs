using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	// Generate Id:72b12f24-e27d-4247-84f3-34ce57ffe751
	public partial class UIGameOverPanel
	{
		public const string Name = "UIGameOverPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnBackStart;
		[SerializeField]
		public UnityEngine.UI.Button BtnTest;
		
		private UIGameOverPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnBackStart = null;
			BtnTest = null;
			
			mData = null;
		}
		
		public UIGameOverPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameOverPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameOverPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
