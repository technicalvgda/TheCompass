using UnityEngine;
using System.Collections;

public class CircularPatrol : MonoBehaviour {
    //private int controller;
    private int currentPath;
    private int patrolDistance;
    public int totalDistance; // Can be changed in unity
    private int patrol;
	// Use this for initialization
	void Start () {
        //controller = 5;
        currentPath = 0;
        patrolDistance = 0;
        patrol = 1;
	}
	
	/* Creates a circular path for the object to patrol */
	void Update () {
        // Test to see in what direction the object will go
        switch (currentPath)
        {
            case 0:
                // Object moves straight to the right
                transform.Translate((Time.deltaTime * patrol), 0, 0);
                break;
            case 1:
                // Object moves up while moving right
                transform.Translate((Time.deltaTime * patrol), (Time.deltaTime * patrol), 0);
                break;
            case 2:
                // Object moves up while moving left
                transform.Translate(-(Time.deltaTime * patrol), (Time.deltaTime * patrol), 0);
                break;
            case 3:
                // Object moves straight to the left
                transform.Translate(-(Time.deltaTime * patrol), 0, 0);
                break;
            case 4:
                // Object moves down and to the left
                transform.Translate(-(Time.deltaTime * patrol), -(Time.deltaTime * patrol), 0);
                break;
            case 5:
                // Object moves down and right
                transform.Translate((Time.deltaTime * patrol), -(Time.deltaTime * patrol), 0);
                break;
        }

        // Tests to see if the patrol distance
        if (patrolDistance >= totalDistance)
        {
            // Resets the patrolDistance
            patrolDistance = 0;

            // test if the current path is less than the controller
            if (currentPath <= 5)
            {
                // Increments the current path
                currentPath += 1;
            }

            else
            {
                // Resets the current path
                currentPath = 0;
            }
        }

        else
        {
            // Increments the patrol distance
            patrolDistance += 1;

        }
    }
}
