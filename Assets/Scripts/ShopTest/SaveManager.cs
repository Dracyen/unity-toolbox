using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public string trackName;

    public SaveDataEditor Editor;

    public void SaveFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path;
        path = Application.persistentDataPath + "/" + trackName + ".fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        Editor.saveData.OnBeforeSerialize();

        formatter.Serialize(stream, Editor.saveData.serializableData);
        stream.Close();

        Debug.Log("A file was created at " + path);
    }

    public void LoadFile()
    {
        string path = Application.persistentDataPath + "/" + trackName + ".fun";

        SerializableSaveData data;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            stream.Position = 0;

            data = (SerializableSaveData)formatter.Deserialize(stream);

            Editor.saveData.serializableData = data;

            Editor.saveData.OnAfterDeserialize();

            Debug.Log("The file at " + path + " was loaded");
        }
    }
}
