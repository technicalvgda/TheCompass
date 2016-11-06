using UnityEngine;
using System.Collections;

public class PlayerRangeParticle : MonoBehaviour
{

    //the child particle system object
    GameObject child;
    Camera mainCam;

    float camVertExtent;
    float camHorizExtent;

    float colliderBounds;

    float vertTrig;
    float horizTrig;

    // Use this for initialization
    void Start()
    {
        child = transform.GetChild(0).gameObject;

        mainCam = Camera.main.GetComponent<Camera>();
        //get camera bounds
        //length from center to top/bottom edge
        camVertExtent = mainCam.orthographicSize;
        //length from center to left/right edge 
        camHorizExtent = camVertExtent * Screen.width / Screen.height;
        //get radius of this object
        colliderBounds = GetComponentInChildren<Collider2D>().bounds.size.magnitude / 2;


        vertTrig = camVertExtent + colliderBounds;
        horizTrig = camHorizExtent + colliderBounds;

    }

    // Update is called once per frame
    void Update()
    {
        //if this object has a child particle object
        if (child != null)
        {

            //if the player is inside render range
            if (mainCam.transform.position.x < transform.position.x + horizTrig
                && mainCam.transform.position.x > transform.position.x - horizTrig
                && mainCam.transform.position.y < transform.position.y + vertTrig
                && mainCam.transform.position.y > transform.position.y - vertTrig)
            {

                //enable particles
                child.SetActive(true);
            }
            else
            {

                //disable particles
                child.SetActive(false);
            }
        }
    }
}

