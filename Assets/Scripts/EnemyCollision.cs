using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour 
{
	private GameObject _asteroidInput;
	private Rigidbody2D _asteroidRigidBody;
	private float _asteroidVelocity = 0f;
	private float _enemyMinimum = 0f;
	public GameObject Items;
	public float Health = 3;
	public float Damage = 1;

	private Rigidbody2D rb2d;

	public int knockBackImpact = 5;  // adjust the impact force
	public float angleCollisionDamage = 0f; //Angle of asteroid object collision

	private Vector2 curVelocity;
	private float curSpeed;


	void Start () 
	{
		//get rigidbody component 
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () 
	{
	}


	void FixedUpdate()
	{
		curVelocity = rb2d.velocity.normalized;
		curSpeed = rb2d.velocity.magnitude;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		float _asteroidMinVelDamage = 0f; //Minimum velocity of asteroid to deal damage to player, can be changed later
		float _enemyDamageForce = 0f;
		const float _ASTEROIDFORCE = 1f; //Can change later
		EnemyControl enemyControl;

		bool collidedWithAsteroid = false;
		bool _enemyDamaged = false;

		Vector2 oppositeDirection = new Vector2(0, 0);

		if (col.gameObject.tag == "Debris") 
		{
			collidedWithAsteroid = true;
			_asteroidInput = col.gameObject;
			_asteroidRigidBody = _asteroidInput.GetComponent<Rigidbody2D> ();
			_asteroidVelocity = _asteroidRigidBody.velocity.magnitude;
			//if (_asteroidVelocity > _enemyMinimum) {
				//Health = Health - Damage;
			//}
			if (Health <= 0) 
			{
                // kill enemy
				Destroy (gameObject);
                // increase kill counter
                Player.increaseKillCount();
                Instantiate (Items, transform.position, Quaternion.identity);
			}


			if (collidedWithAsteroid) 
			{
				enemyControl = gameObject.GetComponent<EnemyControl> ();

				if (_asteroidRigidBody && enemyControl) { 


					if (_asteroidVelocity > _asteroidMinVelDamage)
					{
						_enemyDamaged = true;
						_enemyDamageForce = Mathf.Round (Mathf.Round (_asteroidVelocity * _ASTEROIDFORCE)); 


						if (_enemyDamaged) { 
							enemyControl.takeDamage (_enemyDamageForce);

							oppositeDirection = -rb2d.velocity.normalized;
							rb2d.AddForce (oppositeDirection * _asteroidVelocity * knockBackImpact);


						}                   

					}
				}
			}
		}
	}
}