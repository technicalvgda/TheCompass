using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	
    public Transform target;
    
    private float _dampTime = 0.15f;
    private float _xVelRatio = 2.0f;
    private float _yVelRatio = 2.0f;
    private Vector3 _velocity = Vector3.zero;

	public float screenRatio = 0f;

	private Vector2 _previousVelocity;
	private float _jerkCompensationTime = 0f;


	void Start(){
		_previousVelocity = Vector2.zero;

	}
    void Awake()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
	void FixedUpdate()
	{
    	/*
    	Uses the player's position and velocity to determine where to move the camera
    	*/

		screenRatio = (float)Screen.height / (float)Screen.width;

		if (target)
        {
            //NOTE: can modify the ratio of inputted x and y velocity to adjust how far the camera goes ahead of player
            //aheadpoint = player position + vector of player velocity => position of player after one second
			Vector3 aheadPoint = target.position + new Vector3(target.GetComponent<Rigidbody2D>().velocity.x / (_xVelRatio * screenRatio) , target.GetComponent<Rigidbody2D>().velocity.y / _yVelRatio, 0);


			//converts aheadPoint to camera viewport reference (Shown screen goes from bottom left [0,0] to top right [1,1]
            Vector3 point = Camera.main.WorldToViewportPoint(aheadPoint);

            //delta is the vector from the aheadpoint to the center of the screen
            Vector3 delta = aheadPoint - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            
			//destination is the point from the player to the difference between where the player will be in a second and the center of the screen
            Vector3 destination = transform.position + delta;

			//If the player experiences sudden movement, makes camera movement smoother
			if (playerIsJerked(target.GetComponent<Rigidbody2D>()) || _jerkCompensationTime > 0)
			{
				_jerkCompensationTime -= Time.fixedDeltaTime;
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime * (_jerkCompensationTime * 2 + 1));
			}
			else
			{

				//The camera position transition to the calculated destination
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime);
			}

		}
	}

	public bool playerIsJerked (Rigidbody2D ship) {
		Vector2 deltaVel = ship.velocity - _previousVelocity;
		bool shipIsSpedUp = (ship.velocity.magnitude > _previousVelocity.magnitude);
		if (deltaVel.magnitude >= 30) {
			_jerkCompensationTime = deltaVel.magnitude/30;
			Debug.Log (deltaVel.magnitude);
		} else if (deltaVel.magnitude > 3){
			_jerkCompensationTime = 1.0f;
			Debug.Log (deltaVel.magnitude);
		}
		_previousVelocity = ship.velocity;

		return deltaVel.magnitude > 3;
	}
		
}
