using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	// Generate Id:9125e74b-c709-4089-92ca-ea017ebd14e6
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
		[SerializeField]
		public UnityEngine.UI.Button BtnMaxHpUpgrade;
		
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
			BtnMaxHpUpgrade = null;
			
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
