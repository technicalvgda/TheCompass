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

    //Determines which control scheme to use.  Set to 1 or 2 (2 being the default value)
    // 1-> W (accelerate forward), S (accelerate backwards), A (Rotate Counter-Clockwise), D (Rotate Clockwise)
    // 2-> W (rotate/accelerate up), S (rotate/accelerate down), A (rotate/accelerate left), D (rotate/accelerate right) 
    public int playerMovementControlScheme = 2;

    
    // STAT VARIABLES
    const float PLAYER_SPEED = 40.0f;
    const float BRAKE_SPEED = 20.0f;
    const float ROTATION_SPEED = 5.0f;

    public float playerStartingHealth;//< the amount of health the player begins with
    public float playerHealth;//< the player's current health
    public float playerMaxHealth;//< the maximum health the player can have

    public float healthRegen = 0;//Health to be healed over time
    public float RegenDuration = 0;//The duration of each tick of heal

    bool alive = true; //<bool for whether player is alive

    //Used for making U-Turns
    private float currentuTurnTime = 0;
    const float U_TURN_TIME = 0.01f;
    private Vector2 playerExitPos = new Vector2(0, 0);
    private Vector2 oppositeDirection = new Vector2(0, 0);
    private bool disablePlayerControl = false;


    //Joystick Variable
    // only applies if being built to a mobile platform creates 
    public VirtualJoystickMovement joystick;


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

        // The health regen will only occur when we are below max health
        if (alive && (playerHealth < playerMaxHealth))
        {
            healOverTime(healthRegen, RegenDuration);
        }

        //TEMP
        //temp code for damage testing
        if (Input.GetKeyDown (KeyCode.U))
			playerHealth += 5f;
		if (Input.GetKeyDown (KeyCode.J))
			playerHealth -= 5f;
        //END TEMP

    }

    private void ControlPlayer()
    {
        //Removes player control if doing U-Turn for a set time
        if (currentuTurnTime <= 0)
        {
            if (playerMovementControlScheme == 1)
	        {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
	            /*
			    Controls player movement
				    A and D rotates player
				    W and S accelerates and decelerates player
			    */
	            float rotation = Input.GetAxis("Horizontal");
	            float acceleration = Input.GetAxis("Vertical");

	            transform.Rotate(new Vector3(0, 0, -ROTATION_SPEED * rotation));

	            rb2d.AddForce(transform.up * PLAYER_SPEED * acceleration);
#elif UNITY_IOS || UNITY_ANDROID
                /*for mobile build the movement is determined by the joystick 
                 * left or right rotates the player
                 * up accelerates the player and down decelerates the player
                */
                float rotation = joystick.inputValue().x;
                float acceleration = joystick.inputValue().y;

                transform.Rotate(new Vector3(0, 0, -ROTATION_SPEED * rotation));

                rb2d.AddForce(transform.up * PLAYER_SPEED * acceleration);
#endif

            }
            else if (playerMovementControlScheme == 2)
		    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
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
			    if (movement != Vector2.zero)
			    {
				    float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
				    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle,Vector3.forward), Time.deltaTime * ROTATION_SPEED);
			    }

                  /*
                 * Disables player control until player is either not inputting movement or away from where they were initially heading
                 * NOTE: the reason why the comparison is  <= 0 is because the opposite direction is inversed when passing the point of entry
                 */
                if (disablePlayerControl)
                {
                    if ((moveHorizontal * oppositeDirection.x) <= 0 && (moveVertical * oppositeDirection.y) <= 0)
                    {
                        disablePlayerControl = false;
                    }
                    else
                    {
                        moveVertical = 0;
                        moveHorizontal = 0;
                    }
                }
#elif UNITY_IOS || UNITY_ANDROID

                //use the joystick input to create movement vector
                Vector2 movement = joystick.inputValue().normalized;
                Debug.Log(rb2d.velocity.magnitude);


                //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
                rb2d.AddForce(movement * PLAYER_SPEED);

                //Rotates front of ship to direction of movement
                if (movement != Vector2.zero)
                {
                    float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * ROTATION_SPEED);
                }
#endif

              

            }

        }
        currentuTurnTime = uTurnPlayer(currentuTurnTime); 
    }

    //Turns the player around back to point of entry and returns time remaining
    private float uTurnPlayer(float lengthOfTime)
    {
        if (lengthOfTime > 0)
        {
            lengthOfTime -= Time.deltaTime;

            //The unit vector of the opposite the direction the player was initialling heading
            oppositeDirection = (playerExitPos - (Vector2)transform.position).normalized;
            rb2d.AddForce(oppositeDirection * (PLAYER_SPEED));
            disablePlayerControl = true;
            if (rb2d.velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(-(oppositeDirection.x), oppositeDirection.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * ROTATION_SPEED);
            }

        }
        return lengthOfTime;
    }

    public void resetUTurnTime()
    {
        currentuTurnTime = U_TURN_TIME;
    }

    public void setPlayerExitPos(Vector2 exitPos)
    {
        playerExitPos = exitPos;
    }

    public void healOverTime(float healAmount, float duration)
    {
        StartCoroutine(healOverTimeCoroutine(healAmount, duration));
    }

    IEnumerator healOverTimeCoroutine(float healAmount, float duration)
    {
        float amountHealed = 0;
        float amountPerLoop = healAmount / duration;
        while (amountHealed < healAmount)
        {
            gainHealth(amountPerLoop);
            amountHealed += amountPerLoop;
            yield return new WaitForSeconds(1f);
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
