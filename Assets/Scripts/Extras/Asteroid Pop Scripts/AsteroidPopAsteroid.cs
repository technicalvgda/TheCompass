using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidPopAsteroid : MonoBehaviour {
	public bool isShootable,nextInLine,isShot;
	public string color;
	public List<GameObject> adjacentAsteroids;
	public Collider2D[] hitColliders;
	public float moveSpeed;
	private AsteroidPopManager _manager;
	private Vector3 mouseClickPos;
	private float _step;
	private Rigidbody2D rb2d;
	private Vector2 _forceVector;
	// Use this for initialization
	void Start () {		
		_step = 75 * Time.deltaTime;
		_manager = GameObject.Find ("AsteroidPopManager").GetComponent<AsteroidPopManager> ();
		Debug.Log (color);
		hitColliders = Physics2D.OverlapCircleAll (transform.position, 2.5f);
		collectSameColoredAdjacentAsteroids ();
		rb2d = transform.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isShootable) 
		{
			if (nextInLine)
			{
				if(Input.GetMouseButtonDown(0))
				{
					mouseClickPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					mouseClickPos.z = 0;
					_forceVector = new Vector3 (mouseClickPos.x - transform.position.x, mouseClickPos.y - transform.position.y);
					Debug.Log (_forceVector);
					//asteroidShot ();
					isShot = true;
					rb2d.isKinematic = false;
					isShootable = false;
					nextInLine = false;
					StartCoroutine (SignalManagerAsteroidFired ());
				}
			}
		}
		if (isShot) 
		{
			
			rb2d.AddForce (_forceVector);

		}
		
	}
	void collectSameColoredAdjacentAsteroids()
	{
		for (int i = 0; i < hitColliders.Length; i++) 
		{
			if (hitColliders [i].GetComponent<AsteroidPopAsteroid> ().color == color) 
			{
				adjacentAsteroids.Add (hitColliders [i].gameObject);
				adjacentAsteroids.Remove (transform.gameObject);
			}
		}
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.GetComponent<AsteroidPopAsteroid>()) 
		{
			Debug.Log ("HIT");
			rb2d.isKinematic = true;
			rb2d.velocity = Vector3.zero;
			rb2d.angularVelocity = 0;
			transform.parent = _manager.transform;
		}
	}
	public void prepareAsteroid(Vector3 launchPos)
	{
		nextInLine = true;
		StartCoroutine (MoveAsteroidUp (launchPos));

	}
	IEnumerator SignalManagerAsteroidFired()
	{
		yield return new WaitForSeconds (1);
		_manager.AsteroidFired (this.gameObject);
	}
	IEnumerator MoveAsteroidUp(Vector3 launchPos)
	{
		while (transform.position.y < launchPos.y) 
		{
			transform.position = new Vector3 (transform.position.x, transform.position.y + Time.deltaTime * 20.0f, 0);
			yield return new WaitForSeconds (0.1f);
		}
	}
}

