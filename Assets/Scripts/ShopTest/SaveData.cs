using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SerializableSaveData
{
    public float[] Vector3;
    public float[] Vector2;
    public float LayerMask;

    public Vector3 newVector3;
}

public class SaveData
{
    public Vector3 vector3 = Vector3.zero;

    public Vector2 vector2 = Vector2.zero;

    public LayerMask layerMask;

    public Vector3 newVector3 = Vector3.zero;

    public SerializableSaveData serializableData;

    public void OnBeforeSerialize()
    {
        serializableData = new SerializableSaveData();

        serializableData.Vector3 = new float[3];
        serializableData.Vector3[0] = vector3.x;
        serializableData.Vector3[1] = vector3.y;
        serializableData.Vector3[2] = vector3.z;

        serializableData.Vector2 = new float[2];
        serializableData.Vector2[0] = vector2.x;
        serializableData.Vector2[1] = vector2.y;

        serializableData.LayerMask = layerMask;

        serializableData.newVector3 = newVector3;
    }

    public void OnAfterDeserialize()
    {
        vector3.x = serializableData.Vector3[0];
        vector3.y = serializableData.Vector3[1];
        vector3.z = serializableData.Vector3[2];


        vector2.x = serializableData.Vector2[0];
        vector2.y = serializableData.Vector2[1];

        layerMask = (LayerMask)serializableData.LayerMask;

        newVector3 = serializableData.newVector3;
    }
}
