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

	//Used for making U-Turns
	private float currentuTurnTime = 0;
	const float U_TURN_TIME = 0.01f;
	private Vector2 playerExitPos = new Vector2 (0, 0);
	private Vector2 oppositeDirection = new Vector2 (0,0);
	private bool disablePlayerControl = false;

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
		//Removes player control if doing U-Turn for a set time
		if (currentuTurnTime <= 0) {
			if (playerMovementControlScheme == 1) {
				/*
			Controls player movement
				A and D rotates player
				W and S accelerates and decelerates player
			*/
				float rotation = Input.GetAxis ("Horizontal");
				float acceleration = Input.GetAxis ("Vertical");

				transform.Rotate (new Vector3 (0, 0, -ROTATION_SPEED * rotation));

				rb2d.AddForce (transform.up * PLAYER_SPEED * acceleration);
			} else if (playerMovementControlScheme == 2) {


				//Store the current horizontal input in the float moveHorizontal.
				float moveHorizontal = Input.GetAxis ("Horizontal");

				//Store the current vertical input in the float moveVertical.
				float moveVertical = Input.GetAxis ("Vertical");
		

				/*
				 * Disables player control until player is either not inputting movement or away from where they were initially heading
				 * NOTE: the reason why the comparison is  <= 0 is because the opposite direction is inversed when passing the point of entry
				 */ 
				if (disablePlayerControl) {
					if ((moveHorizontal * oppositeDirection.x) <= 0 && (moveVertical * oppositeDirection.y) <= 0) {
						disablePlayerControl = false;
					} else {
						moveVertical = 0;
						moveHorizontal = 0;
					}
				}

				//Use the two store floats to create a new Vector2 variable movement.
				Vector2 movement = new Vector2 (moveHorizontal, moveVertical);


				//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
				rb2d.AddForce (movement * PLAYER_SPEED);

				//Rotates front of ship to direction of movement
				if (movement != Vector2.zero) {
					float angle = Mathf.Atan2 (-movement.x, movement.y) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle, Vector3.forward), Time.deltaTime * ROTATION_SPEED);
				}
			
			}
		}
		currentuTurnTime = uTurnPlayer (currentuTurnTime);

	}

	//Turns the player around back to point of entry and returns time remaining
	private float uTurnPlayer (float lengthOfTime)
	{
		if (lengthOfTime > 0)
		{
			lengthOfTime -= Time.deltaTime;

			//The unit vector of the opposite the direction the player was initialling heading
			oppositeDirection = (playerExitPos - (Vector2)transform.position).normalized;
			rb2d.AddForce (oppositeDirection * (PLAYER_SPEED));
			disablePlayerControl = true;
			if (rb2d.velocity != Vector2.zero)
			{
				float angle = Mathf.Atan2(-(oppositeDirection.x), oppositeDirection.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle,Vector3.forward), Time.deltaTime * ROTATION_SPEED);
			}
				
		}
		return lengthOfTime;
	}

	public void resetUTurnTime () 
	{
		currentuTurnTime = U_TURN_TIME;
	}

	public void setPlayerExitPos(Vector2 exitPos)
	{
		playerExitPos = exitPos;
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
