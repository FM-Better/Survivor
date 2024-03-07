using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	// Generate Id:ecea1235-d2fa-46d8-9385-15541aa4f277
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TxtExp;
		[SerializeField]
		public UnityEngine.UI.Text TxtLv;
		[SerializeField]
		public UnityEngine.UI.Text TxtTimer;
		[SerializeField]
		public UnityEngine.UI.Text TxtEnemy;
		[SerializeField]
		public RectTransform UpgradeRoot;
		[SerializeField]
		public UnityEngine.UI.Button BtnSimpleDamage;
		[SerializeField]
		public UnityEngine.UI.Button BtnSimpleCD;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TxtExp = null;
			TxtLv = null;
			TxtTimer = null;
			TxtEnemy = null;
			UpgradeRoot = null;
			BtnSimpleDamage = null;
			BtnSimpleCD = null;
			
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
