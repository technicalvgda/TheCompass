using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4EndDialogue : MonoBehaviour
{

    public TextAsset textFile;
    public string speakerName;
    public int startLine;
    public int endLine;
    public AudioClip audioClip, audioClip2;

    public TextBoxManager theTextBox;

    public bool destroyWhenActivated, timedDialogue;
    public float timeUntilFinished;
    // Use this for initialization
    void Start()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ActivateCommentary()
    {
        if (Time.timeScale != 0)
        {
           
            theTextBox.startCommentaryDialogue();
            theTextBox.setVoiceOverSourceClip(audioClip);
            if (audioClip2 != null)
            {
                theTextBox.setSecondVoiceOverSourceClip(audioClip2);
            }
            theTextBox.ReloadScript(textFile);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            //theTextBox.EnableTextBox();
            theTextBox.setSpeakerNameText(speakerName);

            if (timedDialogue)
            {
               theTextBox.activateTimedCommentary(timeUntilFinished);
            }
            if (destroyWhenActivated)
            {
               Destroy(gameObject);
            }
            
        }
    }
}
