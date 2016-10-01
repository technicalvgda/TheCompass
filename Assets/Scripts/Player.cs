using UnityEngine;
using System.Collections;
/* 
*   Script to handle all player controls
*   and stat changes
*
*/
public class Player : MonoBehaviour {

	private float currentuTurnTime = 0;
	const float U_TURN_TIME = 0.2f;
	private enum Bounds { Top, Right, Bottom, Left};
	private Bounds boundHit;

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
		//Debug.Log (transform.position.x + ", " + transform.position.y);
		if (Input.GetKeyDown (KeyCode.U))
			playerHealth += 5f;
		if (Input.GetKeyDown (KeyCode.J))
			playerHealth -= 5f;

    }

    private void ControlPlayer()
    {
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

				//Use the two store floats to create a new Vector2 variable movement.
				Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
				//Debug.Log(rb2d.velocity.magnitude);


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
		//Debug.Log ("Time " + currentuTurnTime);

	}
		

	private float uTurnPlayer (float lengthOfTime)
	{
		/*
		if (Input.GetKeyDown ("space")) {
			lengthOfTime = U_TURN_TIME;
		}*/
		if (lengthOfTime > 0)
		{
			//Debug.Log ("U-Turn Player");
			int xMov = 0;
			int yMov = 0;
			lengthOfTime -= Time.deltaTime;
			if (transform.position.x < 0) {
				xMov = 1;
			} else {
				xMov = -1;
			}
			if (transform.position.y < 0) {
				yMov = 1;
			} else {
				yMov = -1;
			}
			/*
			switch (boundHit) 
			{
				case Bounds.Right:
					xMov = -1;
					break;
				case Bounds.Left:
					xMov = 1;
					break;
				case Bounds.Top:
					yMov = -1;
					break;
				case Bounds.Bottom:
					yMov = 1;
					break;
				default:
					break;

			}
			*/

			Vector2 oppositeDirection = new Vector2(xMov, yMov);
			rb2d.AddForce (oppositeDirection * (PLAYER_SPEED/2));

			if (rb2d.velocity != Vector2.zero)
			{
				float angle = Mathf.Atan2(-(xMov), yMov) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle,Vector3.forward), Time.deltaTime * ROTATION_SPEED);
			}

			//rb2d.velocity = Vector2.Lerp (rb2d.velocity, rb2d.velocity * -1, Time.deltaTime);
		}
		return lengthOfTime;
	}

	public void resetUTurnTime () {
		currentuTurnTime = U_TURN_TIME;
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if (other.name == "RightBoundary") 
		{
			boundHit = Bounds.Right;
			resetUTurnTime ();
		} 
		else if (other.name == "LeftBoundary") 
		{
			boundHit = Bounds.Left;
			resetUTurnTime ();
		} 
		else if (other.name == "TopBoundary") 
		{
			boundHit = Bounds.Top;
			resetUTurnTime ();
		} 
		else if (other.name == "BottomBoundary") 
		{
			boundHit = Bounds.Bottom;
			resetUTurnTime ();
		}
		//Debug.Log (boundHit);
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
