using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableMaterial : MonoBehaviour
{
    [SerializeField]
    InputField FieldX;

    [SerializeField]
    InputField FieldY;

    [SerializeField]
    Color color;

    [SerializeField]
    Color backgroundColor;

    Texture2D texture;

    [SerializeField]
    Vector2Int pos;

    void Start()
    {
        texture = new Texture2D(10, 10);

        for (int x = 0; x < texture.width; x++)
            for (int y = 0; y < texture.height; y++)
                texture.SetPixel(x, y, backgroundColor);

        texture.Apply();

        pos = new Vector2Int();

        texture.filterMode = FilterMode.Point;

        GetComponent<Renderer>().material.mainTexture = texture;

        FieldX.onValueChanged.AddListener(delegate { ModifyX(); });
        FieldY.onValueChanged.AddListener(delegate { ModifyY(); });
    }

    public void Apply()
    {
        texture.SetPixel(pos.x, pos.y, color);

        texture.Apply();
    }

    public void ModifyX()
    {
        pos.x = int.Parse(FieldX.text);
    }

    public void ModifyY()
    {
        pos.y = int.Parse(FieldY.text);
    }
}