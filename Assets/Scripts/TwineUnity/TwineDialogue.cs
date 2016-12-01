using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwineDialogue {
    #region Variable Declaration
    public static TwineDialogue Singleton;

    public Dictionary<string, PassageNode> Passage;

    public delegate void PassageChange();
    public static event PassageChange OnChange;

    public PassageNode StartPassage;
    public PassageNode CurrentPassage;
    public string Name;
    public string ShowOnceTag = "Show-Once";
    #endregion

    #region Constructors
    public TwineDialogue(string name, PassageNode startNode)
    {
        Singleton = this;
        Passage = new Dictionary<string, PassageNode>();
        Name = name;
        StartPassage = startNode;
        CurrentPassage = StartPassage;
    }
    public TwineDialogue()
    {
        Singleton = this;
        Passage = new Dictionary<string, PassageNode>();
    }
    public TwineDialogue(string name)
    {
        Singleton = this;
        Passage = new Dictionary<string, PassageNode>();
        Name = name;
    }
    #endregion

    #region Main Code
    public PassageNode GetPassage(string s)
    {
        PassageNode pn = null;
        CurrentPassage = Passage.TryGetValue(s, out pn) ? pn : CurrentPassage;
        return pn;
    }
    public void AddPassage(string key, PassageNode pn)
    {
        Passage.Add(key, pn);
    }
    public void SetCurrentPassage(PassageNode pn)
    {
        CurrentPassage = pn;
        if (OnChange != null)
            OnChange();
    }
    public List<PassageNode> GetPassagesTagged(string tag)
    {
        List<PassageNode> result = new List<PassageNode>();
        foreach(PassageNode pn in Passage.Values)
        {
            if (pn.GetTags().Contains(tag))
            {
                result.Add(pn);
            }
        }
        return result;
    }
    #endregion
}
