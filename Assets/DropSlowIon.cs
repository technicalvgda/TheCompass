using UnityEngine;
using System.Collections;

public class DropSlowIon : MonoBehaviour {
    public GameObject _ion;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(DropAndWait(5.0f));
	}

    private IEnumerator DropAndWait(float waitTime)
    {
        Rigidbody2D asds = Instantiate(_ion, new Vector3(gameObject.transform.position.x, transform.position.y, 0), transform.rotation) as Rigidbody2D;
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
        
    }
}
