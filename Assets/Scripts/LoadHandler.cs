using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadHandler : MonoBehaviour {
	public Text slotText;
	void Start()
	{
		slotText.text = PlayerPrefs.GetString ("onLevel");
	}
		
	public void onLoad() 
	{
		Debug.Log ("Loading: " + PlayerPrefs.GetString("onLevel"));
	}

	public void onBack()
	{
		//No menu to go back to yet
	}
		


}
