using UnityEngine;
using System.Collections;

public class SplitterShard2 : MonoBehaviour 
{
	//PUBLIC VARIABLES
	public bool drift = true;
	public float objectSize = 1;

	private float driftSpeed = 1000;
	private Rigidbody2D rb2d;


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
	}

	void Update ()
	{

	}
}
