using UnityEngine;
using System.Collections;

public class CursorChange : MonoBehaviour
{
    /*
    Holds all the Cursor Textures.

    Is accessible directly from the Unity Inspector so non-programmers
    can add their own Cursor Textures.

    ***Cursor Textures must be set to Cursor! (Done by looking at the texture
    ***in the Inspector and changing their Texture Type)
    */
    public Texture2D[] CursorTextures;

    /*
    The current Cursor Texture being displayed.
    Used only for testing purposes.
    */
    int _index;

    /*
    Runs at the beginning of the GameObject being loaded in scene.
    Will initialize the index as well as set the first CursorTexture.
    */
	void Start ()
    {
        _index = 0;
        Cursor.SetCursor(CursorTextures[_index], Vector2.zero, CursorMode.Auto); ;
	}
	
    /*
    Runs every frame.
    Will check if the player clicked their left mouse button and then
    switch to the next CursorTexture in the array. If it goes beyond
    the size of the array, it will be set back to 0.
    */
	void Update ()
    {
        //Check if the Left Mouse Button is clicked during this frame
	    if (Input.GetMouseButtonDown(0))
        {
            //Increment index by one, and modulo it by the size of the array
            //so that it goes to 0 if it would go out of bounds.
            _index = ++_index % CursorTextures.Length;

            //Sets the cursor's texture to the next texture.
            Cursor.SetCursor(CursorTextures[_index], Vector2.zero, CursorMode.Auto);
        }
	}
}
