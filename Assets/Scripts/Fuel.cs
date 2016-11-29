using UnityEngine;
using System.Collections;

public class Fuel : MonoBehaviour
{
    float FuelAmount;
    bool collected = false;
    //Animator anim;

    float targetScale = 0.1f;
    float shrinkSpeed = 6.0f;

    void Start()
    {
        //anim = GetComponent<Animator>();
        FuelAmount = Random.Range(30f, 40f);
    }

    public float CollectFuel()
    {
        if(!collected)
        {
            collected = true; //< prevent duplicate collection
            //start animation (animation calls destroy on final frame)
            StartCoroutine(DestroyFuel());
            //return fuel amount
            return FuelAmount;
        }
        //returns 0 if already collected to prevent double pickup
        return 0;
        
    }

    public IEnumerator DestroyFuel()
    {
        while(transform.localScale.magnitude > targetScale)
        {
           transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
        yield return null;
    }
	
}
