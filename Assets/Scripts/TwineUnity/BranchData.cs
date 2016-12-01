using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

[Serializable]
public class BranchData {
    //TODO: Actually increment EnemiesKilled when enemy is killed.
    public int EnemiesKilled = 0;
    public bool ColorVisited = false;
    public int CharismaticCounter = 0;
    public int LeaveCounter = 0;
    [NonSerialized]
    public static BranchData Singleton = new BranchData();
}
