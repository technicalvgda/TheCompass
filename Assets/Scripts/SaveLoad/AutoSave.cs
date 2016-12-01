using UnityEngine;
using System.Collections;

public class AutoSave : MonoBehaviour {
	void Start () {
        GameData gd = SaveLoad.LoadGame();
        if (gd != null)
            if (gd.Branch != null)
                BranchData.Singleton = gd.Branch;	
        //TODO: DELETE GAME DATA WHEN PLAYER BEATS GAME.
	}
}
