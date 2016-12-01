using UnityEngine;
using System.Collections;

public class PlayerOutOfBoundsCommentary : MonoBehaviour
{


	public TextAsset outOfBoundsTextFile1,outOfBoundsTextFile2;
	public string speakerName;
	public int startLine;
	public int endLine;
	public AudioClip audioClip1,audioClip2;

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

	void OnTriggerExit2D(Collider2D other)
	{
		if (Time.timeScale != 0) {
			if (other.name == "PlayerPlaceholder") 
			{
				_numbTimesOutOfBounds++;
				if (_numbTimesOutOfBounds == 1) 
				{
					theTextBox.startCommentaryDialogue ();
					theTextBox.setVoiceOverSourceClip(audioClip1);
					theTextBox.ReloadScript (outOfBoundsTextFile1);
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
					theTextBox.setVoiceOverSourceClip(audioClip2);
					theTextBox.ReloadScript (outOfBoundsTextFile2);
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
