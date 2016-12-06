using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


public class SaveLoad : MonoBehaviour {
    public static SaveLoad Singleton;
    public static string defaultFilePath = "SaveData.dat";
    public static string[] SceneExceptions = { "MainMenu", "Logo", "LoadingScreenScene" };
    public static List<string> SceneExceptionList = new List<string>(SceneExceptions);

    public static void SaveGame()
    {
        SaveFile(new GameData(), defaultFilePath);
    }
    public static void SaveGameWithScene(string NextScene)
    {
        if (SceneExceptionList.Contains(NextScene)) return;
        Debug.Log("SaveGameWithScene -- " + NextScene);
        GameData game = SaveLoad.LoadGame();
        game = game == null ? new GameData() : game;
        game.Scene = NextScene;
        Debug.Log(NextScene);
        SaveGame(game);
    }
    public static void SaveGame(GameData game)
    {
        SaveFile(game, defaultFilePath);
    }
    public static GameData LoadGame()
    {
        return (GameData)LoadFile(defaultFilePath);
    }
    public static void SaveFile(object data, string filePath)
    {
        GameData gd = (GameData)data;
        PlayerPrefs.SetString("scene", gd.Scene);
        if (BranchData.Singleton != null)
        {
            PlayerPrefs.SetInt("enemieskilled", BranchData.Singleton.EnemiesKilled);
            PlayerPrefs.SetInt("colorvisited", BranchData.Singleton.ColorVisited ? 1 : 0);
            PlayerPrefs.SetInt("charismatic", BranchData.Singleton.CharismaticCounter);
            PlayerPrefs.SetInt("leave", BranchData.Singleton.LeaveCounter);
            }
        /*Stream stream = File.Open(filePath, FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Serialize(stream, data);
        stream.Close();
        */
    }
    public static object LoadFile(string filePath)
    {
        GameData gd = new GameData();
        gd.Scene = PlayerPrefs.GetString("scene");
        BranchData.Singleton = BranchData.Singleton == null ? new BranchData() : BranchData.Singleton;
        BranchData.Singleton.EnemiesKilled = PlayerPrefs.GetInt("enemiesKilled");
        BranchData.Singleton.ColorVisited = PlayerPrefs.GetInt("colorVisited") == 1 ? true : false;
        BranchData.Singleton.CharismaticCounter = PlayerPrefs.GetInt("charismatic"); 
        BranchData.Singleton.LeaveCounter = PlayerPrefs.GetInt("leave");
        return gd;
    }

        /*
        object data = null;
        try
        {
            if (File.Exists(defaultFilePath))
            {
                Stream stream = File.Open(filePath, FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                data = bformatter.Deserialize(stream);
                stream.Close();
            }
        }
        catch (IOException)
        {
            return null;
        }
        return data;
    }
    */
    public void Start()
    {
        Singleton = this;
    }
    
}
