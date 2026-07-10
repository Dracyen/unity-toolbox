using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TiledPattern : MonoBehaviour
{
    public int patternSize = 1;

    public Material material;

    void Update()
    {
        material.mainTextureScale = new Vector2(transform.localScale.x / patternSize, transform.localScale.z / patternSize);
    }
}
