using UnityEngine;
using System.Collections;

public class DropSlowIon : MonoBehaviour
{
    public GameObject _ion;
    //time until a spawned construct disappears
    private float decayTime = 20; //< must be less than spawn time
    //time between construct spawns ()
    private float spawnTime = 3f;

    //Object pool for spawned constructs
    GameObject[] ionPool;
    
	// Use this for initialization
	void Start ()
    {
        //instantiate new transforms to pool
        ionPool = new GameObject[10];
        for (int i = 0; i < ionPool.Length; i++)
        {
            //instantiates 10 ions as children of enemy
            ionPool[i] = Instantiate(_ion, Vector3.zero, Quaternion.identity) as GameObject;
            ionPool[i].transform.localScale = new Vector3(10.0f, 10.0f, 1.0f);
            ionPool[i].SetActive(false);
        }
        StartCoroutine(DropAndWait(spawnTime));
    }
	

    private IEnumerator DropAndWait(float waitTime)
    {
        //GameObject ionConstruct = Instantiate(_ion, new Vector3(gameObject.transform.position.x, transform.position.y, 0), transform.rotation, transform) as GameObject;
        StartCoroutine(SpawnUnusedIonInPool(decayTime));
        yield return new WaitForSeconds(waitTime);
        //print("WaitAndPrint " + Time.time);
        StartCoroutine(DropAndWait(waitTime));
        yield return null;

    }

    private IEnumerator SpawnUnusedIonInPool(float decayTime)
    {
        // search list for unused construct:
        int poolNum = -1;
        for (int i = 0; i < 20; i++)
        {
            if (ionPool[i].activeSelf == false)
            {
                poolNum = i;
                break;
            }
        }
        // found one (if we couldn't find unused ion, no spawn):
        if (poolNum >= 0)
        {
            // activate this orc, move it to spawn point:
            ionPool[poolNum].SetActive(true);
            ionPool[poolNum].transform.position = new Vector3(gameObject.transform.position.x, transform.position.y, 0);
        }
        //disable ion construct after set time
        yield return new WaitForSeconds(decayTime);
        ionPool[poolNum].SetActive(false);
        yield return null;
    }

  


}
