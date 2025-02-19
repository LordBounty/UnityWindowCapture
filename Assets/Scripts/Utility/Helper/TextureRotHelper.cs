﻿
    using UnityEngine;

    public class TextureRotHelper
    {
        public static Texture2D RoateTextureUpDown180(Texture2D source)
        {
            // Texture2D texture2D = new Texture2D(source.width, source.height);
            Texture2D texture2D =CopyT2DToWrite(source);
       
            for (int w = 0; w < source.width; w++)
            {
                for (int h = 0; h < source.height; h++)
                {
                    Color32 color32 = source.GetPixel(w, source.height - 1 - h);
                    texture2D.SetPixel(w, h, color32);
                }
            }

            texture2D.Apply();
            return texture2D;
        }
        // Copy out a readable Texture2D 
        public static Texture2D CopyT2DToWrite(Texture2D source)
        {
       
            RenderTexture renderTex = RenderTexture.GetTemporary(
                source.width,
                source.height);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            // Copy into a new Texture2D
            Texture2D readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            // resume_release RenderTexture
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }
    }
