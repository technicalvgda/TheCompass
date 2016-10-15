using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    /*
    public GameObject shield, shieldPrefab;
    private bool collision;
    */
    private float initialWaitTime = 2.0f;
    private float waitTime;
    
    Animator anim;

    // Use this for initialization
    void Start()
    {
        
        //collision = true;
        waitTime = initialWaitTime;
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (waitTime <= 0 )
        {
            anim.SetBool("Colliding", false);
        }

        waitTime -= Time.deltaTime;
        
    }

    public void ActivateShield()
    {
        waitTime = initialWaitTime;
        if (anim.GetBool("Colliding") == false)
        {
            anim.SetBool("Colliding", true);
        }
        /*
        
        if (col.gameObject.tag != ("Shield") && collision)
        {
            shield = (GameObject)Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            Debug.Log("Penis");
            collision = false;
        }
        */

    }
}
