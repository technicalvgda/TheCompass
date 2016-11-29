using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour {
	public GameObject creditsObject,mainMenuHeader;
	public float waitTime;
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
		mainMenuHeader.SetActive (false);
		//yield return null;
	}
	public void changeScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}
