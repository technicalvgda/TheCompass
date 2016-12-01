using UnityEngine;
using System.Collections;

public class TwoObjectsCollideCommentary : MonoBehaviour
{


	public TextAsset textFile;
	public string speakerName;
	public int startLine;
	public int endLine;

	public AudioClip audioClip1,audioClip2;
	public TextBoxManager theTextBox;

	public bool destroyWhenActivated,timedDialogue;
	public float timeUntilFinished;
	//public GameObject MoveableObject;
	//private MoveableObject _moveableObjectScript;
	private bool _triggeredOnce;

	private bool inCommentary;
	// Use this for initialization
	void Start ()
	{
		_triggeredOnce = false;
		theTextBox = FindObjectOfType<TextBoxManager>();
		//_moveableObjectScript = MoveableObject.GetComponent<MoveableObject> ();
	}

	// Update is called once per frame
	void Update ()
	{
		//if (_moveableObjectScript.isTractored == true && !_triggeredOnce) 
		//{
		/*
			_triggeredOnce = true;
			theTextBox.startCommentaryDialogue ();
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
			}*/
		//}
	}
	public void activateCommentary()
	{
		theTextBox.startCommentaryDialogue ();
		theTextBox.setVoiceOverSourceClip(audioClip1);
		if (audioClip2 != null) {
			theTextBox.setSecondVoiceOverSourceClip(audioClip2);
		}
		theTextBox.ReloadScript (textFile);
		theTextBox.currentLine = startLine;
		theTextBox.endAtLine = endLine;
		//theTextBox.EnableTextBox();
		theTextBox.setSpeakerNameText (speakerName);
		theTextBox.activateTransitionOnFinished ();
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