using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    const float PLAYER_SPEED = 40.0f;
    const float BRAKE_SPEED = 20.0f;
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
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0,0,5));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0, -5));
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb2d.AddForce (transform.up * PLAYER_SPEED);

        }
        if (Input.GetKey(KeyCode.S))
        {
            rb2d.AddForce (-transform.up * BRAKE_SPEED);

        }
    }
}
