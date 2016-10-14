using UnityEngine;
using System.Collections;

public class DebrisObjectChildHorz : MonoBehaviour
{
	private float maxRayDistance;
	private float boxCollSizeX,boxCollSizeY; // NOTE: vals should be same if square
	private float obXLoc, obYLoc;
	private Vector2 topBoundCorner;
	Ray2D rayBound;
	RaycastHit2D hit;

	void FixedUpdate()
	{
		if (transform.gameObject.name == "DebObLeft")
		{
			topBoundCorner = new Vector2 (obXLoc - boxCollSizeX / 2, obYLoc + boxCollSizeY / 2);
		} 
		else if (transform.gameObject.name == "DebObRight")
		{
			topBoundCorner = new Vector2 (obXLoc + boxCollSizeX / 2, obYLoc + boxCollSizeY / 2);
		} 
		else
		{
			topBoundCorner = new Vector2 (obXLoc, obYLoc); // dummy value, default
		}
		rayBound = new Ray2D (topBoundCorner, Vector2.down);
		Debug.DrawLine (topBoundCorner, topBoundCorner + Vector2.down * maxRayDistance);

	}
	// Use this for initialization
	void Start ()
	{
		maxRayDistance = GetComponent<BoxCollider2D>().size.y;
		boxCollSizeX = GetComponent<BoxCollider2D>().size.x;
		boxCollSizeY = GetComponent<BoxCollider2D>().size.y;
		obXLoc = transform.position.x;
		obYLoc = transform.position.y;
	}

	// Update is called once per frame
	void Update ()
	{
		if (transform.gameObject.name == "DebObLeft")
		{
			hit = Physics2D.Raycast (rayBound.origin, rayBound.direction);
			if (hit != false && hit.collider != null && hit.collider.GetComponent<MoveableObject>() != null)
			{
				Debug.Log ("Detected collision with Ray2D");
				transform.parent.GetComponent<DebrisBorderHorz>().collWithObLeft(hit.collider);
			}
		}
		if (transform.gameObject.name == "DebObRight")
		{	
			hit = Physics2D.Raycast (rayBound.origin, rayBound.direction);
			if (hit != false && hit.collider != null && hit.collider.GetComponent<MoveableObject>() != null)
			{
				Debug.Log ("Detected collision with Ray2D");
				transform.parent.GetComponent<DebrisBorderHorz>().collWithObRight(hit.collider);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		// do nothing
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		// do nothing
	}
}