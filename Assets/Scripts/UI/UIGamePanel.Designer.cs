using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Survivor
{
	// Generate Id:19092949-0f0b-439e-9e83-a9004bc1e062
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TxtHp;
		[SerializeField]
		public UnityEngine.UI.Text TxtExp;
		[SerializeField]
		public UnityEngine.UI.Text TxtLv;
		[SerializeField]
		public UnityEngine.UI.Text TxtTimer;
		[SerializeField]
		public UnityEngine.UI.Text TxtEnemy;
		[SerializeField]
		public UnityEngine.UI.Text TxtGold;
		[SerializeField]
		public RectTransform UpgradeRoot;
		[SerializeField]
		public UnityEngine.UI.Button BtnSimpleDamage;
		[SerializeField]
		public UnityEngine.UI.Button BtnSimpleCD;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TxtHp = null;
			TxtExp = null;
			TxtLv = null;
			TxtTimer = null;
			TxtEnemy = null;
			TxtGold = null;
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
