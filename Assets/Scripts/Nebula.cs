using UnityEngine;
using System.Collections;

public class Nebula : MonoBehaviour
{
	public int type;
	// type 1 - POSITIVE stat boost
	// type 2 - NEGATIVE stat hit
	public bool active;
	// true means active, false means inactive
	private float speedMultiplier;
	// alter speed change of object going through NebulaGel, depends on type

	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name == "PlayerPlaceholder" && isActive ())
		{
			if (type == 1) // POSITIVE SPD BUFF NEBULA
			{
				Debug.Log ("GelType 1 Detected, positive stat boost");
				speedMultiplier = 2f;
				coll.gameObject.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
			} 
			else if (type == 2) // NEGATIVE SPD DEBUFF NEBULA
			{
				Debug.Log ("GelType 2 Detected, negative stat boost");
				speedMultiplier = 0.5f;
				coll.gameObject.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
			}
			else if (type == 3) // DAMAGING NEBULA
			{
				if (coll.gameObject.name == "PlayerPlaceholder" && coll.gameObject.activeSelf) //If nebula collides with player object
				{
					GameObject player = coll.gameObject;

					//When the below is uncommented, the player object will be immediately "destroyed"
					//Should be replaced with code to decrease health
					//player.SetActive(false);

					player.transform.position = new Vector3(0, 0, 0); //Temporary code for respawning, resets the player obj to origin
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll) // revert boost/antiboost effects
	{
		if (coll.gameObject.name == "PlayerPlaceholder" && isActive ())
		{
			if (type == 1)
			{
				Debug.Log ("Reset");
				speedMultiplier = 2f;
			} 
			else if (type == 2) 
			{
				Debug.Log ("Reset");
				speedMultiplier = 0.5f;
			}
			coll.gameObject.GetComponent<Rigidbody2D>().velocity /= speedMultiplier;
			//remove speed buff/debuff
		}
	}

	bool isActive()
	{
		return active;
	}
}