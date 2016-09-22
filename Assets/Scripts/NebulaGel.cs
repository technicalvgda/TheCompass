using UnityEngine;
using System.Collections;

public class NebulaGel : MonoBehaviour
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
			if (type == 1) // POSITIVE
			{
				Debug.Log ("GelType 1 Detected, positive stat boost");
				speedMultiplier = 2f;
			} 
			else if (type == 2) // NEGATIVE
			{
				Debug.Log ("GelType 2 Detected, negative stat boost");
				speedMultiplier = 0.5f;
			}
			coll.gameObject.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
			//apply speed buff/debuff
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