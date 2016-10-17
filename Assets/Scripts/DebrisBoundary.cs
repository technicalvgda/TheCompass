using UnityEngine;
using System.Collections;

public class DebrisBoundary : MonoBehaviour
{
	private float maxRayDistX, maxRayDistY;
	private float ctrXLoc, ctrYLoc;
	private Vector2 rectNWCorner,rectNECorner,rectSWCorner;
	Ray2D northSide,eastSide,southSide,westSide;
	RaycastHit2D hitNorth,hitEast,hitSouth,hitWest;

	void FixedUpdate()
	{
		Debug.DrawLine (rectNWCorner, rectNWCorner + Vector2.right * maxRayDistX);
		Debug.DrawLine (rectNECorner, rectNECorner + Vector2.down * maxRayDistY);
		Debug.DrawLine (rectSWCorner, rectSWCorner + Vector2.right * maxRayDistX);
		Debug.DrawLine (rectNWCorner, rectNWCorner + Vector2.down * maxRayDistY);
	}
	void Start ()
	{
		maxRayDistX = GetComponent<RectTransform>().sizeDelta.x; // stretches the width
		maxRayDistY = GetComponent<RectTransform>().sizeDelta.y; // stretches the height
		ctrXLoc = transform.position.x;
		ctrYLoc = transform.position.y;
		rectNWCorner = new Vector2 (ctrXLoc - maxRayDistX / 2, ctrYLoc + maxRayDistY / 2);
		rectNECorner = new Vector2 (ctrXLoc + maxRayDistX / 2, ctrYLoc + maxRayDistY / 2);
		rectSWCorner = new Vector2 (ctrXLoc - maxRayDistX / 2, ctrYLoc - maxRayDistY / 2);
		northSide = new Ray2D (rectNWCorner, Vector2.right);
		eastSide = new Ray2D (rectNECorner, Vector2.down);
		southSide = new Ray2D (rectSWCorner, Vector2.right);
		westSide = new Ray2D (rectNWCorner, Vector2.down);
	}
	
	// Update is called once per frame
	void Update ()
	{
		hitNorth = Physics2D.Raycast (northSide.origin, northSide.direction);
		hitEast = Physics2D.Raycast (eastSide.origin, eastSide.direction);
		hitSouth = Physics2D.Raycast (southSide.origin, southSide.direction);
		hitWest = Physics2D.Raycast (westSide.origin, westSide.direction);
		if (hitNorth != false && hitNorth.collider != null && hitNorth.collider.GetComponent<MoveableObject> () != null &&
		    hitWest != false && hitWest.collider != null && hitWest.collider.GetComponent<MoveableObject> () != null) {
			//Debug.Log ("Detected collision with NW boundary");
			float offset = hitNorth.collider.GetComponent<CircleCollider2D> ().radius;
			hitNorth.collider.transform.position = new Vector3 (maxRayDistX / 2 - offset - 0.1f, -maxRayDistY / 2 + offset + 0.1f, 0);
		} 
		else if (hitNorth != false && hitNorth.collider != null && hitNorth.collider.GetComponent<MoveableObject> () != null &&
		         hitEast != false && hitEast.collider != null && hitEast.collider.GetComponent<MoveableObject> () != null)
		{
			//Debug.Log ("Detected collision with NE boundary");
			float offset = hitNorth.collider.GetComponent<CircleCollider2D> ().radius;
			hitNorth.collider.transform.position = new Vector3 (-maxRayDistX / 2 + offset + 0.1f, -maxRayDistY / 2 + offset + 0.1f, 0);
		}
		else if(hitSouth != false && hitSouth.collider != null && hitSouth.collider.GetComponent<MoveableObject> () != null &&
				hitEast != false && hitEast.collider != null && hitEast.collider.GetComponent<MoveableObject> () != null)
		{
			//Debug.Log ("Detected collision with SE boundary");
			float offset = hitSouth.collider.GetComponent<CircleCollider2D> ().radius;
			hitSouth.collider.transform.position = new Vector3 (-maxRayDistX / 2 + offset + 0.1f, maxRayDistY / 2 - offset - 0.1f, 0);
		}
		else if(hitSouth != false && hitSouth.collider != null && hitSouth.collider.GetComponent<MoveableObject> () != null &&
			hitWest != false && hitWest.collider != null && hitWest.collider.GetComponent<MoveableObject> () != null)
		{
			//Debug.Log ("Detected collision with SW boundary");
			float offset = hitSouth.collider.GetComponent<CircleCollider2D> ().radius;
			hitSouth.collider.transform.position = new Vector3 (maxRayDistX / 2 - offset - 0.1f, maxRayDistY / 2 - offset - 0.1f, 0);
		}
		else if (hitNorth != false && hitNorth.collider != null && hitNorth.collider.GetComponent<MoveableObject>() != null)
		{
			//Debug.Log ("Detected collision with North side boundary");
			float offset = hitNorth.collider.GetComponent<CircleCollider2D> ().radius;
			hitNorth.collider.transform.position = new Vector3 (hitNorth.collider.transform.position.x,- maxRayDistY / 2 + offset + 0.1f,0);
		}
		else if (hitEast != false && hitEast.collider != null && hitEast.collider.GetComponent<MoveableObject>() != null)
		{
			//Debug.Log ("Detected collision with East side boundary");
			float offset = hitEast.collider.GetComponent<CircleCollider2D> ().radius;
			hitEast.collider.transform.position = new Vector3 (- maxRayDistX / 2 + offset + 0.1f,0,0);
		}
		else if (hitSouth != false && hitSouth.collider != null && hitSouth.collider.GetComponent<MoveableObject>() != null)
		{
			//Debug.Log ("Detected collision with South side boundary");
			float offset = hitSouth.collider.GetComponent<CircleCollider2D> ().radius;
			hitSouth.collider.transform.position = new Vector3 (0,maxRayDistY / 2 - offset - 0.1f,0);
		}
		else if (hitWest != false && hitWest.collider != null && hitWest.collider.GetComponent<MoveableObject>() != null)
		{
			//Debug.Log ("Detected collision with West side boundary");
			float offset = hitWest.collider.GetComponent<CircleCollider2D> ().radius;
			hitWest.collider.transform.position = new Vector3 (maxRayDistX / 2 - offset - 0.1f,0,0);
		}
		else
		{
			//do nothing
		}
	}
}
