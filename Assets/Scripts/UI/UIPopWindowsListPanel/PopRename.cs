/****************************************************************************
 * 2022.5 DESKTOP-CEJCKJU
 ****************************************************************************/

using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using uWindowCapture;

namespace QFramework.Example
{
	public partial class PopRename : UIElement
	{
		public UwcWindow window;
		public Texture2D texture2D;
		private void Awake()
		{
			Button.onClick.AddListener(() =>
			{
				//window.title = InputHereName.text;
				Debug.Log($"重命名成功 {InputEnterName.text} to {InputHereName.text}");

				UILeftTabPanel leftTabPanel = UIKit.GetPanel<UILeftTabPanel>();
				if (leftTabPanel)
				{
					leftTabPanel.AddLeftTabCell(InputHereName.text,texture2D);
				}
				else
				{
					UIKit.OpenPanel<UILeftTabPanel>().AddLeftTabCell(InputHereName.text,texture2D);
				}
				//改变播放面板的贴图
				// SelectComponent.Instance.DisplayPanel.SetDisPlayTexture(InputHereName.text,texture2D);
				TypeEventSystem.Global.Send<EventChangeDisPlayMatTexture>(new EventChangeDisPlayMatTexture(){windowName = InputHereName.text,texture2D = window.texture,Window = this.window});
				// UIKit.ClosePanel<UIPopWindowsListPanel>();
				UIKit.GetPanel<UIPopWindowsListPanel>().Scale(Vector3.zero);
				gameObject.SetActive(false);
			});
			BtnClose.onClick.AddListener(()=>{
				gameObject.SetActive(false);
			});
		}

		public void Init( string windowName,Texture2D texture2D,UwcWindow window)
		{
			this.window = window;
			gameObject.SetActive(true);
			//this.window = window;
			this.texture2D = texture2D;
			InputEnterName.text = windowName;
		}
		
		protected override void OnBeforeDestroy()
		{
		}
	}
}