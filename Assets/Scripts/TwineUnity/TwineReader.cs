using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class TwineReader
{
    #region Patterns
    /*
        Splits the Story data into its name and the starting node.

        KEY:
        1: Name
        2: Start Node Index
    */
    public const string STORY_PATTERN = "<tw-storydata name=\"(.*?)\" startnode=\"([\\d])\"";

    /*
        Splits the Passage into the PassageID (pid), Name, Tags, Position (not relevant for our project,
        but need to separate from other data anyways), and the actual content of the passage (including
        the choices and other data)
    
        KEY:
        1: PID
        2: Name
        3: Tags
        4: Position
        5: Content
    */
    public const string PASSAGE_PATTERN = "<tw-passagedata pid=\"([\\d]*)\" name=\"(.*?)\" tags=\"(.*?)\" position=\"(.*?)\">(.*?)<\\/tw-passagedata>";

    /*
        Grabs the actual content of the passage AFTER it's been parsed through
        PASSAGE_PATTERN

        Use Group 1 from the first iteration ONLY.
    */
    public const string TEXT_PATTERN = "(.*?)(\\((.*?)\\)|\\[\\[(.*?)\\]\\])";
    
    /*
        Grabs choices from the passage before or after it's been parsed through
        PASSAGE_PATTERN

        KEY:
        1: Choice Text Content
        2: Choice Node Name
    */
    public const string CHOICE_PATTERN = "\\[\\[(.*?)\\|(.*?)\\]\\]";

    #endregion
    #region Variable Declaration
    /*
    Holds all the Regex objects for each pattern to avoid redundant object
    creation.
    */
    public static Dictionary<string, Regex> RGX;
    #endregion
    #region Main Code
    /*
    Parses a text according to a pattern (will create a Regex object to parse
    if it doesn't already and store it in the RGX Dictionary)
    */
    public static MatchCollection RegexParse(string pattern, string text)
    {
        RGX = RGX == null ? new Dictionary<string, Regex>() : RGX;
        Regex rgx;
        if (!RGX.TryGetValue(pattern, out rgx))
        {
            rgx = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            RGX.Add(pattern, rgx); 
        }
        return rgx.Matches(text);
    }

    /*
    Parses a Story file from Twine in Harlowe format.
    The data is stored as so:
        1: PID
        2: Name
        3: Tags
        4: Position
        5: Content
    */
    public static TwineDialogue Parse(TextAsset ta)
    {
        string s = ta.text.Replace("&#39;", "\'"); ;
        TwineDialogue result = new TwineDialogue();

        //Getting the Story Data
        GroupCollection storyGroups = RegexParse(STORY_PATTERN, s)[0].Groups;
        result.Name = storyGroups[1].Value;
        string startNodeIndex = storyGroups[2].Value;

        MatchCollection passageMatches = RegexParse(PASSAGE_PATTERN, s);
        foreach(Match pmatch in passageMatches)
        {
            //For each passage, find the regex groups
            GroupCollection passageGroups = pmatch.Groups;
            //Apply the content regex on the passage body
            MatchCollection textMatches = RegexParse(TEXT_PATTERN, passageGroups[5].Value);
            //Grab the content from the passage body
            string contentText = "";
            if (textMatches.Count > 0)
            {
                contentText = textMatches[0].Groups[1].Value;
            }
            else
            {
                contentText = passageGroups[5].Value;
            }
            //Create a node based on the regex groups from the passage regex
            PassageNode newNode = new PassageNode(result, int.Parse(passageGroups[1].Value), passageGroups[2].Value, passageGroups[3].Value, passageGroups[4].Value, contentText);
            //Check if the passage ID is equal to the start node ID of the story
            if (passageGroups[1].Value.Equals(startNodeIndex))
            {
                result.StartPassage = newNode;
            }
            result.Passage.Add(passageGroups[2].Value, newNode);
            //Check all the choices in the passage
            foreach(Match cmatch in  RegexParse(CHOICE_PATTERN, passageGroups[5].Value))
            {
                GroupCollection choiceGroups = cmatch.Groups;
                newNode.AddChoice(choiceGroups[1].Value, choiceGroups[2].Value);
            }
        }
        return result;
    }
    #endregion
}
