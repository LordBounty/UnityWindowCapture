using System.Collections;
using System.Collections.Generic;
using Events;
using ImageCropperNamespace;
using QFramework;
using UnityEngine;

public class ClickDisplayPanel : MonoSingleton<ClickDisplayPanel>
{
    private MeshRenderer meshRenderer;
    public ImageCropperDemo imageCropperDemo;

    public string windName;
    public void SetDisPlayTexture(string windowName,Texture2D texture2D)
    {
        windName = windowName;
        meshRenderer.material.color = Color.white;
        meshRenderer.material.mainTexture = texture2D;
    }

    public void SelectDisPlay()
    {
        meshRenderer.material.color = Color.red;
        meshRenderer.material.mainTexture = null;

    }
    //public bool allowClick; 
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
      //  meshRenderer.material.color = Color.white;
    }
    // private void OnMouseDown()
    // {
    //     if (meshRenderer.material.mainTexture!=null)
    //     {
    //         Crop();
    //     }
    //    
    // }

    public void Crop(string windoName,Texture2D texture2D)
    {
        imageCropperDemo.croppedImageHolder.texture = texture2D;
        windName = windoName;
        StartCoroutine(Capture());
    }

    IEnumerator Capture()
    {
        bool ovalSelection = imageCropperDemo.ovalSelectionInput.isOn;
        bool autoZoom = imageCropperDemo.autoZoomInput.isOn;
        float minAspectRatio, maxAspectRatio;
        if( !float.TryParse( imageCropperDemo.minAspectRatioInput.text, out minAspectRatio ) )
            minAspectRatio = 0f;
        if( !float.TryParse( imageCropperDemo.maxAspectRatioInput.text, out maxAspectRatio ) )
            maxAspectRatio = 0f;
        yield return new WaitForEndOfFrame();

        ImageCropper.Instance.Show( imageCropperDemo.croppedImageHolder.texture, ( bool result, Texture originalImage, Texture2D croppedImage ) =>
            {
                // Destroy previously cropped texture (if any) to free memory
                //Destroy( imageCropperDemo.croppedImageHolder.texture, 5f );

                // If screenshot was cropped successfully
                if( result )
                {
                    // Assign cropped texture to the RawImage
                    imageCropperDemo.croppedImageHolder.enabled = true;
                    imageCropperDemo.croppedImageHolder.texture = croppedImage;

                    Vector2 size = imageCropperDemo.croppedImageHolder.rectTransform.sizeDelta;
                    if( croppedImage.height <= croppedImage.width )
                        size = new Vector2( 400f, 400f * ( croppedImage.height / (float) croppedImage.width ) );
                    else
                        size = new Vector2( 400f * ( croppedImage.width / (float) croppedImage.height ), 400f );
                    imageCropperDemo.croppedImageHolder.rectTransform.sizeDelta = size;

                    imageCropperDemo.croppedImageSize.enabled = true;
                    imageCropperDemo.croppedImageSize.text = "Image size: " + croppedImage.width + ", " + croppedImage.height;

                    // Rotate the texture by 180
                    // Texture2D texture = new Texture2D(croppedImage.width, croppedImage.height);
                    //
                    // for (int w = 0; w < croppedImage.width; w++)
                    // {
                    //     for (int h = 0; h < croppedImage.height; h++)
                    //     {
                    //         var color=croppedImage.GetPixel( croppedImage.width - w, croppedImage.height - h);
                    //         texture.SetPixel(w,h,color);
                    //     }
                    // }
                    // texture.Apply();

                    TypeEventSystem.Global.Send<EventCaptureDisPlay>(new EventCaptureDisPlay(){windowName = windName,texture2D =croppedImage });
                }
                else
                {
                    imageCropperDemo.croppedImageHolder.enabled = false;
                    imageCropperDemo.croppedImageSize.enabled = false;
                }

                // Destroy the screenshot as we no longer need it in this case
                //Destroy( meshRenderer.material.mainTexture );
                // meshRenderer.material.mainTexture = imageCropperDemo.croppedImageHolder.texture;
                 
            },
            settings: new ImageCropper.Settings()
            {
                ovalSelection = ovalSelection,
                autoZoomEnabled = autoZoom,
                imageBackground = Color.clear, // transparent background
                selectionMinAspectRatio = minAspectRatio,
                selectionMaxAspectRatio = maxAspectRatio,
                //initialOrientation = ImageCropper.Orientation.Rotate180

            },
            croppedImageResizePolicy: ( ref int width, ref int height ) =>
            {
                // uncomment lines below to save cropped image at half resolution
                //width /= 2;
                //height /= 2;
            } );
    }
}
