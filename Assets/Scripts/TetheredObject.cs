using UnityEngine;
using System.Collections;

public class TetheredObject : MonoBehaviour
{
	private bool tetherOn;
	float xPos,yPos,zPos;
	private Color c1 = Color.white;
	private Vector3 playerPosition;
	private LineRenderer lineRen;
    public int TetheredHealth = 3; //tethered object's health. Gets hit three times and health goes to zero. 

	// Use this for initialization
	void Start ()
	{
		tetherOn = false;
		lineRen = gameObject.AddComponent<LineRenderer>();
		lineRen.material = new Material(Shader.Find("Particles/Additive"));
		lineRen.SetWidth(.1f , .1f);
		lineRen.SetColors (c1, c1);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (tetherOn == true)
		{
			xPos = GameObject.Find("PlayerPlaceholder").transform.position.x;
			yPos = GameObject.Find("PlayerPlaceholder").transform.position.y;
			zPos = GameObject.Find("PlayerPlaceholder").transform.position.z;
			playerPosition = new Vector3 (xPos, yPos, zPos);
			lineRen.SetPosition (0, playerPosition);
			while ((playerPosition - transform.position).magnitude > 10f)
			{
				transform.position = Vector3.MoveTowards (transform.position, playerPosition, .5f);
				GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				GetComponent<Rigidbody2D>().angularVelocity = 0;
				//Debug.Log (GetComponent<Rigidbody2D> ().velocity);
			}
			lineRen.SetPosition (1, transform.position);
		}
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
		if (coll.gameObject.name == "PlayerPlaceholder")
		{
			tetherOn = true;
			playerPosition = new Vector3(coll.transform.position.x,coll.transform.position.y,coll.transform.position.z);
		} 
		else
		{
			TetheredHealth -= 1; // tethered health loses 1 health point 
			if (TetheredHealth <= 0) // if tethered health reaches zero or below
			{
				tetherOn = false;
				Destroy (coll.gameObject); //the collided object goes bye bye
			}
		}
    }
}
