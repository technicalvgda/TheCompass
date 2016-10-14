using UnityEngine;
using System.Collections;

public class DebrisBorderVert : MonoBehaviour
{
	// NOTE: INCLUDES TWO BORDER OBJECTS

	private GameObject DebObUpper, DebObLower;

	void Start ()
	{
		DebObUpper = transform.GetChild(0).gameObject;
		DebObLower = transform.GetChild(1).gameObject;
	}

	void Update ()
	{

	}

	public void collWithObUpper(Collider2D coll)
	{
		float xLoc = DebObLower.transform.position.x;
		float yLoc = DebObLower.transform.position.y;
		float zLoc = DebObLower.transform.position.z;
		coll.transform.position = new Vector3 (coll.transform.position.x, yLoc+DebObLower.GetComponent<BoxCollider2D>().size.y-.95f, zLoc);
	}

	public void collWithObLower(Collider2D coll)
	{
		float xLoc = DebObUpper.transform.position.x;
		float yLoc = DebObUpper.transform.position.y;
		float zLoc = DebObUpper.transform.position.z;
		coll.transform.position = new Vector3(coll.transform.position.x, yLoc-DebObUpper.GetComponent<BoxCollider2D>().size.y+.95f, zLoc);
	}
}
