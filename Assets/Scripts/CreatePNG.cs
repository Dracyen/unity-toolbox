using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CreatePNG : MonoBehaviour
{
    [SerializeField]Camera cam;
    [SerializeField]Image img;
    [SerializeField]int fileCounter;

    public void CreateImage()
    {
        var rTex = cam.targetTexture;

        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);

        RenderTexture.active = rTex;

        Rect screenshotTexRect = new Rect(0, 0, rTex.width, rTex.height);

        tex.ReadPixels(screenshotTexRect, 0, 0);
        tex.Apply();

        var renderedSprite = Sprite.Create(tex, screenshotTexRect, new Vector2(0.5f, 0.5f));

        img.sprite = renderedSprite;

        byte[] bytes = tex.EncodeToPNG();

        File.WriteAllBytes(Application.dataPath + "/Backgrounds/" + fileCounter + ".png", bytes);
        fileCounter++;
    }
}
