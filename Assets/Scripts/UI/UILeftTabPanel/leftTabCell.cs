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
	public partial class leftTabCell : UIElement
	{
		public string windowName ;
		public Texture2D texture2D;
		private Button buttonChangeTexture;
		public Button buttonCrop;
		private UwcWindow _window;
		private bool isCrop=false;
		private void Awake()
		{
			buttonChangeTexture = GetComponent<Button>();
			BtnClose.onClick.AddListener(()=>{

				//delete the texture directly
				TypeEventSystem.Global.Send<EventClearTexture>(new EventClearTexture(){windowName = windowName});
				//keep the mesh but not show it
				TypeEventSystem.Global.Send<EventHideCubeWindow>();

				//Delete left panel window
				Destroy(gameObject);
			});

			buttonChangeTexture.onClick.AddListener(() =>
			{
				//SelectComponent.Instance.DisplayPanel.SetDisPlayTexture(windowName,texture2D);
				//TypeEventSystem.Global.Send<EventChangeDisPlayMatTexture>(new EventChangeDisPlayMatTexture(){texture2D = this.texture2D});

				//Show mesh when switching windows
				TypeEventSystem.Global.Send<EventShowCubeWindow>();
				
				if (isCrop)
				{
					TypeEventSystem.Global.Send<EventCaptureDisPlay>(new EventCaptureDisPlay(){windowName = windowName,texture2D =this.texture2D });

				}
				else
				{
					TypeEventSystem.Global.Send<EventChangeDisPlayMatTexture>(new EventChangeDisPlayMatTexture(){windowName = this.windowName,texture2D = _window.texture,Window = this._window});
				}

			});

			buttonCrop.onClick.AddListener(()=>{
				// ClickDisplayPanel.Instance.Crop(windowName,texture2D);
				 CropperController.Instance.Crop(windowName,texture2D);
				 TypeEventSystem.Global.Send(new EventVCameraScrollWhereClampCtrol(){isCtrol = false});
			});
			TypeEventSystem.Global.Register<EventCaptureDisPlay>(OnCaptureDisPlay);
			TypeEventSystem.Global.Register<EventChangeDisPlayMatTexture>(OnChangeDisPlayMatTextureHandler);
		}

		private void OnChangeDisPlayMatTextureHandler(EventChangeDisPlayMatTexture obj)
		{
			if (obj.windowName==windowName)
			{
				_window = obj.Window;

			}
		}

		private void OnCaptureDisPlay(EventCaptureDisPlay obj)
		{
			if (obj.windowName==windowName)
			{
				this.texture2D = obj.texture2D;
				RawImg.texture = obj.texture2D;
				isCrop = true;
			}	
		}

		public void Init(string windowName, Texture2D texture2D)
		{
			gameObject.SetActive(true);
			this.windowName = windowName;
			this.texture2D = texture2D;
			RawImg.texture = texture2D;
			Txtname.text = windowName;
		}
	

		protected override void OnBeforeDestroy()
		{
			TypeEventSystem.Global.UnRegister<EventCaptureDisPlay>(OnCaptureDisPlay);
			TypeEventSystem.Global.UnRegister<EventChangeDisPlayMatTexture>(OnChangeDisPlayMatTextureHandler);

		}
	}
}