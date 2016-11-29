using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System;

[Serializable]
public class BranchData {
    public int EnemiesKilled = 0;
    public bool ColorVisited = false;
    [NonSerialized]
    public static BranchData Singleton = new BranchData();
}
