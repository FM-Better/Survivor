using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	// Generate Id:12ffc78e-81c0-450f-b7a8-1291c8ade682
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TxtLv;
		[SerializeField]
		public UnityEngine.UI.Text TxtTimer;
		[SerializeField]
		public UnityEngine.UI.Text TxtEnemy;
		[SerializeField]
		public UnityEngine.UI.Text TxtGold;
		[SerializeField]
		public ExpUpgradePanel ExpUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Image ExpValue;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TxtLv = null;
			TxtTimer = null;
			TxtEnemy = null;
			TxtGold = null;
			ExpUpgradePanel = null;
			ExpValue = null;
			
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
