using UnityEngine;
using System.Collections;

public class DusterParticle : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnParticleCollision(GameObject test)
    {
        Debug.Log("HITT");
    }
}
