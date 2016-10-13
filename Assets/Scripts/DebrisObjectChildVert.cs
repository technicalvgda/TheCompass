using UnityEngine;
using System.Collections;

public class DebrisObjectChildVert : MonoBehaviour
{
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		// do nothing
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.GetComponent<MoveableObject>() != null) // MoveableObject script does exist
		{
			if (transform.gameObject.name == "DebObUpper")
			{
				transform.parent.GetComponent<DebrisBorderVert>().collWithObUpper(coll);
			}

			if (transform.gameObject.name == "DebObLower")
			{
				transform.parent.GetComponent<DebrisBorderVert>().collWithObLower(coll);
			}
		}
	}
}
