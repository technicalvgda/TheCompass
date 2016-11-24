using UnityEngine;
using System.Collections;

public class PlayerShieldActivation : MonoBehaviour
{


	public TextAsset shieldActivationText1,shieldActivation2;
	public string speakerName;
	public int startLine;
	public int endLine;
	//public AudioClip audioClip;

	public TextBoxManager theTextBox;

	public bool timedDialogue;
	public float timeUntilFinished;
	private float _numbTimesOutOfBounds;
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
		if (Time.timeScale != 0) {
			if (other.name == "PlayerPlaceholder") 
			{
				_numbTimesOutOfBounds++;
				if (_numbTimesOutOfBounds == 1) 
				{
					theTextBox.startCommentaryDialogue ();
					//theTextBox.setVoiceOverSourceClip(audioClip);
					theTextBox.ReloadScript (shieldActivationText1);
					theTextBox.currentLine = startLine;
					theTextBox.endAtLine = endLine;
					//theTextBox.EnableTextBox();
					theTextBox.setSpeakerNameText (speakerName);

					if (timedDialogue) 
					{
						theTextBox.activateTimedCommentary (timeUntilFinished);
					}
				} 
				else if (_numbTimesOutOfBounds == 2) 
				{
					theTextBox.startCommentaryDialogue ();
					//theTextBox.setVoiceOverSourceClip(audioClip);
					theTextBox.ReloadScript (shieldActivation2);
					theTextBox.currentLine = startLine;
					theTextBox.endAtLine = endLine;
					//theTextBox.EnableTextBox();
					theTextBox.setSpeakerNameText (speakerName);

					if (timedDialogue) 
					{
						theTextBox.activateTimedCommentary (timeUntilFinished);
					}
					Destroy (gameObject);

				}

			}
		}
	}
}
