using UnityEngine;
using System.Collections;

public class BlockedLaserCommentary : MonoBehaviour
{


	public TextAsset textFileBlockSuccessful,textFileBlockNotSuccessful;
	public string speakerName;
	public int startLine;
	public int endLine;

	public TextBoxManager theTextBox;

	public bool destroyWhenActivated,timedDialogue;
	public float timeUntilFinished;
	private MoveableObject _moveableObjectScript;
	public float timeUntilUnsuccessfulBlock;
	private bool _blockSuccessful;
	private float _timer;
	// Use this for initialization
	void Start ()
	{
		_blockSuccessful = false;
		_timer = timeUntilUnsuccessfulBlock;
		theTextBox = FindObjectOfType<TextBoxManager>();
	}

	// Update is called once per frame
	void Update ()
	{
		//if the laser is not blocked yet and the timer is at 0
		_timer -= Time.deltaTime;
		Debug.Log ("UNSUCCESSFUL IN: " + _timer);
		if (!_blockSuccessful && _timer < 0) 
		{
			_blockSuccessful = true;
			theTextBox.startCommentaryDialogue ();
			theTextBox.ReloadScript (textFileBlockNotSuccessful);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			//theTextBox.EnableTextBox();
			theTextBox.setSpeakerNameText (speakerName);

			if (timedDialogue) {
				theTextBox.activateTimedCommentary (timeUntilFinished);
			}
			if (destroyWhenActivated) {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (Time.timeScale != 0) {
			_moveableObjectScript = other.GetComponent<MoveableObject> ();
			if (_moveableObjectScript != null) 
			{
				if (_moveableObjectScript.isTractored) 
				{
					_blockSuccessful = true;
					theTextBox.startCommentaryDialogue ();
					theTextBox.ReloadScript (textFileBlockSuccessful);
					theTextBox.currentLine = startLine;
					theTextBox.endAtLine = endLine;
					//theTextBox.EnableTextBox();
					theTextBox.setSpeakerNameText (speakerName);

					if (timedDialogue) {
						theTextBox.activateTimedCommentary (timeUntilFinished);
					}
					if (destroyWhenActivated) {
						Destroy (gameObject);
					}
				}

			}
		}
	}
}
