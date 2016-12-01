using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CreditsScript : MonoBehaviour {
	public GameObject creditsObject;
	public float waitTime;
	public Image blackOutImage;
	private Color _color;
	// Use this for initialization
	void Start () {
		StartCoroutine (StartCredits (waitTime));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator StartCredits(float sec)
	{
		yield return new WaitForSeconds (sec);
		creditsObject.SetActive (true);
		//mainMenuHeader.SetActive (false);
		//yield return null;
	}
	//This is for the back button in the credits scene
	public void changeScene(string sceneName)
	{
		StartCoroutine (CreditsTransitionBlackOut (sceneName));
	}
	IEnumerator CreditsTransitionBlackOut(string sceneName)
	{		
		while (blackOutImage.color.a < 1) 
		{
			_color = new Color (blackOutImage.color.r, blackOutImage.color.g, blackOutImage.color.b, blackOutImage.color.a + Time.unscaledDeltaTime);
			blackOutImage.color = _color;
			yield return null;
		}
		SceneManager.LoadScene (sceneName);
	}
}
