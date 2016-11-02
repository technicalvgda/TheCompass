using UnityEngine;
using System.Collections;

public class TetheredObject : MonoBehaviour {

    public int TetheredHealth = 5; //tethered object's health. Gets hit five times and health goes to zero. 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        {
            TetheredHealth -= 1; // tethered health loses 1 health point 
            if (TetheredHealth <= 0) // if tethered health reaches zero or below
            {
                Destroy(col.gameObject); //the collided object goes bye bye
            }
        }
    }
}
