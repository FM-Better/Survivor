using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	// Generate Id:7a621cc0-e09b-4a92-8835-0c1372f94408
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnStart;
		[SerializeField]
		public UnityEngine.UI.Image GlodUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Text TxtGold;
		[SerializeField]
		public UnityEngine.UI.Button BtnClose;
		[SerializeField]
		public UnityEngine.UI.Button BtnExpUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnGoldUpgrade;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnUpgrade = null;
			BtnStart = null;
			GlodUpgradePanel = null;
			TxtGold = null;
			BtnClose = null;
			BtnExpUpgrade = null;
			BtnGoldUpgrade = null;
			
			mData = null;
		}
		
		public UIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
