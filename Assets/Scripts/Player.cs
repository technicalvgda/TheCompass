using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    const float PLAYER_SPEED = 40.0f;
    const float BRAKE_SPEED = 20.0f;
    const float ROTATION_SPEED = 5.0f;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update ()
    {
		/*
		Controls player movement
			A and D rotates player
			W and S accelerates and decelerates player
		*/
        float rotation = Input.GetAxis("Horizontal");
        float acceleration = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(0,0,-ROTATION_SPEED * rotation));

        rb2d.AddForce (transform.up * PLAYER_SPEED * acceleration);


    }
}
