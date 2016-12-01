using UnityEngine;
using System.Collections.Generic;

public class BranchHandler : MonoBehaviour {
    public string ColorChangeTag = "FlagColor";
	// Use this for initialization
	void OnEnable () {
        TwineDialogue.OnChange += PassageCheck;
	}
	void OnDisable () {
        TwineDialogue.OnChange -= PassageCheck;
	}
    void PassageCheck()
    {
        List<string> tags = TwineDialogue.Singleton.CurrentPassage.GetTags();
        if (tags.Contains(ColorChangeTag))
        {
            BranchData.Singleton.ColorVisited = true;
            SaveLoad.SaveGame();
        }
    }
}
