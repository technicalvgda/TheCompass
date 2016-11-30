using UnityEngine;
using System.Collections;

public class MapPing : MonoBehaviour {

    float startScale = 0.1f;
    float endScale = 3.0f;
    Vector3 startScaleVec, endScaleVec;
    Vector3 scaleGrowth = new Vector3(0.06f, 0.06f, 0);
    SpriteRenderer rend;

    bool fading = false;

	// Use this for initialization
	void Start ()
    {      
        startScaleVec = new Vector3(startScale, startScale, transform.localScale.z);
        endScaleVec = new Vector3(endScale, endScale, transform.localScale.z);
	}

    void OnEnable()
    {
        rend = GetComponent<SpriteRenderer>();
        transform.localScale = startScaleVec;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(!fading)
        {
            transform.localScale += scaleGrowth;
            if (transform.localScale.magnitude > endScaleVec.magnitude)
            {
                fading = true;
                StartCoroutine(FadeAndRestart());
            }
        }     
    }

    IEnumerator FadeAndRestart()
    {
        Color fadeColor = rend.color;
        while(rend.color.a > 0)
        {
            fadeColor.a -= 0.1f;
            rend.color = fadeColor;
            yield return new WaitForSeconds(0.05f); 
        }      
        transform.localScale = startScaleVec;
        fadeColor.a = 1;
        rend.color = fadeColor;
        fading = false;
        yield return null;
    }
}
