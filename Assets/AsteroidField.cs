/* Creates an asteroid belt where asteroids spawn in a random position within a defined field*/
using UnityEngine;
using System.Collections;

public class AsteroidField : MonoBehaviour
{
	public GameObject asteroid;
	public int numOfAsteroids;
	private GameObject[] _asteroidField;
	private bool _instantiating;
	private int _currentIndex;
	public float fieldRadius = 2;
	public float size; //size of asteroid
	public float movementSpeed;
	public float spawnSpeed;
	private float _spawnCounter;
	public Vector2 direction = new Vector2(1, 1).normalized;

	// Use this for initialization
	void Start ()
	{
		_asteroidField = new GameObject[numOfAsteroids];
		_spawnCounter = 0f;
		_currentIndex = 0;
		_instantiating = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_instantiating)
		{
			_spawnCounter += Time.deltaTime;
			if (_spawnCounter >= spawnSpeed)
			{
				_spawnCounter = 0;
				Vector2 spawnPos = transform.position;
				spawnPos.x += Random.value * fieldRadius - fieldRadius/2;
				spawnPos.y += Random.value * fieldRadius - fieldRadius/2;
				_asteroidField [_currentIndex] = (GameObject)Instantiate (asteroid, spawnPos, Quaternion.identity);
				Rigidbody2D rgbd = _asteroidField[_currentIndex].GetComponent<Rigidbody2D>();
				Vector2 v = direction;
				float Angle = Mathf.Atan2(direction.y, direction.x);
				v.x *= Mathf.Cos(Angle) * movementSpeed;
				v.y *= Mathf.Sin(Angle) * movementSpeed;
				rgbd.velocity = v; 
				if (++_currentIndex >= numOfAsteroids)
				{
					_instantiating = false;
				}
			}
		}
	}
}
