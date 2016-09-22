using UnityEngine;
using System.Collections;

public class MoveableObject : MonoBehaviour
{
    public float objectSize = 5;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start ()
    {
        //get rigidbody component 
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
