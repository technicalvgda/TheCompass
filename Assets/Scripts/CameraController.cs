using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	
    public Transform target;
    
    private float _dampTime = 0.15f;
    private float _xVelRatio = 2.0f;
    private float _yVelRatio = 2.0f;
    private Vector3 _velocity = Vector3.zero;
 

    void Awake()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
    void FixedUpdate()
    {
    	/*
    	Uses the player's position and velocity to determine where to move the camera
    	*/
        if (target)
        {
            //NOTE: can modify the ratio of inputted x and y velocity to adjust how far the camera goes ahead of player
            Vector3 aheadPoint = target.position + new Vector3(target.GetComponent<Rigidbody2D>().velocity.x/_xVelRatio, target.GetComponent<Rigidbody2D>().velocity.y/_yVelRatio, 0);
    
            Vector3 point = Camera.main.WorldToViewportPoint(aheadPoint);
            
            Vector3 delta = aheadPoint - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            
            Vector3 destination = transform.position + delta;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime);
        }
    }
}
