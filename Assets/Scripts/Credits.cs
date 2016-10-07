using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour 
{
	public int CreditTime;
	private float height;
	private float updateDist;

	/* Retrieves the height of the text box and divides the height by the value of Credit Time 
	   in order to reach the end by the value of the Credit Time */
	void Start () 
	{
		height = GetComponent< RectTransform >().rect.height;
		updateDist = height / CreditTime;
	}

	/* This method updates the text position by 2 every time this method is called.
		Once it updates, it will set the position of the text to the new location.*/
	void Update () 
	{
		// If the user enters escape or the space bar, it will redirect the user to the
		// title screen.
		if ( Input.GetKey( KeyCode.Escape ) || Input.GetKey( KeyCode.Space ) )
		{
			SceneManager.LoadScene ( "TitleMenu" );
		}
		// Else it will move the text based on the Credit Time in the inspector.
		else
		{
			var updatePos = transform.position;
			updatePos.y = updatePos.y + (float) ( 2.25 * updateDist * Time.deltaTime );
			transform.position = updatePos;
		}
	}

	/* Redirects the user to the title menu. This is used for the button in the Credit Scene*/
	public void loadTitleMenu()
	{
		SceneManager.LoadScene ( "TitleMenu" );
	}

}
