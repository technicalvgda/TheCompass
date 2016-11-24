using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SimpleTextDisplay : MonoBehaviour {
	private string _stringText;
	public Text textBox;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setText(string text)
	{
		textBox.text = text;
	}
}
