using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScriptedEvent : MonoBehaviour {
	public Image backgroundImage, blackFadeOutImage;
	public Text theText;
	public AudioSource audioSource;
	public TextAsset[] textFiles;
	public AudioClip[] audioClips;
	private Color _color;
	private int _counter;
	// Use this for initialization
	void Start () {
		_counter = 0;
		StartCoroutine (InitiateEnding ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator InitiateEnding()
	{
		//Fade out the black to the background image
		while (blackFadeOutImage.color.a > 0) 
		{
			_color = new Color (blackFadeOutImage.color.r, blackFadeOutImage.color.g, blackFadeOutImage.color.b, blackFadeOutImage.color.a - Time.deltaTime);
			blackFadeOutImage.color = _color;
			yield return null;
		}
		//play audio source
		audioSource.clip = audioClips[0];
		audioSource.Play ();
		//Implement the rest. Manipulate the WaitForSeconds to match subtitles with voice over
		if (textFiles.Length == 0) 
		{
			Debug.Log ("NO TEXT FILES IN THE ARRAY.");
		} 
		else 
		{
			theText.text = textFiles [_counter].ToString();
			_counter++;
			yield return new WaitForSeconds (18);
		}

		//TEMP YIELD FOR SECONDS REMOVE BEFORE LAUNCH
		yield return new WaitForSeconds (2);

		//Fade in the black for smooth transition
		while (blackFadeOutImage.color.a <1) 
		{
			_color = new Color (blackFadeOutImage.color.r, blackFadeOutImage.color.g, blackFadeOutImage.color.b, blackFadeOutImage.color.a + Time.deltaTime);
			blackFadeOutImage.color = _color;
			yield return null;
		}
		//Load the credits scene
		SceneManager.LoadScene ("Credit Scene");
	}
}