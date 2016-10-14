using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwineDialogue {
    public Dictionary<string, PassageNode> Passage;
    public PassageNode StartPassage;
    public string Name;
    public string SeeOnceTag = "See Once";

    public TwineDialogue(string name, PassageNode startNode)
    {
        Passage = new Dictionary<string, PassageNode>();
        Name = name;
        StartPassage = startNode;
    }
    public TwineDialogue()
    {
        Passage = new Dictionary<string, PassageNode>();
    }
    public TwineDialogue(string name)
    {
        Passage = new Dictionary<string, PassageNode>();
        Name = name;
    }
}
