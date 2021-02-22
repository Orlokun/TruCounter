using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveImages(ShameDataManager sManager)
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shameData.shame";

        FileStream fStream = new FileStream(path, FileMode.Create);

        bFormatter.Serialize(fStream, sManager);
        fStream.Close();
    }


    public static ShameDataManager LoadShameData()
    {
        string path = Application.persistentDataPath + "/shameData.shame";
        if (File.Exists(path))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream fStream = new FileStream(path, FileMode.Open);

            ShameDataManager shameData = (ShameDataManager)bFormatter.Deserialize(fStream);
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
