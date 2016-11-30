using UnityEngine;
using System.Collections;

public class AutoSave : MonoBehaviour {
	void Start () {
        GameData gd = SaveLoad.LoadGame();
		BranchData.Singleton = gd != null? gd.Branch : BranchData.Singleton;
        SaveLoad.SaveGame();
        //TODO: DELETE GAME DATA WHEN PLAYER BEATS GAME.
	}
}
