using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    const float PLAYER_SPEED = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	

	// Update is called once per frame
	void Update ()
    {
	
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
            transform.position += (transform.right * PLAYER_SPEED);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (-transform.right * PLAYER_SPEED);
        }
    }
}
