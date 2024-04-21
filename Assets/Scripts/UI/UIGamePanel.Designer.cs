using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	// Generate Id:c1ab7680-5ad2-4b14-92c7-805a8646afc6
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
		public UnityEngine.UI.Image ExpValue;
		[SerializeField]
		public ExpUpgradePanel ExpUpgradePanel;
		[SerializeField]
		public TreasurePanel TreasurePanel;
		[SerializeField]
		public UnityEngine.UI.Image ScreenColor;
		[SerializeField]
		public UnLockedIconPanel UnLockedIconPanel;
		[SerializeField]
		public AchievementController AchievementController;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TxtLv = null;
			TxtTimer = null;
			TxtEnemy = null;
			TxtGold = null;
			ExpValue = null;
			ExpUpgradePanel = null;
			TreasurePanel = null;
			ScreenColor = null;
			UnLockedIconPanel = null;
			AchievementController = null;
			
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
