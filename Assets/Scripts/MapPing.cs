using UnityEngine;
using System.Collections;

public class MapPing : MonoBehaviour {

    float startScale = 0.1f;
    float endScale = 15.0f;
    Vector3 startScaleVec, endScaleVec;
    Vector3 scaleGrowth = new Vector3(0.1f, 0.1f, 0);
	// Use this for initialization
	void Start ()
    {
        startScaleVec = new Vector3(startScale, startScale, transform.localScale.z);
        endScaleVec = new Vector3(endScale, endScale, transform.localScale.z);
	}

    void OnEnable()
    {
        transform.localScale = startScaleVec;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.localScale += scaleGrowth;
        if(transform.localScale.magnitude > endScaleVec.magnitude)
        {
            transform.localScale = startScaleVec;
        }

    }
}
