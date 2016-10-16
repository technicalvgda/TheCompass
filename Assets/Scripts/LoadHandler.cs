/* Script that handles the Load Features via a "Master" gameobject called Load Handler */ 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadHandler : MonoBehaviour 
{
	
	/* Stores the slotText aka the middle button */ 
	public Text slotText;

	/* Display what PlayerPref's "onLevel" is storing */ 
	void Start()
	{
		slotText.text = PlayerPrefs.GetString ("onLevel");
	}
		
	/* Loads the level */ 
	public void onLoad() 
	{
		Debug.Log ("Loading: " + PlayerPrefs.GetString("onLevel"));
	}

	/* Goes back to previous scene */ 
	public void onBack()
	{
		Debug.Log ("Back!");
	}
		


}
