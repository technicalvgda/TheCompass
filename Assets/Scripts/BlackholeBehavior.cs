//Code tested on the "AsteroidCollisionTestScene" scene


using UnityEngine;
using System.Collections;

public class BlackholeBehavior : MonoBehaviour {

	//private int _blackHoleRadius = 100;
	private Rigidbody2D _rb2d;
	public int _gravityScale = 20;
	public float _blackHoleBoundary = 7f;
	public int _acceleration = 950;

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
	}

	void FixedUpdate()
	{
		//Vector2 blackHolePos = transform.position;
		GameObject[] spaceObjects = FindObjectsOfType<GameObject>();

		foreach(GameObject _spaceObject in spaceObjects)
		{
			_rb2d = _spaceObject.GetComponent<Rigidbody2D>();

			if (_spaceObject && _spaceObject.transform != transform && _rb2d)
			{
				Vector2 distance = transform.position - _spaceObject.transform.position;
				float near = Vector2.Distance(_spaceObject.transform.position, transform.position);

				if (near > _blackHoleBoundary)//boundary set; if outside the inner boundary of black hole, object is still being pulled
				{ 
					_rb2d.AddForce((_acceleration/near) * distance.normalized * _rb2d.mass * _gravityScale * Time.fixedDeltaTime , ForceMode2D.Force );
				}
				else 
				{
					Destroy (_spaceObject.gameObject); 
				} 
			} 
		}
	}

}
