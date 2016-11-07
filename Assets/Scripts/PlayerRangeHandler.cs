using UnityEngine;
using System.Collections;

public class PlayerRangeHandler : MonoBehaviour
{
   
    //The spriteRenderer of this object
    SpriteRenderer SR;
    //The collider of the object
    Collider2D col;
    //The Particle System of the object
    ParticleSystem part;
    //the emission module for the particle system
    ParticleSystem.EmissionModule em;

    //the minimum distance at which the spriteRender will be activated
    float triggerDistance;
    //a preset value if the object is small (roughly the screen size)
    float defaultTriggerDist = 100f;

	// Use this for initialization
	void Start ()
    {
        if (SR = GetComponent<SpriteRenderer>())
        {
            triggerDistance = SR.bounds.size.magnitude + defaultTriggerDist;
        }
        else if(col = GetComponent<Collider2D>())
        {
            triggerDistance = col.bounds.size.magnitude + defaultTriggerDist;
        }
        else
        {
            triggerDistance = defaultTriggerDist;
        }

        if(part = GetComponent<ParticleSystem>())
        {
           em = part.emission;
        }
       
      
       
       

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(SR != null)
        {
            //if the player is outside render range and the spriterenderer is enabled
            if (SR.enabled && Vector3.Distance(transform.position, Player.playerPos) > triggerDistance)
            {
                //disable renderer
                SR.enabled = false;
            }
            else if (!SR.enabled && Vector3.Distance(transform.position, Player.playerPos) <= triggerDistance)
            {
                //enable renderer
                SR.enabled = true;
            }
        }
        /*
        if(part != null)
        {
            //if the player is outside render range and the spriterenderer is enabled
            if (em.enabled && Vector3.Distance(transform.position, Player.playerPos) > triggerDistance)
            {
                //disable emission
                em.enabled = false;
            }
            else if (!em.enabled && Vector3.Distance(transform.position, Player.playerPos) <= triggerDistance)
            {
                //enable emission
                em.enabled= true;
            }
        }
        */
        
	}
}
