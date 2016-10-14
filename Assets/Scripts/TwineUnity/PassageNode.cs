using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassageNode
{
    public bool isVisited;
    int _passageID;
    string _name;
    string _tags;
    string _position;
    string _content;
    TwineDialogue _parent;

    Dictionary<string, string> _choiceDictionary;

    public PassageNode(TwineDialogue parent, int pid, string name, string tags, string position, string content)
    {
        _parent = parent;
        _passageID = pid;
        _name = name;
        _tags = tags;
        _position = position;
        _content = content;
        isVisited = false;
        _choiceDictionary = new Dictionary<string, string>();
    }
    public string GetContent()
    {
        return _content;
    }
    public void AddChoice(string content, string nodeName)
    {
        _choiceDictionary.Add(content, nodeName);
    }
    /*
        Somewhat complicated nested if's, but basically:

        If the choice node is tagged "See Once", it will
        check to see if it was already visited. If it is
        not visited, it will add it to the list of available
        options.

        If it's not tagged, it's added regardless of whether it
        was visited or not.
    */
    public List<string> GetChoices()
    {
        List<string> choiceKeyList = new List<string>(_choiceDictionary.Keys);
        List<string> choiceList = new List<string>();
        PassageNode tempNode = null;
        string tempKey = "";
        foreach (string key in choiceKeyList)
        {
            if (_choiceDictionary.TryGetValue(key, out tempKey))
            {
                if (_parent.Passage.TryGetValue(tempKey, out tempNode))
                {
                    if (tempNode.GetTag().Equals(_parent.SeeOnceTag))
                    {
                        if (!tempNode.isVisited)
                        {
                            choiceList.Add(key);
                        }
                        else
                        {
                            Debug.Log("Node was already visited!");
                        }
                    }
                    else
                    {
                        choiceList.Add(key);
                    }
                }
            }
        }
        return choiceList;
    }

    public PassageNode GetDecision(string s)
    {
        PassageNode decision;
        string key;
        if (_choiceDictionary.TryGetValue(s, out key))
        {
            if (_parent.Passage.TryGetValue(key, out decision))
            {
                return decision;
            }
        }
        return null;
    }
    public string GetTag()
    {
        return _tags;
    }
}
