using UnityEngine;
using System.Collections.Generic;

public class BranchHandler : MonoBehaviour {
    public string ColorChangeTag = "FlagColor";
    public string CounterTag = "Counter";
    public string LeaveTag = "Leave";
    public string CharismaticTag = "Charismatic";
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
        }
        for(int i = 0; i < tags.Count; i++)
        {
            if (tags[i].Equals(CounterTag) && i < tags.Count - 2)
            {
                int increment;
                if (int.TryParse(tags[i+2], out increment))
                {
                    if (tags[i+1].Equals(LeaveTag))
                    {
                        BranchData.Singleton.LeaveCounter += increment;
                    }
                    else if (tags[i+1].Equals(CharismaticTag))
                    {
                        BranchData.Singleton.CharismaticCounter += increment;
                    }
                }
            }
        }
    }
}
