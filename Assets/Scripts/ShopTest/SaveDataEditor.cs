using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataEditor : MonoBehaviour
{
    public SaveData saveData;

    public Vector3 vector3 = Vector3.zero;

    public Vector2 vector2 = Vector2.zero;

    public LayerMask layerMask;

    public Vector3 newVector3 = Vector3.zero;

    public bool readingValues;

    private void Start()
    {
        saveData = new SaveData();
    }

    private void Update()
    {
        if(readingValues)
        {
            vector3 = saveData.vector3;
            vector2 = saveData.vector2;
            layerMask = saveData.layerMask;
            newVector3 = saveData.newVector3;
        }
        else
        {
            saveData.vector3 = vector3;
            saveData.vector2 = vector2;
            saveData.layerMask = layerMask;
            saveData.newVector3 = newVector3;
        }
    }
}
