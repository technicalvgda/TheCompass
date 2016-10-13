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

    // Use this for initialization
    void Start ()
    {
		//Set cursor1B as default
		Cursor.SetCursor(CursorTextures[0], Vector2.zero, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void cursor1Blue()
    {
        Cursor.SetCursor(CursorTextures[0], Vector2.zero, CursorMode.Auto); 
    }
    public void cursor1Yellow()
    {
        Cursor.SetCursor(CursorTextures[1], Vector2.zero, CursorMode.Auto);
    }
    public void cursor1Green()
    {
        Cursor.SetCursor(CursorTextures[2], Vector2.zero, CursorMode.Auto);
    }
    public void cursor1Red()
    {
        Cursor.SetCursor(CursorTextures[3], Vector2.zero, CursorMode.Auto);
    }

    public void cursor2Blue()
    {
        Cursor.SetCursor(CursorTextures[4], Vector2.zero, CursorMode.Auto);
    }
    public void cursor2Yellow()
    {
        Cursor.SetCursor(CursorTextures[5], Vector2.zero, CursorMode.Auto);
    }
    public void cursor2Pink()
    {
        Cursor.SetCursor(CursorTextures[6], Vector2.zero, CursorMode.Auto);
    }
    public void cursor2Orange()
    {
        Cursor.SetCursor(CursorTextures[7], Vector2.zero, CursorMode.Auto);
    }

    public void cursor3InvertedBlue()
    {
        Cursor.SetCursor(CursorTextures[8], Vector2.zero, CursorMode.Auto);
    }
    public void cursor3Blue()
    {
        Cursor.SetCursor(CursorTextures[9], Vector2.zero, CursorMode.Auto);
    }
    public void cursor3Green()
    {
        Cursor.SetCursor(CursorTextures[10], Vector2.zero, CursorMode.Auto);
    }
    public void cursor3Purple()
    {
        Cursor.SetCursor(CursorTextures[11], Vector2.zero, CursorMode.Auto);
    }
    public void cursor3Yellow()
    {
        Cursor.SetCursor(CursorTextures[12], Vector2.zero, CursorMode.Auto);
    }
    public void cursor3Red()
    {
        Cursor.SetCursor(CursorTextures[13], Vector2.zero, CursorMode.Auto);
    }

}
