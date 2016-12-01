using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
    public GameObject tiles;
    public Vector2 dimension;
    private Vector2 start;
    private Vector3 size;
	// Use this for initialization
	void Start () {
        start = transform.position;
        size = GetComponent<Renderer>().bounds.size;
        for(int i = 0; i<dimension.x; i++)
        {
            for (int j = 0; j< dimension.y; j++)
            {
                Instantiate(tiles, new Vector3(start.x + (i * size.x), start.y + (j * size.y), 0), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
