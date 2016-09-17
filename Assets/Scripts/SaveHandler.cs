using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SaveHandler : MonoBehaviour {
	public Text slotText;
	public GameObject onScreenPrompt;
	void Start()
	{
		slotText.text = PlayerPrefs.GetString ("onLevel");
		onScreenPrompt.SetActive (false);
	}

	public void onSave() 
	{
		onScreenPrompt.SetActive (true);
	}

	public void onLoad() 
	{
		Debug.Log ("Loading: " + PlayerPrefs.GetString("onLevel"));
	}

	public void onBack()
	{
		//No menu to go back to yet
	}

	public void promptYes()
	{
		//Random number generator to test the saving feature
		PlayerPrefs.SetString("onLevel", "Level " + Random.Range(1,100));
		slotText.text = PlayerPrefs.GetString ("onLevel");
		Debug.Log ("Saving: " + PlayerPrefs.GetString("onLevel"));
		onScreenPrompt.SetActive (false);
	}

	public void promptNo() 
	{
		onScreenPrompt.SetActive (false);
	}


}
