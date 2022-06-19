using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using QFramework.Example;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using uWindowCapture;
using Color = UnityEngine.Color;

public class ScreenCellPre : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Image imgBg;
    private Button btn;
    public RawImage imgWindow;
    public TMP_Text screenName;

    private Color defaultColor;
    private Color outlineColor=Color.cyan;
    public UwcWindow window;
     bool showTextureSucceed;

    private float timer = 0.2f;

    private UIPopWindowsListPanel uiPopWindowsListPanel;
    // Start is called before the first frame update
    void Awake()
    {
        btn = GetComponent<Button>();
        imgBg = GetComponent<Image>();
        defaultColor = imgBg.color;
    }

    public void Init(UIPopWindowsListPanel uiPopWindowsListPanel,UwcWindow window)
    {
        this.uiPopWindowsListPanel = uiPopWindowsListPanel;
        this.window = window;
        
        screenName.text = window.title;
        btn.onClick.AddListener(OnBtnClickRename);
        
    }

    private void OnBtnClickRename()
    {
        //TODO 复制一份贴图
        Texture2D source = imgWindow.texture as Texture2D;
       
        //Color32[] pixesl=window.GetPixels(0, 0, window.width, window.height);
        // var sp=Sprite.Create(source, new Rect(0, 0, source.width, source.height), new Vector2(0,0));
        // Texture2D result = new Texture2D(source.width, source.height);
        // Graphics.CopyTexture(source, result);
        //result.SetPixels32(pixesl);
        //var data = source.GetRawTextureData();
        //Color[] rpixels=source.GetPixels(0,0,source.width,source.height);
        //Debug.Log($"{result.width} {result.height} "+"color "+rpixels.Length);
       
        // if (SystemInfo.copyTextureSupport == UnityEngine.Rendering.CopyTextureSupport.None)
        // {
        //     //High GC allocs here
        //     Debug.Log("SetPixels");
        //     Color[] pixelBuffer = source.GetPixels(0, 0, result.width, result.height);
        //     result.SetPixels(pixelBuffer);
        //     result.Apply();
        // }
        // else
        // {
        //     Debug.Log("CopyTexture");
        //
        //     Graphics.CopyTexture(source, 0, 0,0, 0, result.width, result.height, result, 0, 0, 0, 0);
        // }
       // Debug.Log("SetPixels");
        // Color[] pixelBuffer = source.GetPixels(0, 0, source.width, source.height);
        //
        // for (int i = 0; i < 100; i++)
        // { 
        //     Debug.Log(pixelBuffer[i]);
        //     
        // }
        // result.SetPixels(pixelBuffer);
        // result.Apply();
        
        //result.LoadRawTextureData(data);
        // //将贴图旋转180
        // Texture2D texture = new Texture2D(result.width, result.height);
        //
        // for (int w = 0; w < result.width; w++)
        // {
        //     for (int h = 0; h < result.height; h++)
        //     {
        //         var color=result.GetPixel( result.width - w, result.height - h);
        //         texture.SetPixel(w,h,color);
        //     }
        // }
        // texture.Apply();
        
        //uiPopWindowsListPanel.PopRenamePanel(window.title,texture);
        
        uiPopWindowsListPanel.PopRenamePanel(window.title,TextureRotHelper.RoateTextureUpDown180(source),window);
    }
    

   
    

    void Update()
    {
        if (window!=null&& !showTextureSucceed)
        {
            this.window.RequestCapture();
           // Debug.Log("windwo  不为空");
        }
        if (window != null&& window.texture&& !showTextureSucceed)
        {
            imgWindow.texture = TextureRotHelper.CopyT2DToWrite(window.texture);
            timer -= Time.deltaTime;
            if (timer<=0)
            {
                //Debug.Log(window.texture.width+" "+window.texture.height);
                showTextureSucceed = true;

            }
        }
        
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveListener(OnBtnClickRename);
        window = null;
    }

    /// <summary>
/// 鼠标进入
/// </summary>
/// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        imgBg.color=outlineColor;
    }
/// <summary>
/// 鼠标离开
/// </summary>
/// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        imgBg.color=defaultColor;

    }
}
