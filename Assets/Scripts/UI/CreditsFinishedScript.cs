using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//This script is for the animator to fire an animation event.
//It is strictly for loading the main menu after the credits have finished
public class CreditsFinishedScript : MonoBehaviour {
	
	public void loadMainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
