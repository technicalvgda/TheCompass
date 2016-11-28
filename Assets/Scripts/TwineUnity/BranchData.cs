using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System;

[Serializable]
public class BranchData {
    //TODO: Actually increment EnemiesKilled when enemy is killed.
    public int EnemiesKilled = 0;
    public bool ColorVisited = false;
    [NonSerialized]
    public static BranchData Singleton = new BranchData();
}
