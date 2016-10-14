using UnityEngine;
using System.Collections;

public class DebrisObjectChildVert : MonoBehaviour
{
	private float maxRayDistance;
	private float boxCollSizeX,boxCollSizeY; // NOTE: vals should be same if square
	private float obXLoc, obYLoc;
	private Vector2 leftBoundCorner;
	Ray2D rayBound;
	RaycastHit2D hit;

	void FixedUpdate()
	{
		if (transform.gameObject.name == "DebObUpper")
		{
			leftBoundCorner = new Vector2 (obXLoc - boxCollSizeX / 2, obYLoc + boxCollSizeY / 2);
		} 
		else if (transform.gameObject.name == "DebObLower")
		{
			leftBoundCorner = new Vector2 (obXLoc - boxCollSizeX / 2, obYLoc - boxCollSizeY / 2);
		} 
		else
		{
			leftBoundCorner = new Vector2 (obXLoc, obYLoc); // dummy value, default
		}
		rayBound = new Ray2D (leftBoundCorner, Vector2.right);
		Debug.DrawLine (leftBoundCorner, leftBoundCorner + Vector2.right * maxRayDistance);

	}
	void Start ()
	{
		maxRayDistance = GetComponent<BoxCollider2D>().size.x;
		boxCollSizeX = GetComponent<BoxCollider2D>().size.x;
		boxCollSizeY = GetComponent<BoxCollider2D>().size.y;
		obXLoc = transform.position.x;
		obYLoc = transform.position.y;
	}

	// Update is called once per frame
	void Update ()
	{
		if (transform.gameObject.name == "DebObUpper")
		{
			hit = Physics2D.Raycast (rayBound.origin, rayBound.direction);
			if (hit != false && hit.collider != null && hit.collider.GetComponent<MoveableObject>() != null)
			{
				Debug.Log ("Detected collision with Ray2D");
				transform.parent.GetComponent<DebrisBorderVert>().collWithObUpper(hit.collider);
			}
		}
		if (transform.gameObject.name == "DebObLower")
		{	
			hit = Physics2D.Raycast (rayBound.origin, rayBound.direction);
			if (hit != false && hit.collider != null && hit.collider.GetComponent<MoveableObject>() != null)
			{
				Debug.Log ("Detected collision with Ray2D");
				transform.parent.GetComponent<DebrisBorderVert>().collWithObLower(hit.collider);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		// do nothing
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		//do nothing
	}
}
