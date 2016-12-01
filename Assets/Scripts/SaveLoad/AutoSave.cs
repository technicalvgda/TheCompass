using UnityEngine;
using System.Collections;

public class AutoSave : MonoBehaviour {
	void Start () {
        GameData gd = SaveLoad.LoadGame();
		BranchData.Singleton = gd != null? gd.Branch != null ? gd.Branch : BranchData.Singleton : new BranchData();
        //TODO: DELETE GAME DATA WHEN PLAYER BEATS GAME.
	}
}
