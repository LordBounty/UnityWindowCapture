using Events;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using uWindowCapture;

namespace QFramework.Example
{
	public class UILeftTabPanelData : UIPanelData
	{
	}
	public partial class UILeftTabPanel : UIPanel
	{
		protected override void ProcessMsg(int eventId, QMsg msg)
		{
			throw new System.NotImplementedException();
		}
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UILeftTabPanelData ?? new UILeftTabPanelData();
			// please add init code here
			Button_ReStart.onClick.AddListener(() =>
			{
				//reset selection
				//RayComponent.Instance.ReStartClick();
				TypeEventSystem.Global.Send<EventRestartMeshCreate>();
			});
		}

		
		public void AddLeftTabCell(string windowName, Texture2D texture2D)
		{
			var cell = Instantiate(leftTabCell, Content);
			
			cell.Init(windowName,texture2D);
		}
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
