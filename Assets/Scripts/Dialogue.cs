using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour 
{
	public Text text;
	public float textSpeed = .1f;
	public Color textColor;
	void Start () 
	{
		text = GetComponent<Text> ();
		text.color = new Color(textColor.r, textColor.g, textColor.b);
		StartCoroutine (AnimateText("This is a textThis is a textThis is a textThis is a textThis is a textThis is a textThis is a textThis is a textThis is a textThis is a text"));
	}
	
	IEnumerator AnimateText(string textInput) 
	{
		int i = 0;
		text.text = "";
		while(i < textInput.Length)
		{
			text.text += textInput [i++];
			yield return new WaitForSeconds (textSpeed);
		}
	}
}
