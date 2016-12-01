using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Runtime.Serialization;
[Serializable()]
public class GameData : ISerializable
{
    public static string GameVersion = "1.0.0";
    public string Version;
    public string Scene;
    public BranchData Branch;
            
    public GameData() {}
    
    public GameData(SerializationInfo info, StreamingContext ctxt)
    {
        Version = (string)info.GetValue("Version", typeof(string));
        Scene = (string)info.GetValue("Scene", typeof(string));
        Branch = (BranchData)info.GetValue("Branch", typeof(BranchData));
    }
    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
        info.AddValue("Version", GameVersion);
        info.AddValue("Scene", Scene);
        info.AddValue("Branch", BranchData.Singleton);
    }
}
