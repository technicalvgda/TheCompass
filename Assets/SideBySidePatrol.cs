using UnityEngine;
using System.Collections;

public class SideBySidePatrol : MonoBehaviour {
    public double totalDistance; // Can be adjusted in unity
    private int patrolDistance;
    public bool facingRight;


	// Use this for initialization
	void Start () {
        //totalDistance = 5;
        patrolDistance = 0;
        facingRight = true;
	}
	
	/* This moves the object from left to right
     * so the object will be patrolling from left to right 
     */
	void Update () {
        // Tests to see if the object has reached its total patrol distance
        if (patrolDistance < totalDistance)
        {
            // Test to see what side the object is facing in order to move it
            // in the correct direction
            if (!facingRight)
            {
                // Moves the object to the left
                transform.Translate(-(Time.deltaTime * patrolDistance), 0, 0);
            }

            else
            {
                // Moves the object to the right
                transform.Translate((Time.deltaTime * patrolDistance), 0, 0);
            }
            
            // Increments the patrol distance
            patrolDistance += 1;
        }

        else
        {
            // Tests to see if the object was moving to the left
            // If it wasn't, flips the object to start moving tho the right
            // else vice versa
            facingRight = (!facingRight) ? true : false;

            // Resets the patrol distance to 0
            patrolDistance = 0;
        }
	}
}
