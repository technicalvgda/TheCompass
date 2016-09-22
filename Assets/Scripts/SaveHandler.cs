/*
Script that handles the Save Features via a "Master" gameobject called Save Handler
*/ 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SaveHandler : MonoBehaviour 
{
	/* Stores the slotText aka the middle button */ 
	public Text slotText;

	/* Stores the gameobject of the onScreenPrompt */ 
	public GameObject onScreenPrompt;

	/* Display what PlayerPref's "onLevel" is storing and sets the prompt to inactive */ 
	void Start()
	{
		slotText.text = PlayerPrefs.GetString ("onLevel");
		onScreenPrompt.SetActive (false);
	}

	/* Prompt the user if they want to overwrite */ 
	public void onSave() 
	{
		onScreenPrompt.SetActive (true);
	}
	public void Onload()
	{
		Debug.Log ("Loaded " + PlayerPrefs.GetString ("onLevel"));
	}

	/* Goes back to previous scene */ 
	public void onBack()
	{
		Debug.Log ("Back!");
	}

	/* Saves the level */ 
	public void promptYes()
	{
		//Random number generator to test the saving feature
		PlayerPrefs.SetString("onLevel", "Level " + Random.Range(1,100));
		slotText.text = PlayerPrefs.GetString ("onLevel");
		Debug.Log ("Saving: " + PlayerPrefs.GetString("onLevel"));
		onScreenPrompt.SetActive (false);
	}

	/* Closes the prompt */ 
	public void promptNo() 
	{
		onScreenPrompt.SetActive (false);
	}


}
