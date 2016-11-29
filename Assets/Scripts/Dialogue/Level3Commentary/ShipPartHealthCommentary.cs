using UnityEngine;
using System.Collections;

public class ShipPartHealthCommentary : MonoBehaviour {

	public TextAsset DMGTextFile1,DMGTextFile2,DMGTextFile3,destroyedTextFile;
	public string speakerName;
	public int startLine;
	public int endLine;

	public TextBoxManager theTextBox;

	public bool destroyWhenActivated,timedDialogue;
	public float timeUntilFinished;
	private GameObject _shipPart;
	private TetheredObject _tetheredObjectScript;
	private bool _triggeredOnce1,_triggeredOnce2,_triggeredOnce3,_triggeredOnce4;
	// Use this for initialization
	void Start ()
	{
		_triggeredOnce1 = false;
		_triggeredOnce2 = false;
		_triggeredOnce3 = false;
		_triggeredOnce4 = false;
		_shipPart = GameObject.FindGameObjectWithTag ("TetheredPart");
		theTextBox = FindObjectOfType<TextBoxManager>();
		_tetheredObjectScript = _shipPart.GetComponent<TetheredObject> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (_tetheredObjectScript.TetheredHealth == 3 && _triggeredOnce1 == false) 
		{
			_triggeredOnce1 = true;
			theTextBox.startCommentaryDialogue ();
			theTextBox.ReloadScript (DMGTextFile1);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			//theTextBox.EnableTextBox();
			theTextBox.setSpeakerNameText (speakerName);

			if (timedDialogue) 
			{
				theTextBox.activateTimedCommentary (timeUntilFinished);
			}
		}
		else if (_tetheredObjectScript.TetheredHealth == 2 && _triggeredOnce2 == false) 
		{
			_triggeredOnce2 = true;
			theTextBox.startCommentaryDialogue ();
			theTextBox.ReloadScript (DMGTextFile2);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			//theTextBox.EnableTextBox();
			theTextBox.setSpeakerNameText (speakerName);

			if (timedDialogue) 
			{
				theTextBox.activateTimedCommentary (timeUntilFinished);
			}
		}
		else if (_tetheredObjectScript.TetheredHealth == 1 && _triggeredOnce3 == false) 
		{
			_triggeredOnce3 = true;
			theTextBox.startCommentaryDialogue ();
			theTextBox.ReloadScript (DMGTextFile3);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			//theTextBox.EnableTextBox();
			theTextBox.setSpeakerNameText (speakerName);

			if (timedDialogue) 
			{
				theTextBox.activateTimedCommentary (timeUntilFinished);
			}
		}
		else if (_tetheredObjectScript.TetheredHealth == 0 && _triggeredOnce4 == false) 
		{
			
			_triggeredOnce4 = true;
			theTextBox.startCommentaryDialogue ();
			theTextBox.ReloadScript (destroyedTextFile);
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
			_tetheredObjectScript.FailLevel ();
		}
	}

	IEnumerator WaitForTextBoxToFailLevel()
	{

		Time.timeScale = 0;
		theTextBox.ReloadScript (destroyedTextFile);
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
		theTextBox.startCommentaryDialogue ();
		while (!theTextBox.isTyping) 
		{
			Debug.Log ("HERE");
			yield return new WaitForSecondsRealtime (0.01f);
		}
		yield return null;
		while (theTextBox.isTyping) 
		{
			Debug.Log ("HERE2");
			yield return new WaitForSecondsRealtime (0.01f);
		}
		Debug.Log ("HERE3");
		//Time.timeScale = 1;
		_tetheredObjectScript.FailLevel ();
		//yield return null;
	}
}