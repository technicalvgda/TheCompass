using UnityEngine;
using System.Collections;

public class PlayerWithinRangeOfEnemy : MonoBehaviour
{


	public TextAsset textFile;
	public string speakerName;
	public int startLine;
	public int endLine;
	public AudioClip audioClip;

	public TextBoxManager theTextBox;

	public bool destroyWhenActivated,timedDialogue;
	public float timeUntilFinished;
	// Use this for initialization
	void Start ()
	{
		theTextBox = FindObjectOfType<TextBoxManager>();
	}

	// Update is called once per frame
	void Update ()
	{

	}
	/*
	void OnTriggerEnter2D(Collider2D other)
	{
		if (Time.timeScale != 0) {
			if (other.name == "PlayerPlaceholder") {
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
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (Time.timeScale != 0) {
			if (other.name == "PlayerPlaceholder") {
				theTextBox.startCommentaryDialogue ();
				theTextBox.ReloadScript (textFile);
				theTextBox.setVoiceOverSourceClip(audioClip);
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
	public void activateCommentary()
	{
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
