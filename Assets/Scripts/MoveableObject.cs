using UnityEngine;
using System.Collections;

public class MoveableObject : MonoBehaviour
{
	//PUBLIC VARIABLES
	public bool drift, rotateActivated, splitactivated;
	public float objectSize = 2; // small 1, medium 2, large 3 
    public GameObject splitter, splitterShard;
    public float splitterX, splitterY;
    public float splitShards = 4;

    public float driftSpeed = 100;
	private Rigidbody2D rb2d;
	private bool iAmAsteroid = false;

	public int knockBackImpact = 500;  // adjust the impact force
    public bool isTractored = false;
	public float angleCollisionDamage = 0f; //Angle of asteroid object collision

	//Moveable Object
	private Vector2 curVelocity;
	private float curSpeed;



	// Use this for initialization
	void Start ()
	{
		//get rigidbody component 
		rb2d = GetComponent<Rigidbody2D>();

        if (transform.localScale.x < 0.65)
        {
            objectSize = 1;
        }
        else if (transform.localScale.x >= 0.65 && transform.localScale.x <= 1.35)
        {
            objectSize = 2;
        }
        else
        {
            objectSize = 3;
        }

		if (drift)
		{
			float x = Random.Range(-1f, 1f);
			float y = Random.Range(-1f, 1f);
			Vector2 direction = new Vector2(x, y).normalized;
			rb2d.AddForce(direction * driftSpeed);
		}
        

        if ( this.gameObject.tag == "small" || this.gameObject.name == "AsteroidPlaceHolder" ) 
		{	
			iAmAsteroid = true;
		}

        //give the object a random rotation
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        transform.eulerAngles = euler;
    }

	// Update is called once per frame
	void Update ()
	{
        if (rotateActivated)
        {
            RotateLeft();
        }
    }

    void RotateLeft()
    {
        transform.Rotate(Vector3.forward);
    }


	void FixedUpdate()
	{
		curVelocity = rb2d.velocity.normalized;
		curSpeed = rb2d.velocity.magnitude;
	}

    void OnCollisionEnter2D(Collision2D col)
	{
        if (splitactivated)
        {
            
            splitterX = splitter.transform.position.x;
            splitterY = splitter.transform.position.y;

            isTractored = false;
            Destroy(splitter);
            for (int i = 0; i < splitShards; i++)
            {
                
                Instantiate(splitterShard, new Vector3(Random.Range(splitterX -objectSize*2, splitterX + objectSize*2), Random.Range(splitterY - objectSize*2, splitterY + objectSize*2), 0), Quaternion.identity);
            }
        }

        float _directionStatus;
		bool _playerDamaged = false;
		float _asteroidSpeed = 0f;
		float _asteroidDamageForce = 0f;
		const float _VELOCITYMINIMUM = 0f; //Change later
		const float _ASTEROIDFORCE = 1f; //Can change later

		Vector2 asteroidDirection;
		Player playerObject;
		Rigidbody2D playerRb2d;
		bool collidedWithPlayer = false;
		// Opposite direction of player
		Vector2 oppositeDirection = new Vector2(0, 0);

		if (col.gameObject.tag == "Player") {
			collidedWithPlayer = true;
		}

		if (iAmAsteroid && collidedWithPlayer) {
			playerObject = col.gameObject.GetComponent<Player> ();
			playerRb2d = col.gameObject.GetComponent<Rigidbody2D> ();

			if (playerObject && playerRb2d && !isTractored) 
			{	
                /*
				Debug.Log ("----------------------------------------------");
				Debug.Log ("Current object tag: " + this.gameObject.tag);
				Debug.Log ("Collided object tag: " + col.gameObject.tag);
                */
				_asteroidSpeed = rb2d.velocity.magnitude;
                /*
				Debug.Log ("Apos: " + col.transform.position);
				Debug.Log ("PPos: " + transform.position);
                */
				asteroidDirection = (col.transform.position - transform.position).normalized;
                /*
				Debug.Log ("Asteroid Direction: " + rb2d.velocity.normalized);
				Debug.Log ("Asteroid Direction from Player: " + (col.transform.position - transform.position));
				Debug.Log ("Asteroid Direction from Player Normalized: " + asteroidDirection);
				Debug.Log ("Asteroid rigidbody velocity: " + rb2d.velocity);
				Debug.Log ("Asteroid rigidbody velocity Normalized: " + rb2d.velocity.normalized);
                */

				_directionStatus = Vector2.Dot (asteroidDirection, curVelocity);

                /*
				Debug.Log ("Dot Product: " + _directionStatus);
                */

				if ((_asteroidSpeed > _VELOCITYMINIMUM && _directionStatus > angleCollisionDamage) || curSpeed == 0) 
				{
					_playerDamaged = true;
					_asteroidDamageForce = Mathf.Round (_asteroidSpeed * _ASTEROIDFORCE); 

                    /*
					print ("Player current health= " + playerObject.getHealth ());
                    */

					if (_playerDamaged) 
					{	
						playerObject.takeDamage (_asteroidDamageForce);
						// Increase the knockback to the player when hit by an asteroid 
						// (scaled with the speed of the asteroid )
						oppositeDirection = -playerRb2d.velocity.normalized;
						playerRb2d.AddForce(oppositeDirection * _asteroidSpeed * knockBackImpact);

						Debug.Log ("oppositeDirection: " + oppositeDirection);
						Debug.Log ("_asteroidSpeed " + _asteroidSpeed);
						Debug.Log ("Force applied: " + oppositeDirection * _asteroidSpeed * knockBackImpact);

                        /*
						print ("Player damage= " + _asteroidDamageForce);
						print ("Player health after damage= " + playerObject.getHealth ());
                        */
					}
                    /*
					print (_playerDamaged);
					print ("Velocity= " + _asteroidSpeed);
                    */
				}
			}
		}
	}

}