using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour 
{
	private GameObject _asteroidInput;
	private Rigidbody2D _asteroidRigidBody;
	private float _asteroidVelocity = 0f;
	private float _asteroidMinimum = 0f;
	public GameObject Items;
	public float Health = 3;
	public float Damage = 1;

	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.name == "AsteroidPlaceHolder") 
		{
			_asteroidInput = col.gameObject;
			_asteroidRigidBody = _asteroidInput.GetComponent<Rigidbody2D> ();
			_asteroidVelocity = _asteroidRigidBody.velocity.magnitude;
			if (_asteroidVelocity > _asteroidMinimum) 
			{
				Health = Health - Damage;
			}
			if (Health <= 0) 
			{
				Destroy (gameObject);
				Instantiate (Items, transform.position, Quaternion.identity);
			}
		}
	}
}
