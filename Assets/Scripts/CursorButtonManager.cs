using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorButtonManager : MonoBehaviour
{
    /*
    public GameObject cursor1B, cursor1R, cursor1G, cursor1Y;
    public GameObject cursor2B, cursor2Y, cursor2P, cursor2O;
    public GameObject cursor3IB, cursor3B, cursor3G, cursor3P, cursor3O, cursor3Y;
    */

    public Texture2D[] CursorTextures;
	void Awake () 
	{
		DontDestroyOnLoad (this);
	}
    // Use this for initialization
    void Start ()
    {
		//Set cursor to the PlayerPrefs Value
		loadPlayerCursor ();
	}

	/* Loads the player's cursor from the prefs */ 
	public void loadPlayerCursor ()
	{
		Cursor.SetCursor (CursorTextures [PlayerPrefs.GetInt ("CursorValue")], Vector2.zero, CursorMode.Auto);
	}

	/* Saves the player's cursor choice as an int and changes the cursor to that */ 
	public void savePlayerCursor(int choice)
    {
		Cursor.SetCursor(CursorTextures[choice], Vector2.zero, CursorMode.Auto); 
		PlayerPrefs.SetInt ("CursorValue", choice);
    }
		

}
