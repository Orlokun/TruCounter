﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void FormatAndSaveShameData(ShameManager sManager)
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shameBytes.dog";

        FileStream fStream = new FileStream(path, FileMode.Create);

        bFormatter.Serialize(fStream, sManager.GetShameData());
        fStream.Close();
    }


    public static ShameData LoadShameData()
    {
        string path = Application.persistentDataPath + "/shameBytes.dog";
        if (File.Exists(path))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream fStream = new FileStream(path, FileMode.Open);

            ShameData shameData = (ShameData)bFormatter.Deserialize(fStream);
            fStream.Close();
            return shameData;
        }
        else 
        {
            Debug.LogError("Save file not found in " + path + "");
            return null;
        }
    }
}
