using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using QFramework;
using UnityEngine;
using uWindowCapture;

public class CreateNewMeshCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    private Material _material;
    private bool isDisplay = false;
    public UwcWindow uwcWindow;

    public float time=0;
    public float timer = 0.05f;
    private string windowname;
    void Start()
    {
        TypeEventSystem.Global.Register<EventCaptureDisPlay>(OnCaptureDisPlay);
        TypeEventSystem.Global.Register<EventChangeDisPlayMatTexture>(OnChangeDisPlayMatTextureHandler);
        TypeEventSystem.Global.Register<EventClearTexture>(OnClearTextureHandler);
        TypeEventSystem.Global.Register<EventHideCubeWindow>(OnHideCubeWindowHander);
        TypeEventSystem.Global.Register<EventShowCubeWindow>(OnShowCubeWindowHander);
        _material = GetComponent<MeshRenderer>().material;
    }
    private void OnHideCubeWindowHander(EventHideCubeWindow obj)
    {
        FindObjectOfType<CreateNewMeshCtrl>().gameObject.GetComponent<Renderer>().enabled = false;
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("ClickTag"))
        {
            o.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnShowCubeWindowHander(EventShowCubeWindow obj)
    {
        FindObjectOfType<CreateNewMeshCtrl>().gameObject.GetComponent<Renderer>().enabled = true;
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("ClickTag"))
        {
            o.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnClearTextureHandler(EventClearTexture obj)
    {
        if (windowname==obj.windowName)
        {
            isDisplay = false;
            _material.mainTexture = null;
            // this.Delay(0.1f, () =>
            // {
            //     
            // });
        }
    }
    

    private void OnCaptureDisPlay(EventCaptureDisPlay obj)
    {
        //Flip screenshot upside down
        isDisplay = false;
        windowname = obj.windowName;
        _material.mainTexture = TextureRotHelper.RoateTextureUpDown180(TextureRotHelper.CopyT2DToWrite(obj.texture2D));
    }
    private void OnChangeDisPlayMatTextureHandler(EventChangeDisPlayMatTexture obj)
    {
        isDisplay = true;
        uwcWindow = obj.Window;
        windowname = obj.windowName;
        _material.mainTexture = obj.texture2D;
    }
    // Update is called once per frame
    void Update()
    {
        if (isDisplay)
        {
            time -= Time.deltaTime;
            if (time<=0)
            {
                
                if (uwcWindow!=null)
                {
                    this.uwcWindow.RequestCapture();
                    // Debug.Log("windwo  No NULL");
                }
                if (uwcWindow != null&& uwcWindow.texture)
                {
                    // _material.mainTexture = TextureRotHelper.CopyT2DToWrite(uwcWindow.texture);
                    _material.mainTexture = uwcWindow.texture;
               
                }

                time = timer;
            }
           
        }
    }

    private void OnDestroy()
    {
        TypeEventSystem.Global.UnRegister<EventCaptureDisPlay>(OnCaptureDisPlay);
        TypeEventSystem.Global.UnRegister<EventChangeDisPlayMatTexture>(OnChangeDisPlayMatTextureHandler);
        TypeEventSystem.Global.UnRegister<EventClearTexture>(OnClearTextureHandler);
        TypeEventSystem.Global.UnRegister<EventHideCubeWindow>(OnHideCubeWindowHander);
        TypeEventSystem.Global.UnRegister<EventShowCubeWindow>(OnShowCubeWindowHander);

    }
}
