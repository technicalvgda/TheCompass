using UnityEngine;
using System.Collections;

public class FlameTrailHandler : MonoBehaviour {

    bool startDestroy = false;
	
	// Update is called once per frame
	void Update ()
    {
	    if(transform.parent == null && startDestroy == false)
        {
            startDestroy = true;
            StartCoroutine(DestroyTrail());
        }
	}

    public IEnumerator DestroyTrail()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        yield return null;
    }
}
