using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/* 
*   Script to handle all player controls
*   and stat changes
*
* Moved PlayerCollision code to main player script
//============================
//Amy Becerra
//Task Description (9/27): Create a collision function for the player that deals damage to the player when they collide with an asteroid and the asteroid has a particular velocity
//Task Description (10/2): Modify the code so that the player tests the velocity value component in the direction of the player, and takes damage only if that particular velocity is over a certain value (this means that a player colliding with an asteroid that is moving away from them will not damage them)
//Last edited : 10/4/16
//============================
*/
public class Player : MonoBehaviour {


    //variable to keep track of how many enemies the player has killed
    public static int killCounter;


    //static variable to allow all scripts to access player position directly
    public static Vector3 playerPos;


	//PLAYER COMPONENTS
	private Rigidbody2D rb2d;

	//Determines which control scheme to use.  Set to 1 or 2 (2 being the default value)
	// 1-> W (accelerate forward), S (accelerate backwards), A (Rotate Counter-Clockwise), D (Rotate Clockwise)
	// 2-> W (rotate/accelerate up), S (rotate/accelerate down), A (rotate/accelerate left), D (rotate/accelerate right) 
	public int playerMovementControlScheme = 2;


	// STAT VARIABLES
	const float PLAYER_SPEED = 40.0f;
	const float BRAKE_SPEED = 20.0f;
	public float rotationSpeed = 2.5f;
    const float MAX_FUEL = 100.0f;  //the maximum amount of fuel the player can have
	private float _enginePower = 0.0f;
	const float MAX_ENGINE_POWER = 40.0f;
	const float LINEAR_ENGINE_POWER_COEFFICIENT = 15.0f;

    public float nebulaMultiplier = 1.0f;
    public float tractorSlow = 0;

    public float playerStartingHealth;//< the amount of health the player begins with
	public float playerHealth;//< the player's current health
	public float playerMaxHealth;//< the maximum health the player can have

	public float healthRegen = 0;//Health to be healed over time
	public float RegenDuration = 0;//The duration of each tick of heal

    private float _bulletDamage = 5.0f;//<amount of damage a single bullet deals

    //variable for keeping track of the player's current fuel
    private float currentFuel;

    bool alive = true; //<bool for whether player is alive

	//Used for making U-Turns
	private float currentuTurnTime = 0;
	const float U_TURN_TIME = 0.01f;
	private Vector2 playerExitPos = new Vector2(0, 0);
	private Vector2 oppositeDirection = new Vector2(0, 0);
	private bool disablePlayerControl = false;

	//Values for boundary dimensions
	public int maxXBoundary = 0;
	public int maxYBoundary = 0;
	public int minXBoundary = 0;
	public int minYBoundary = 0;

    private int unit = 5;

	private int numberOfChecks = 0;

	//Joystick Variable
	// only applies if being built to a mobile platform creates 
	public VirtualJoystickMovement joystick;

	// Camera Shake variable
	CameraShake mainCam;

    //COLLISION VARIABLES
    private GameObject _asteroidInput;
    private bool _playerDamaged = false;
    private Rigidbody2D _asteroidRigidbody;
    private float _asteroidVelocity = 0f;
    private float _asteroidMinimum = 1f; //Minimum velocity of asteroid to deal damage to player, can be changed later
    private float _asteroidDamageForce = 0f;
    const float _ASTEROIDFORCECONSTANT = 2f; //Can change later
    public Vector2 asteroidDirection;
    //shield script in child object
    PlayerShield shield;

    //Audio
    public AudioSource AccelerateSound;
    bool engineOn = false;
    float accelerateVolume;

    // Use this for initialization
    void Start () 
	{

        maxXBoundary *= unit;
        maxYBoundary *= unit;
        minXBoundary *= unit;
        minYBoundary *= unit;

        //get rigidbody component of player object
        rb2d = GetComponent<Rigidbody2D> ();
        //get shield script
        shield = GetComponentInChildren<PlayerShield>();
        //set player health to starting health
        playerHealth = playerStartingHealth;

        mainCam = Camera.main.GetComponent<CameraShake>();

        currentFuel = MAX_FUEL;

        if (joystick == null && GameObject.Find("VirtualJoystickMovement")!= null)
        {
            joystick = GameObject.Find("VirtualJoystickMovement").GetComponentInChildren<VirtualJoystickMovement>();
        }

        
	}

	void Update ()
	{
        //update static vector for player position
        playerPos = transform.position;

		//Function to handle player movement
		ControlPlayer();
        LoseFuel();
        

		checkIfPlayerOutOfBounds(maxXBoundary, maxYBoundary, minXBoundary, minYBoundary);

		// The health regen will only occur when we are below max health
		if (alive && (playerHealth < playerMaxHealth))
		{
			healOverTime(healthRegen, RegenDuration);
		}

		//TEMP
		//temp code for damage testing
		if (Input.GetKeyDown (KeyCode.U))
			playerHealth += 5f;
		if (Input.GetKeyDown (KeyCode.J)) {
			playerHealth -= 5f;
			mainCam.shakeCam ();
		}
		//END TEMP

	}

    
      
    
	private void ControlPlayer()
	{
		//Removes player control if doing U-Turn for a set time
		if (currentuTurnTime <= 0)
		{
			if (playerMovementControlScheme == 1)
			{
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
                /*
			    Controls player movement
				    A and D rotates player
				    W and S accelerates and decelerates player
			    */
                float rotation = Input.GetAxis("Horizontal");
				float acceleration = Input.GetAxis("Vertical");

				if (acceleration == 0f){
					_enginePower = 0f;
                    //stop accelerate sound
                    if(engineOn)
                    {
                        engineOn = false;
                        StartCoroutine(FadeSoundAndEnd(AccelerateSound));   
                    }
                   
                    
                } else {
					_enginePower += Time.deltaTime * LINEAR_ENGINE_POWER_COEFFICIENT;
					_enginePower = Mathf.Clamp(_enginePower, 0, MAX_ENGINE_POWER);
                    //play accelerate sound
                    if(!engineOn)
                    {
                        engineOn = true;
                        StartCoroutine(FadeSoundAndStart(AccelerateSound));
                    }
                    

                }

				transform.Rotate(new Vector3(0, 0, -rotationSpeed * rotation));

				rb2d.AddForce(transform.up * ((_enginePower * nebulaMultiplier) - tractorSlow) * acceleration);
#elif UNITY_IOS || UNITY_ANDROID
				/*for mobile build the movement is determined by the joystick 
				* left or right rotates the player
				* up accelerates the player and down decelerates the player
				*/
				float rotation = joystick.inputValue().x;
				float acceleration = joystick.inputValue().y;

				if (acceleration == 0f){
					_enginePower = 0f;
                    //stop accelerate sound
                    if(engineOn)
                    {
                        engineOn = false;
                        StartCoroutine(FadeSoundAndEnd(AccelerateSound));   
                    }
				} else {
					_enginePower += Time.deltaTime * LINEAR_ENGINE_POWER_COEFFICIENT;
					_enginePower = Mathf.Clamp(_enginePower, 0, MAX_ENGINE_POWER);
                   //play accelerate sound
                    if(!engineOn)
                    {
                        engineOn = true;
                        StartCoroutine(FadeSoundAndStart(AccelerateSound));
                    }
				}

				transform.Rotate(new Vector3(0, 0, -rotationSpeed * rotation));

				rb2d.AddForce(transform.up * ((_enginePower * nebulaMultiplier) - tractorSlow) * acceleration);
#endif

            }
            else if (playerMovementControlScheme == 2)
			{
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
                //Store the current horizontal input in the float moveHorizontal.
                float moveHorizontal = Input.GetAxis ("Horizontal");

				//Store the current vertical input in the float moveVertical.
				float moveVertical = Input.GetAxis ("Vertical");

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

				//Use the two store floats to create a new Vector2 variable movement.
				Vector2 movement = new Vector2 (moveHorizontal, moveVertical).normalized;
				//Debug.Log(rb2d.velocity.magnitude);

				//Increase force the longer movement direction is inputted
				//Resets when input is in neutral position
				if (movement.magnitude == 0f){
					_enginePower = 0f;
                    //stop accelerate sound
                    if (engineOn)
                    {
                        engineOn = false;
                        StartCoroutine(FadeSoundAndEnd(AccelerateSound));
                    }
                } else {
					_enginePower += Time.deltaTime * LINEAR_ENGINE_POWER_COEFFICIENT;
					_enginePower = Mathf.Clamp(_enginePower, 0, MAX_ENGINE_POWER);
                    //play accelerate sound
                    if (!engineOn)
                    {
                        engineOn = true;
                        StartCoroutine(FadeSoundAndStart(AccelerateSound));
                    }
                }


                //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
				rb2d.AddForce(movement * ((_enginePower * nebulaMultiplier) - tractorSlow));

                //Rotates front of ship to direction of movement
                if (movement != Vector2.zero)
				{
					float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle,Vector3.forward), Time.deltaTime * rotationSpeed);
				}


#elif UNITY_IOS || UNITY_ANDROID

				//use the joystick input to create movement vector
				Vector2 movement = joystick.inputValue().normalized;
				Debug.Log(rb2d.velocity.magnitude);

				if (movement.magnitude == 0f){
					_enginePower = 0f;
                     //stop accelerate sound
                    if(engineOn)
                    {
                        engineOn = false;
                        StartCoroutine(FadeSoundAndEnd(AccelerateSound));   
                    }
				} else {
					_enginePower += Time.deltaTime * LINEAR_ENGINE_POWER_COEFFICIENT;
					_enginePower = Mathf.Clamp(_enginePower, 0, MAX_ENGINE_POWER);
                    //play accelerate sound
                    if(!engineOn)
                    {
                        engineOn = true;
                        StartCoroutine(FadeSoundAndStart(AccelerateSound));
                    }
				}

				//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
				rb2d.AddForce(movement * ((PLAYER_SPEED * nebulaMultiplier) - tractorSlow));

				//Rotates front of ship to direction of movement
				if (movement != Vector2.zero)
				{
				float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
				}
#endif



            }

        }
		currentuTurnTime = uTurnPlayer(currentuTurnTime); 
	}

	private void checkIfPlayerOutOfBounds(int maxX, int maxY, int minX, int minY)
	{
		//Shows error if invalid values
		if (maxX < minX || maxY < minY) 
		{
			Debug.LogError ("Input for boundaries are incorrect. Max values should be larger than corresponding min values.");
		}

		//Checks if player is out of designated zone
		if (transform.position.x >= maxX || transform.position.y >= maxY|| transform.position.x <= minX || transform.position.y <= minY) 
		{
			resetUTurnTime();
			numberOfChecks++;
			if (numberOfChecks == 1) {
				setPlayerExitPos (transform.position);
			}
		} 
		else 
		{
			numberOfChecks = 0;
		}
	}

	//Turns the player around back to point of entry and returns time remaining
	private float uTurnPlayer(float lengthOfTime)
	{
		if (lengthOfTime > 0)
		{
			lengthOfTime -= Time.deltaTime;

			//The unit vector of the opposite the direction the player was initialling heading
			oppositeDirection = (playerExitPos - (Vector2)transform.position).normalized;
			rb2d.AddForce(oppositeDirection * ((PLAYER_SPEED * nebulaMultiplier) - tractorSlow));
			disablePlayerControl = true;
			if (rb2d.velocity != Vector2.zero)
			{
				float angle = Mathf.Atan2(-(oppositeDirection.x), oppositeDirection.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
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

	// function for when the player takes damage. It will shake the main camera
	// when the player takes damage.
	public void takeDamage(float damage)
	{
		playerHealth -= damage;
        if(mainCam != null)
        {
            mainCam.shakeCam();
        }
		
	}
	public float getHealth()
	{
		return playerHealth;
	}
   
    public void setHealth(float h)
	{
		playerHealth = h;
	}

    public float getFuel01()
    {
        currentFuel = Mathf.Clamp(currentFuel, 0, MAX_FUEL);
        return currentFuel / MAX_FUEL;
    }

    //function for adding onto the player's current fuel
    public void gainFuel(float fuelAmount)
    {
        //increase fuel by fuel amount
        currentFuel += fuelAmount;
        //set to max if above max amount
        if (currentFuel > MAX_FUEL)
        {
            currentFuel = MAX_FUEL;
        }

        Debug.Log("Your current fuel amount is increasing = " + currentFuel);

    }

    //function that decreases the Player's fuel
    //decreases faster when moving
     void LoseFuel()
     {
        if(rb2d.velocity.magnitude < 2)
        {
            currentFuel -= Time.deltaTime;
        }
        else
        {
            currentFuel -= Time.deltaTime*1.5f;
        }
        //Debug.Log("CurrentFuel is: " +currentFuel);
     }

    //Function that grabs velocity from the asteroid object and stores it in a var, applies damage to player if velocity is high enough, calculates a force, and subtracts that force from player health
    //Function also calculates if asteroid and player are moving away from each other or are facing 
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "SceneLoader")
        {
            SceneManager.LoadScene("TitleMenu");
        }
        else if (col.gameObject.name == "AsteroidPlaceholder")
        {
            float _directionStatus;
            _asteroidInput = col.gameObject;
            _asteroidRigidbody = _asteroidInput.GetComponent<Rigidbody2D>();
            _asteroidVelocity = _asteroidRigidbody.velocity.magnitude;

            //Get direction vector from asteroid to player
            asteroidDirection = (col.transform.position - transform.position).normalized;

            //Find the Dot to see if they are facing the same way, facing opposite ways, etc..
            _directionStatus = Vector2.Dot(asteroidDirection, _asteroidRigidbody.velocity.normalized);

            if (_asteroidVelocity > _asteroidMinimum && _directionStatus >= 0f) //temporarily set to if the asteroid is moving, it deals damage automatically
            {
                _playerDamaged = true;
                _asteroidDamageForce = _asteroidVelocity * _ASTEROIDFORCECONSTANT * _directionStatus; //Damage to scale; Need requirements for damage 
                takeDamage(_asteroidDamageForce);
                print("Velocity= " + _asteroidVelocity);
            }
        }
        else if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("Player hit by bullet");
            _playerDamaged = true;
            //deal damage
            takeDamage(_bulletDamage);

        }
        //activate player shield
        if (shield != null)
        {
            //activate shield, send info if player was damaged
            shield.ActivateShield(_playerDamaged);

        }

        _playerDamaged = false;

    }

    /**
     * function to increase the counter of how many enemies the player has killed if the enemies' health reaches zero
     */
     public static void increaseKillCount()
     {
         killCounter++;
     }

    private IEnumerator FadeSoundAndEnd(AudioSource source)
    {
        StopCoroutine(FadeSoundAndStart(source));
        if (source.isPlaying)
        {
            while(source.volume > 0)
            {
                source.volume -= 0.1f;
                yield return new WaitForSeconds(0.3f);
            }
            source.Stop();
        }
        yield return null;
    }
    private IEnumerator FadeSoundAndStart(AudioSource source)
    {
        StopCoroutine(FadeSoundAndEnd(source));
        if (!source.isPlaying)
        {
            source.volume = 0;
            source.Play();
            while (source.volume < 1)
            {
                source.volume += 0.1f;
                yield return new WaitForSeconds(0.3f);
            }
            
        }
        yield return null;
    }

}
