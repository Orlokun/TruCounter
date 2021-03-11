using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
<<<<<<< HEAD
    public static void FormatAndSaveShameData(ShameManager sManager)
=======
    public static void SaveImages(ShameDataManager sManager)
>>>>>>> f367fcff32ee6afe4e06792db0134f3ee7237185
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shameData.shame";

        FileStream fStream = new FileStream(path, FileMode.Create);

<<<<<<< HEAD
        bFormatter.Serialize(fStream, sManager.GetShameData());
=======
        bFormatter.Serialize(fStream, sManager);
>>>>>>> f367fcff32ee6afe4e06792db0134f3ee7237185
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
