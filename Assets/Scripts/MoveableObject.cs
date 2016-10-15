using UnityEngine;
using System.Collections;

public class MoveableObject : MonoBehaviour
{
	//PUBLIC VARIABLES
	public bool drift, rotateActivated, splitactivated;

	public float objectSize = 5;

    public GameObject splitter, splitterShard;
    public float splitterX, splitterY;

    private float driftSpeed = 100;
	private Rigidbody2D rb2d;

	private bool iAmAsteroid = false;


	// Use this for initialization
	void Start ()
	{
		//get rigidbody component 
		rb2d = GetComponent<Rigidbody2D>();

		if (drift)
		{
			float x = Random.Range(-1f, 1f);
			float y = Random.Range(-1f, 1f);
			Vector2 direction = new Vector2(x, y).normalized;
			rb2d.AddForce(direction * driftSpeed);
		}

        if(splitactivated)
        {
            splitterX = splitter.transform.position.x;
            splitterY = splitter.transform.position.y;
        }
        

        if ( this.gameObject.tag == "small" || this.gameObject.name == "AsteroidPlaceHolder" ) 
		{	
			iAmAsteroid = true;
		}

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

    void OnCollisionEnter2D(Collision2D col)
	{

        if (splitactivated)
        {
            Destroy(splitter);
            for (int i = 0; i < 3; i++)
            {
                Instantiate(splitterShard, new Vector3(splitterX, splitterY, 0), Quaternion.identity);
            }
        }

        float _directionStatus;
		bool _playerDamaged = false;

		float _asteroidVelocity = 0f;
		float _asteroidMinimum = -100f; //Minimum velocity of asteroid to deal damage to player, can be changed later
		float _asteroidDamageForce = 0f;
		const float _VELOCITYMINIMUM = 0f; //Change later
		bool _objectVelocityMet = false;

		const float _ASTEROIDFORCECONSTANT = 1f; //Can change later

		Vector2 asteroidDirection;
		Player playerObject;

		bool collidedWithPlayer = false;

		if (col.gameObject.tag == "Player") {
			collidedWithPlayer = true;
		}

		if ( iAmAsteroid && collidedWithPlayer ) {

			playerObject = col.gameObject.GetComponent<Player> ();

			if (playerObject != null) {	

                /*
				Debug.Log ("----------------------------------------------");

				Debug.Log ("Current object tag: " + this.gameObject.tag);
				Debug.Log ("Collided object tag: " + col.gameObject.tag);
                */

				_asteroidVelocity = rb2d.velocity.magnitude;

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

				_directionStatus = Vector2.Dot (asteroidDirection, rb2d.velocity.normalized);

                /*
				Debug.Log ("Dot Product: " + _directionStatus);
                */

				if (_asteroidVelocity > _VELOCITYMINIMUM) 
				{
					_objectVelocityMet = true;
                    /*
					Debug.Log ("Object Velocity met?" + _objectVelocityMet);
                    */
				}

				if (_asteroidVelocity > _asteroidMinimum && _directionStatus > 0f) {
					_playerDamaged = true;
					_asteroidDamageForce = Mathf.Round (_asteroidVelocity * _ASTEROIDFORCECONSTANT); 

                    /*
					print ("Player current health= " + playerObject.getHealth ());
                    */

					if (_playerDamaged) {	
						playerObject.takeDamage (_asteroidDamageForce);

                        /*
						print ("Player damage= " + _asteroidDamageForce);
						print ("Player health after damage= " + playerObject.getHealth ());
                        */
					}
                    /*
					print (_playerDamaged);
					print ("Velocity= " + _asteroidVelocity);
                    */
				}
			}
		}
	}

}