using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class MenuTransitionSavedData : MenuTransition
{
    public Menu nextNew, nextContinue;

    public override Menu next
    {
        get
        {
			bool hasSavedData = File.Exists(SaveLoad.defaultFilePath);
            //Debug.Log("hasSavedData = " + hasSavedData);
           // bool hasSavedData = true;

            return !hasSavedData ? nextNew : nextContinue;
        }
    }
}
