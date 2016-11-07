using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

	LineRenderer myLine;
	Transform target;

	// Use this for initialization
	void Start () 
	{
		target = transform.GetChild(0);
		myLine = GetComponent<LineRenderer>();
		myLine.SetVertexCount(2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		myLine.SetPosition (0, transform.position);
		myLine.SetPosition (1, target.position);
		float distance = Vector3.Distance (transform.position, target.position);
		myLine.material.mainTextureScale = new Vector2(distance * 2, 1);

	}
}
