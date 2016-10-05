using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/* Loads the title menu after the VGDA logo gate animation is finished */
public class LogoGateLoadTitleMenu : MonoBehaviour 
{
	public string sceneName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey)
			SceneManager.LoadScene (sceneName);
			
	}
	public void loadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}
