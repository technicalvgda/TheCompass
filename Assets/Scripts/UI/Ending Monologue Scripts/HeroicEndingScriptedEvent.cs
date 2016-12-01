using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroicEndingScriptedEvent : MonoBehaviour {
	public Image backgroundImage, blackFadeOutImage;
	public int numbOfEndings;
	public Text theText;
	public AudioSource audioSource;

	public AudioClip Heroic1And2Clip, Heroic3Clip;
	public TextAsset[] textFilesHeroic1And2;
	public TextAsset[] textFilesHeroic3;
	public float[] waitForSecArrayHeroic1And2;
	public float[] waitForSecArrayHeroic3;
	private Color _color;
	private int _counter;

	private int _rand;


	// Use this for initialization
	void Start () {
		_counter = 0;
		_rand = (int)Random.Range (0, numbOfEndings);
		Debug.Log ("rand: " + _rand);
		switch (_rand) 
		{
		case 0:
			StartCoroutine (InitiateEnding (textFilesHeroic1And2,waitForSecArrayHeroic1And2,Heroic1And2Clip));
			return;
		case 1:
			StartCoroutine (InitiateEnding (textFilesHeroic3,waitForSecArrayHeroic3,Heroic3Clip));
			return;
		
		}
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator InitiateEnding(TextAsset[] textFiles, float[] waitForSecArray,AudioClip clip)
	{
		//Fade out the black to the background image
		while (blackFadeOutImage.color.a > 0) 
		{
			_color = new Color (blackFadeOutImage.color.r, blackFadeOutImage.color.g, blackFadeOutImage.color.b, blackFadeOutImage.color.a - Time.deltaTime);
			blackFadeOutImage.color = _color;
			yield return null;
		}
		//play audio source
		audioSource.clip = clip;
		audioSource.Play ();
		//Implement the rest. Manipulate the WaitForSeconds to match subtitles with voice over
		if (textFiles.Length == 0) 
		{
			Debug.Log ("NO TEXT FILES IN THE ARRAY.");
		} 
		else 
		{
			Debug.Log (textFiles.Length);
			while (_counter < textFiles.Length) 
			{
				theText.text = textFiles [_counter].ToString ();
				Debug.Log ("Counter: " + _counter);
				_counter++;
				yield return new WaitForSeconds (waitForSecArray[_counter-1]);
			}
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