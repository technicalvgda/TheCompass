using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour {
    public static SaveLoad Singleton;
    public static void SaveFile(object data, string filePath)
    {
        Stream stream = File.Open(filePath, FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Serialize(stream, data);
        stream.Close();
    }
    public static object LoadFile(string filePath)
    {
        object data = null;
        try
        {
            Stream stream = File.Open(filePath, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            data = bformatter.Deserialize(stream);
            stream.Close();
        }
        catch(IOException)
        {
            return null;
        }
        return data;
    }
    public void Start()
    {
        Singleton = this;
    }
}
