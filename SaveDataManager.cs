using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveDataManager
{
    public static void save (SaveData data)
    {
        BinaryFormatter formatt = new BinaryFormatter();
        string path = Path.Combine(Application.dataPath, "data_01.bin");
        FileStream stream = File.Create(path);

        formatt.Serialize(stream, data);
        stream.Close();
    }


    public static SaveData load ()
    {
        string path = Path.Combine(Application.dataPath, "data_01.bin");

        if (File.Exists(path))
        {
            BinaryFormatter formatt = new BinaryFormatter();

            FileStream stream = File.OpenRead(path);

            SaveData data = (SaveData)formatt.Deserialize(stream);
            stream.Close();
            return data;
        }
      else
        {
            Debug.Log("파일이 없어요");
            return null;
        }
    }
}
