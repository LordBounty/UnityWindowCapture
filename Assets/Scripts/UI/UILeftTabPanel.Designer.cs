using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:a01471f6-e696-4497-9b86-d1688f6e517f
	public partial class UILeftTabPanel
	{
		public const string Name = "UILeftTabPanel";
		
		[SerializeField]
		public UnityEngine.RectTransform Content;
		[SerializeField]
		public leftTabCell leftTabCell;
		[SerializeField]
		public UnityEngine.UI.Button Button_ReStart;
		
		private UILeftTabPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Content = null;
			leftTabCell = null;
			Button_ReStart = null;
			
			mData = null;
		}
		
		public UILeftTabPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UILeftTabPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UILeftTabPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
