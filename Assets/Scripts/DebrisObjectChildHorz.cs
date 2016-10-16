using UnityEngine;
using System.Collections;

public class DebrisObjectChildHorz : MonoBehaviour
{

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		// do nothing
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.GetComponent<MoveableObject>() != null) // MoveableObject script does exist
		{
			if (transform.gameObject.name == "DebObLeft")
			{
				transform.parent.GetComponent<DebrisBorderHorz>().collWithObLeft(coll);
			}

			if (transform.gameObject.name == "DebObRight")
			{
				transform.parent.GetComponent<DebrisBorderHorz>().collWithObRight(coll);
			}
		}
	}
}
