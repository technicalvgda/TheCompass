using UnityEngine;
using System.Collections;

public class TestDiagMovement : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3 (transform.position.x + 1, transform.position.y + 1, 0);
		//transform.position = new Vector3 (transform.position.x +1, 0, 0);
	}
}
