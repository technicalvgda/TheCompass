﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassageNode
{
    #region Variable Declaration
    //This is flagged in the GetDecision() function
    public bool isVisited;

    //Private fields obtained via regex.
    //Position currently has no use.
    int _passageID;
    string _name;
    string _tags;
    string _position;
    string _content;

    //The TwineDialogue that holds this PassageNode.
    TwineDialogue _parent;

    //Contains all the choices (paths to other PassageNodes)
    Dictionary<string, string> _choiceDictionary;
    #endregion

    #region Constructors
    /*
        Basic constructor that sets all the fields of the PassageNode.
        Also initializes the choice dictionary.
    */
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
    #endregion

    #region Add Functions
    /*
        Adds another choice (path to another PassageNode) to the choice
        dictionary
    */
    public void AddChoice(string content, string nodeName)
    {
        _choiceDictionary.Add(content, nodeName);
    }
    #endregion

    #region Get Functions
    /*
        Returns the content text of the Passage.
    */
    public string GetContent()
    {
        return _content;
    }

    /*
        Returns all the keys of the choice dictionary.
        The keys are the strings that are displayed to
        the player.
    */
    public List<string> GetChoices()
    {
        return new List<string>(_choiceDictionary.Keys);
    }

    /*
        Iterates through each choice in a given list and returns a list
        of all choices whose passages have a specific tag.
    */
    public List<string> GetChoicesTagged(List<string> list, string tag)
    {
        List<string> resultList = new List<string>();
        PassageNode pn;
        string choiceString;
        foreach(string s in list)
        {
            if (_choiceDictionary.TryGetValue(s, out choiceString))
            {
                if (_parent.Passage.TryGetValue(choiceString, out pn))
                {
                    if (pn._tags.ToUpper().Equals(tag.ToUpper()))
                    {
                        resultList.Add(s);
                    }
                }
            }
        }
        return resultList;
    }

    /*
        Iterates through all the choices in a given list and returns a list
        of all passages that have isVisited flagged.
    */
    public List<string> GetChoicesVisited(List<string> list)
    {
        List<string> resultList = new List<string>();
        PassageNode pn;
        string choiceString;
        foreach (string s in list)
        {
            if (_choiceDictionary.TryGetValue(s, out choiceString))
            {
                if (_parent.Passage.TryGetValue(choiceString, out pn))
                {
                    if (pn.isVisited)
                    {
                        resultList.Add(s);
                    }
                }
            }
        }
        return resultList;
    }

    /*
        Takes in a choice and returns the corresponding
        PassageNode. Will also trigger the isVisited flag.
    */
    public PassageNode GetDecision(string s)
    {
        PassageNode decision;
        string key;
        if (_choiceDictionary.TryGetValue(s, out key))
        {
            if (_parent.Passage.TryGetValue(key, out decision))
            {
                decision.isVisited = true;
                return decision;
            }
        }
        return null;
    }

    /*
        Same function as GetDecision but will not trigger isVisited
    */
    public PassageNode GetDecisionIncognito(string s)
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
    #endregion
}
