using UnityEngine;
using System.Collections;

public class MoveableObject : MonoBehaviour
{
    //PUBLIC VARIABLES
    public bool drift;
    public float objectSize = 5;

    private float driftSpeed = 100;
    private Rigidbody2D rb2d;

   

    // Use this for initialization
    void Start ()
    {
        //get rigidbody component 
        rb2d = GetComponent<Rigidbody2D>();
        if (drift)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            Vector2 direction = new Vector2(x, y).normalized;
            rb2d.AddForce(direction * driftSpeed);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {

        
    }
}
