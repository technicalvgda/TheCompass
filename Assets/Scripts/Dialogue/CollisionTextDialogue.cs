using UnityEngine;
using System.Collections;

public class CollisionTextDialogue : MonoBehaviour
{


    public TextAsset theText;
	public string speakerName;
    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;

    public bool destroyWhenActivated;

	// Use this for initialization
	void Start ()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "PlayerPlaceholder")
        {
			theTextBox.startCommentaryDialogue();
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            //theTextBox.EnableTextBox();
			theTextBox.setSpeakerNameText (speakerName);

            if(destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }
}
