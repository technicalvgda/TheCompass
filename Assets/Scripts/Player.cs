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
    private ParticleSystem partSys;
    private Color trailColor;
    private Color lowFuelColor = Color.green;

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
    const float FUEL_LOSS_VARIABLE = 0.75f;

    public float nebulaMultiplier = 1.0f;
    public float tractorSlow = 0;

    public float playerStartingHealth;//< the amount of health the player begins with
	public float playerHealth;//< the player's current health
	public float playerMaxHealth;//< the maximum health the player can have

	public float healthRegen = 5;//Health to be healed per second
	//public float RegenDuration = 0;//The duration of each tick of heal
    private bool isDamaged = false;
    public float damageTimeCounter = 10;//
    public float regenDelay = 7;

    //variable for keeping track of the player's current fuel
    private float currentFuel;

    bool alive = true; //<bool for whether player is alive

	//Used for making U-Turns
	private float currentuTurnTime = 0;
	const float U_TURN_TIME = 0.01f;
	private Vector2 playerExitPos = new Vector2(0, 0);
	private Vector2 oppositeDirection = new Vector2(0, 0);
	private bool restrictPlayerControl = false;

	//handles player movement for cutscenes
	private Vector2 _playerDestination = new Vector2 (0, 0);
	private bool _disablePlayerControl = false;
    private bool _disableFuelLoss = false;

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
    private Rigidbody2D _asteroidRigidbody;
    const float _ASTEROIDFORCECONSTANT = 2f; //Can change later
    public Vector2 asteroidDirection;
    //shield script in child object
    PlayerShield shield;

    //Audio
    private float MAX_VOLUME = 1; 
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
        healthRegen = 5;
        regenDelay = 7;
        //get rigidbody component of player object
        rb2d = GetComponent<Rigidbody2D> ();
        //get particle system
        partSys = transform.FindChild("ParticleTrail").GetComponent<ParticleSystem>();
        trailColor = partSys.startColor;
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

        StartCoroutine(healthDamageCoroutine());

        
	}

	void Update ()
	{
        //update static vector for player position
        playerPos = transform.position;

		//Function to handle player movement
		ControlPlayer ();
        LoseFuel();

		checkIfPlayerOutOfBounds(maxXBoundary, maxYBoundary, minXBoundary, minYBoundary);	

        

	}

	private void ControlPlayer()
	{
		//Removes player control if doing U-Turn for a set time
		if (!_disablePlayerControl && currentuTurnTime <= 0 )
        {
			if (playerMovementControlScheme == 1)
            {
                PlayerMovementSchemeOne();
            }
            else if (playerMovementControlScheme == 2)
            {
                PlayerMovementSchemeTwo();
            }
        }
		currentuTurnTime = uTurnPlayer (currentuTurnTime);
	}
	private void PlayerMovementSchemeOne()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        float rotation = Input.GetAxis("Horizontal"); //<  A and D rotates player
        float acceleration = Input.GetAxis("Vertical"); //< W and S accelerates and decelerates player
#elif UNITY_IOS || UNITY_ANDROID
		/*for mobile build the movement is determined by the joystick*/
		float rotation = joystick.inputValue().x; //<left or right rotates the player
		float acceleration = joystick.inputValue().y; //< up accelerates the player and down decelerates the player				
#endif
        HandleEngine(acceleration);
        transform.Rotate(new Vector3(0, 0, -rotationSpeed * rotation));
        rb2d.AddForce(transform.up * ((_enginePower * nebulaMultiplier) - tractorSlow) * acceleration);
    }

    private void PlayerMovementSchemeTwo()
    {
        Vector2 movement;
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        /* Disables player control until player is either not inputting movement or away from where they were initially heading
         * NOTE: the reason why the comparison is  <= 0 is because the opposite direction is inversed when passing the point of entry
         */
        if (restrictPlayerControl)
        {
            if ((moveHorizontal * oppositeDirection.x) <= 0 && (moveVertical * oppositeDirection.y) <= 0)
            { restrictPlayerControl = false; }
            else
            {
                moveVertical = 0;
                moveHorizontal = 0;
            }
        }
        //Use the two store floats to create a new Vector2 variable movement.
        movement = new Vector2(moveHorizontal, moveVertical).normalized;
#elif UNITY_IOS || UNITY_ANDROID
				//use the joystick input to create movement vector
				movement = joystick.inputValue().normalized;
#endif
        //Increase force the longer movement direction is inputted
        //Resets when input is in neutral position
        HandleEngine(movement.magnitude);
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * ((_enginePower * nebulaMultiplier) - tractorSlow));
        //Rotates front of ship to direction of movement
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
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
			restrictPlayerControl = true;
			if (rb2d.velocity != Vector2.zero)
			{
				float angle = Mathf.Atan2(-(oppositeDirection.x), oppositeDirection.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
			}

		}
		return lengthOfTime;
	}

	//Disable player control and takes it to a determined destination
	public void cutSceneMovePlayer(float speed)
	{
		Vector2 deltaDestination = (_playerDestination - (Vector2)transform.position).normalized;

		//Debug.Log (deltaDestination);
		rb2d.AddForce (deltaDestination * speed);
		setDisablePlayerControl (true);
		if (rb2d.velocity != Vector2.zero)
		{
			float angle = Mathf.Atan2(-(deltaDestination.x), deltaDestination.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
		}
	}

	public void setPlayerDestination( Vector2 dest ) 
	{
		_playerDestination = dest;
	}

	public void resetUTurnTime()
	{
		currentuTurnTime = U_TURN_TIME;
	}

	private void setDisablePlayerControl(bool value) 
	{
		_disablePlayerControl = value;
	}

	public void setPlayerExitPos(Vector2 exitPos)
	{
		playerExitPos = exitPos;
	}

    //Health regen coroutine when the player is damaged it will heal at a rate of 5 persecond but whenever a player is damaged the player must not take damage for 10 seconds before he will regenerate again 
    IEnumerator healthDamageCoroutine()
    {
        //Debug.Log("coroutine start ");
        while (true)
        {
            if (isDamaged)
            {
                while(damageTimeCounter < regenDelay)
                {
                    yield return new WaitForSeconds(1f);
                    damageTimeCounter++;
                    Debug.Log(damageTimeCounter);
                    
                }
                isDamaged = false;
            }
            else if (playerHealth < playerMaxHealth)
            {
                gainHealth(healthRegen);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                //Debug.Log("blah");  
            }
               
        }
               
    }

	public void gainHealth(float health)
	{
		playerHealth += health;
        //if players health ever goes above max health will cap it at max health
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }

	// function for when the player takes damage. It will shake the main camera
	// when the player takes damage.
	public void takeDamage(float damage)
	{
        //skip damage if this is the bowling scene
        if(SceneManager.GetActiveScene().name == "Bowling")
        { return; }

		playerHealth -= damage;
        if(mainCam != null)
        {
            mainCam.shakeCam();
        }

        damageTimeCounter = 0;
        isDamaged = true;
		
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
    }

    public void DisableFuelLoss()
    {
        _disableFuelLoss = true;
    }
    //function that decreases the Player's fuel
    //decreases faster when moving
     void LoseFuel()
     {
        //skip fuel loss if this is the bowling scene
        if (SceneManager.GetActiveScene().name == "Bowling" || _disableFuelLoss == true)
        { return; }

        if (rb2d.velocity.magnitude < 2)
        {
            currentFuel -= Time.deltaTime * FUEL_LOSS_VARIABLE;
        }
        else
        {
            currentFuel -= Time.deltaTime*1.5f * FUEL_LOSS_VARIABLE;
        }
        //if fuel is less than 40%
        if(currentFuel < (MAX_FUEL*0.4f))
        {
            partSys.startColor = lowFuelColor;
        }
        else
        {
            partSys.startColor = trailColor;
        }
     }

    //Function that grabs velocity from the asteroid object and stores it in a var, applies damage to player if velocity is high enough, calculates a force, and subtracts that force from player health
    //Function also calculates if asteroid and player are moving away from each other or are facing 
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "SceneLoader")
        {
            SceneManager.LoadScene("TitleMenu");
        }   
    }

    
    //function to increase the counter of how many enemies the player has killed if the enemies' health reaches zero
    public static void increaseKillCount()
    {killCounter++;}

    public void ActivatePlayerShield(bool damageDealt)
    {
        //activate player shield
        if (shield != null)
        {
            //activate shield, send info if player was damaged
            shield.ActivateShield(damageDealt);
        }
    }

    //Increase force the longer movement direction is inputted
    //Resets when input is in neutral position
    private void HandleEngine(float movement)
    {
        if (movement == 0f)
        {
            _enginePower = 0f;
            if (engineOn)
            {
                engineOn = false;
                //StopCoroutine(FadeSoundAndEnd(AccelerateSound));
                //StopAllCoroutines();
                StopCoroutine("FadeSoundAndStart");
                StopCoroutine("FadeSoundAndEnd");
                StartCoroutine(FadeSoundAndEnd(AccelerateSound));
            }
        }
        else
        {
            _enginePower += Time.deltaTime * LINEAR_ENGINE_POWER_COEFFICIENT;
            _enginePower = Mathf.Clamp(_enginePower, 0, MAX_ENGINE_POWER);
            if (!engineOn)
            {
                engineOn = true;
                //StopCoroutine(FadeSoundAndStart(AccelerateSound));
                //StopAllCoroutines();
                StopCoroutine("FadeSoundAndStart");
                StopCoroutine("FadeSoundAndEnd");
                StartCoroutine(FadeSoundAndStart(AccelerateSound));
            }
        }
    }

    private IEnumerator FadeSoundAndEnd(AudioSource source)
    {   
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
        //set max volume to the current max
        MAX_VOLUME = SoundSettingCompare("FXSlider");
  
        if (!source.isPlaying)
        {
            source.volume = 0;
            source.Play();
            while (source.volume < MAX_VOLUME)
            {
                source.volume += 0.1f;
                yield return new WaitForSeconds(0.3f);
            } 
        }
        yield return null;
    }

    //send in the playerpref for "BGSlider" or "FXSlider" and compare it against master volume
    //return the lower of the two
    private float SoundSettingCompare(string prefName)
    {
       float compareVolume = PlayerPrefs.GetFloat(prefName);
       float masterVolume = PlayerPrefs.GetFloat("MSTRSlider");
       if (masterVolume > compareVolume)
       {
            return compareVolume;
       }   
       else
       {
            return masterVolume;
       }        
    }
}
