using UnityEngine;
using System.Collections;

public class PlayerIdleCommentary : MonoBehaviour
{


	public TextAsset textFile;
	public string speakerName;
	public int startLine;
	public int endLine;
	public AudioClip audioClip;

	public TextBoxManager theTextBox;

	public bool destroyWhenActivated,timedDialogue;
	public float timeUntilFinished;
	public PlayerIdleTimer idleTimerScript;
	public float displayMessageTime;
	private bool _displayedIdleCommentary;
	// Use this for initialization
	void Start ()
	{
		theTextBox = FindObjectOfType<TextBoxManager>();
		_displayedIdleCommentary = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (idleTimerScript.getPlayerIdleTime () > displayMessageTime && !_displayedIdleCommentary) 
		{
			_displayedIdleCommentary = true;
			theTextBox.startCommentaryDialogue ();
			theTextBox.setVoiceOverSourceClip(audioClip);
			theTextBox.ReloadScript (textFile);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			//theTextBox.EnableTextBox();
			theTextBox.setSpeakerNameText (speakerName);

			if (timedDialogue) 
			{
				theTextBox.activateTimedCommentary (timeUntilFinished);
			}
			if (destroyWhenActivated) 
			{
				Destroy (gameObject);
			}
		}
			
	}
	/*
	void OnTriggerEnter2D(Collider2D other)
	{
		if (Time.timeScale != 0) {
			if (other.name == "PlayerPlaceholder") {
				theTextBox.startCommentaryDialogue ();
				theTextBox.setSourceClip(audioClip);
				theTextBox.ReloadScript (textFile);
				theTextBox.currentLine = startLine;
				theTextBox.endAtLine = endLine;
				//theTextBox.EnableTextBox();
				theTextBox.setSpeakerNameText (speakerName);

				if (timedDialogue) 
				{
					theTextBox.activateTimedCommentary (timeUntilFinished);
				}
				if (destroyWhenActivated) 
				{
					Destroy (gameObject);
				}
			}
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (Time.timeScale != 0) {
			if (other.name == "PlayerPlaceholder") {
				theTextBox.startCommentaryDialogue ();
				theTextBox.ReloadScript (textFile);
				theTextBox.setSourceClip(audioClip);
				theTextBox.currentLine = startLine;
				theTextBox.endAtLine = endLine;
				//theTextBox.EnableTextBox();
				theTextBox.setSpeakerNameText (speakerName);

				if (timedDialogue) 
				{
					theTextBox.activateTimedCommentary (timeUntilFinished);
				}
				if (destroyWhenActivated) 
				{
					Destroy (gameObject);
				}
			}
		}
	}*/
}
