using UnityEngine;
using System.Collections;

public class PlayerHealthLowCommentary : MonoBehaviour
{


	public TextAsset textFile;
	public string speakerName;
	public int startLine;
	public int endLine;
	public AudioClip audioClip;

	public TextBoxManager theTextBox;

	public bool destroyWhenActivated,timedDialogue;
	public float timeUntilFinished;
	private Player _player;
	public float healthToActivateCommentary;
	private bool _displayedLowHealthCommentary;
	// Use this for initialization
	void Start ()
	{
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		theTextBox = FindObjectOfType<TextBoxManager>();
		_displayedLowHealthCommentary = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (_player.getHealth() <= healthToActivateCommentary && !_displayedLowHealthCommentary) 
		{
			_displayedLowHealthCommentary = true;
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
