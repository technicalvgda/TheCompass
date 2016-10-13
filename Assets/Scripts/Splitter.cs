using UnityEngine;
using System.Collections;

public class Splitter : MonoBehaviour
{
	//PUBLIC VARIABLES
	public bool drift;
	public float objectSize = 5;
	public GameObject splitter, splitterShard;
	public float splitterX, splitterY;

	private float driftSpeed = 100;
	private Rigidbody2D rb2d;



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
		splitterX = splitter.transform.position.x;
		splitterY = splitter.transform.position.y;

	}

	// Update is called once per frame
	void Update ()
	{


	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Destroy (splitter);
		for (int i = 0; i < 3; i++)
		{
			Instantiate (splitterShard, new Vector3(splitterX, splitterY, 0), Quaternion.identity);
		}
	}

}
