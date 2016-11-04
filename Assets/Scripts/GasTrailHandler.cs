using UnityEngine;
using System.Collections;

public class GasTrailHandler : MonoBehaviour {

    public Transform GasGiant;
    public Transform BlackHole;

    //amount to move particle emission from center of gas giant
    float offset;
    //amount to adjust emitter to match border of gas giant
    float adjustment = 10;

	// Use this for initialization
	void Start ()
    {
        //gets radius of gas giant, assumes width and height are the same
        offset = GasGiant.GetComponent<SpriteRenderer>().bounds.size.x/2 - adjustment;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SetRotationOfTrail();
	}

    //Gets proper direction of trail
    void SetRotationOfTrail()
    {
       
        //get the heading from gas giant to black hole
        Vector2 Heading = BlackHole.position - GasGiant.position;
        //face gas trail towards vector
        transform.rotation = Quaternion.LookRotation(-Heading);
        //offset position
        // = GasGiantPosition + (unitvector in proper direction * offsetMagnitude)
        transform.position = GasGiant.position + ((Vector3)Heading.normalized * offset);
    }
    
}
