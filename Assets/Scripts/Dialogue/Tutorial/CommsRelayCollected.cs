using UnityEngine;
using System.Collections;

public class CommsRelayCollected : MonoBehaviour
{


	public TextAsset textFile;
	public string speakerName;
	public int startLine;
	public int endLine;

	public AudioClip audioClip;
	public TextBoxManager theTextBox;

	public bool destroyWhenActivated,timedDialogue;
	public float timeUntilFinished;
	private GameObject _player;
	private TractorBeamControls _tractorBeamControls;
	private bool _triggeredOnce;
	// Use this for initialization
	void Start ()
	{
		_triggeredOnce = false;
		_player = GameObject.FindGameObjectWithTag ("Player");
		theTextBox = FindObjectOfType<TextBoxManager>();
		_tractorBeamControls = _player.GetComponent<TractorBeamControls> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (_tractorBeamControls.partCollected == true && !_triggeredOnce) 
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