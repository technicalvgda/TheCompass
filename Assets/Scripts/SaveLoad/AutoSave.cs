using UnityEngine;
using System.Collections;

public class AutoSave : MonoBehaviour {
    public static string defaultFilePath = "SaveData.bin";
	void Start () {
        GameData gd = (GameData)SaveLoad.LoadFile(defaultFilePath);
        BranchData.Singleton = gd != null ? gd.Branch : new BranchData();
        SaveLoad.SaveFile(new GameData(), defaultFilePath);
	}
}
