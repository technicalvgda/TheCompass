using UnityEngine;
using System.Collections;

public class TetheredShipPartCommentary : MonoBehaviour
{


	public TextAsset textFile;
	public string speakerName;
	public int startLine;
	public int endLine;

	public AudioClip audioClip;
	public TextBoxManager theTextBox;

	public bool destroyWhenActivated,timedDialogue;
	public float timeUntilFinished;
	private GameObject _shipPart;
	private TetheredObject _tetheredObjectScript;
	private bool _triggeredOnce;
	// Use this for initialization
	void Start ()
	{
		_triggeredOnce = false;
		_shipPart = GameObject.FindGameObjectWithTag ("TetheredPart");
		theTextBox = FindObjectOfType<TextBoxManager>();
		_tetheredObjectScript = _shipPart.GetComponent<TetheredObject> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (_tetheredObjectScript.tetherOn == true && !_triggeredOnce) 
		{
			_triggeredOnce = true;
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