using UnityEngine;
using System.Collections;

public class LogoBackgroundAudioScript : MonoBehaviour {
	public AudioSource audioSource;
	public float audioDelay;
	// Use this for initialization
	void Start () {
		StartCoroutine (PlayAudio (audioDelay));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PlayAudio(float sec)
	{
		yield return new WaitForSeconds(sec);
		audioSource.Play ();
		while (audioSource.volume <= 1) 
		{
			audioSource.volume += Time.deltaTime;
			yield return new WaitForSeconds (0.1f);
		}

	}
}
