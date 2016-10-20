using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidPopAsteroid : MonoBehaviour {
	public string color;
	public List<GameObject> adjacentAsteroids;
	public Collider2D[] hitColliders;
	public Vector3 _center;
	// Use this for initialization
	void Start () {		
		Debug.Log (color);
	}
	
	// Update is called once per frame
	void Update () {
		hitColliders = Physics2D.OverlapCircleAll (transform.position, 2.5f);
	}
}
