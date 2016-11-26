using UnityEngine;
using System.Collections;

public class AutoSave : MonoBehaviour {
    public static string defaultFilePath = "SaveData.bin";
	void Start () {
        SaveLoad.SaveFile(new GameData(), defaultFilePath);	
	}
}
