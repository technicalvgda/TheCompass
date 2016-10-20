using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidPopAsteroid : MonoBehaviour {
	public bool isShootable;
	public string color;
	public List<GameObject> adjacentAsteroids;
	public Collider2D[] hitColliders;
	// Use this for initialization
	void Start () {		
		Debug.Log (color);
		hitColliders = Physics2D.OverlapCircleAll (transform.position, 2.5f);
		collectSameColoredAdjacentAsteroids ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isShootable) 
		{
			
		}
		
	}
	void collectSameColoredAdjacentAsteroids()
	{
		for (int i = 0; i < hitColliders.Length; i++) 
		{
			if (hitColliders [i].GetComponent<AsteroidPopAsteroid> ().color == color)
				adjacentAsteroids.Add (hitColliders [i].gameObject);
		}
	}
}
