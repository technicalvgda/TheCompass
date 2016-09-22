using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		
	}

	/* This method updates the text position by 2 every time this method is called.
		Once it updates, it will set the position of the text to the new location.*/
	void Update () 
	{
		var updatePos = transform.position;
		updatePos.y += 2;
		transform.position = updatePos;
	}
}
