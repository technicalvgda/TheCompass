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

    //Shield Audio Source
    AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        
        //collision = true;
        waitTime = initialWaitTime;
        
        anim = GetComponent<Animator>();

        audioSrc = GetComponent<AudioSource>();
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

    public void ActivateShield(bool playerDamaged)
    {
        waitTime = initialWaitTime;

        if (anim.GetBool("Colliding") == false)
        {
            anim.SetBool("Damage", playerDamaged);
            anim.SetBool("Colliding", true);
            

            // Activate Sound on collision
            if (audioSrc != null)
            {
                audioSrc.Play();
            }
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
