using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Level4GameOver : MonoBehaviour {
	public Image blackFadeInImage;
	public GameObject gameOverCanvas;
	public AudioSource audioSource;
	private Color _color;
	private bool _playedAudio;
	// Use this for initialization
	void Start () {
		_playedAudio = false;
		StartCoroutine (AlternateGameOver ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator AlternateGameOver()
	{
		while (blackFadeInImage.color.a < 1) 
		{
			_color = new Color (blackFadeInImage.color.r, blackFadeInImage.color.g, blackFadeInImage.color.b, blackFadeInImage.color.a + Time.deltaTime);
			blackFadeInImage.color = _color;
			if (blackFadeInImage.color.a > 0.5f && !_playedAudio) 
			{
				_playedAudio = true;
				audioSource.PlayOneShot (audioSource.clip);
			}
			yield return null;
		}
		gameOverCanvas.SetActive (true);
	}
	public void activateAlternateEnding()
	{
		StartCoroutine (AlternateGameOver ());
	}
	//reloads the level that is currently executing
	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//loads up title menu level
	public void Quit()
	{	
		SceneManager.LoadScene ("MainMenu");	
		//Application.LoadLevel("TitleMenu");
	}
}
