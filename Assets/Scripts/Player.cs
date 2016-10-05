using UnityEngine;
using System.Collections;
/* 
*   Script to handle all player controls
*   and stat changes
*
*/
public class Player : MonoBehaviour {

    //PLAYER COMPONENTS
    private Rigidbody2D rb2d;

	//Determines which control scheme to use.  Set to 1 or 2
	public int playerMovementControlScheme = 1;

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
    	if(playerMovementControlScheme == 1)
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
		else if (playerMovementControlScheme == 2)
		{
			//Store the current horizontal input in the float moveHorizontal.
			float moveHorizontal = Input.GetAxis ("Horizontal");

			//Store the current vertical input in the float moveVertical.
			float moveVertical = Input.GetAxis ("Vertical");

			//Use the two store floats to create a new Vector2 variable movement.
			Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
			Debug.Log(rb2d.velocity.magnitude);


			//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
			rb2d.AddForce (movement * PLAYER_SPEED);

			//Rotates front of ship to direction of movement
			if (movement != Vector2.zero)
			{
				float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle,Vector3.forward), Time.deltaTime * ROTATION_SPEED);
			}
			
		}
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
