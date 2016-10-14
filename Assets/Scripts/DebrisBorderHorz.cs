using UnityEngine;
using System.Collections;

public class DebrisBorderHorz : MonoBehaviour
{
	// NOTE: INCLUDES TWO BORDER OBJECTS

	private GameObject DebObLeft, DebObRight;

	void Start ()
	{
		DebObLeft = transform.GetChild(0).gameObject;
		DebObRight = transform.GetChild(1).gameObject;
	}

	void Update ()
	{

	}

	public void collWithObLeft(Collider2D coll)
	{
		float xLoc = DebObRight.transform.position.x;
		float yLoc = DebObRight.transform.position.y;
		float zLoc = DebObRight.transform.position.z;
        coll.transform.position = new Vector3(xLoc - DebObRight.GetComponent<BoxCollider2D>().size.x + .95f, coll.transform.position.y, zLoc);
	}

	public void collWithObRight(Collider2D coll)
	{
		float xLoc = DebObLeft.transform.position.x;
		float yLoc = DebObLeft.transform.position.y;
		float zLoc = DebObLeft.transform.position.z;
		coll.transform.position = new Vector3(xLoc+DebObLeft.GetComponent<BoxCollider2D>().size.x-.95f, coll.transform.position.y, zLoc);
	}
}
