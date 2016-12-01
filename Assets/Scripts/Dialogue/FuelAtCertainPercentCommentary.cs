using UnityEngine;
using System.Collections;

public class FuelAtCertainPercentCommentary : MonoBehaviour
{


	public TextAsset textFile;
	public string speakerName;
	public int startLine;
	public int endLine;

	public AudioClip audioClip;
	public TextBoxManager theTextBox;
	public float fuelLevelToActivate;
	public bool destroyWhenActivated,timedDialogue;
	public float timeUntilFinished;
	private Player player;
	private bool _triggeredOnce;
	// Use this for initialization
	void Start ()
	{
		_triggeredOnce = false;
		theTextBox = FindObjectOfType<TextBoxManager>();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (player.getFuel01 () < fuelLevelToActivate && !_triggeredOnce) 
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