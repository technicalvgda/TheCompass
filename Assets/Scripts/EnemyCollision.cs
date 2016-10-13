using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour 
{
	private GameObject _asteroidInput;
	private Rigidbody2D _asteroidRigidBody;
	private float _asteroidVelocity = 0f;
	private float _asteroidMinimum = 0f;
	// Use this for initialization
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
				Destroy (gameObject);
			}
		}
	}
}
