﻿using UnityEngine;
using System.Collections;
/* 
*   Script to handle all player controls
*   and stat changes
*
*/
public class Player : MonoBehaviour {

    //PLAYER COMPONENTS
    private Rigidbody2D rb2d;

    // STAT VARIABLES
    const float PLAYER_SPEED = 40.0f;
    const float BRAKE_SPEED = 20.0f;
    const float ROTATION_SPEED = 5.0f;

    public float playerStartingHealth;//< the amount of health the player begins with
    private float playerHealth;//< the player's current health
    bool alive = true; //<bool for whether player is alive

    // Use this for initialization
    void Start () 
	{
        //get rigidbody component of player object
		rb2d = GetComponent<Rigidbody2D> ();
        //set player health to starting health
        playerHealth = playerStartingHealth;
	}

	void Update ()
    {
        //Function to handle player movement
        ControlPlayer();
		if (Input.GetKeyDown (KeyCode.U))
			playerHealth += 5f;
		if (Input.GetKeyDown (KeyCode.J))
			playerHealth -= 5f;

    }

    private void ControlPlayer()
    {
        /*
		Controls player movement
			A and D rotates player
			W and S accelerates and decelerates player
		*/
        float rotation = Input.GetAxis("Horizontal");
        float acceleration = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(0, 0, -ROTATION_SPEED * rotation));

        rb2d.AddForce(transform.up * PLAYER_SPEED * acceleration);
    }


    public void gainHealth(float health)
    {
        playerHealth += health;
    }

    public void takeDamage(float damage)
    {
        playerHealth -= damage;
    }
	public float getHealth()
	{
		return playerHealth;
	}
	public void setHealth(float h)
	{
		playerHealth = h;
	}
}
