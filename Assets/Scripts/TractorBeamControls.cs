using UnityEngine;
using System.Collections;

public class TractorBeamControls : MonoBehaviour {

    //tractor beam variables
    private RaycastHit2D _tractorStick;
    private Vector3 _MouseClickedPoint;
    private bool _hitDebris;
    private int _tractorlength;
    //private int massModifier;

    // Use this for initialization
    void Start () {
        _tractorlength = 0;
        _hitDebris = false;

    }
	
	// Update is called once per frame
	void Update () {
        /* Tractor Beam Controls Below 
         *    click to send out a tractor beam in that direction 
         *    when it connects with debris it will stick to it and 
         *    the object will move towards the mouse. 
         *    when the object gets to the mouse position it stops
         *    if the mouse button is released before it gets to 
         *    the mouse it will maintain its velocity
         *        
         * */
        //when left mouse button is clicked and held
        if (Input.GetMouseButton(0))
        {

            _MouseClickedPoint = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            //sends ray out to check if it hits an object when it does it records which object it hit
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _MouseClickedPoint, _tractorlength);
            if (_tractorlength < 10 && !_hitDebris)
            {
                Debug.DrawLine(transform.position, _MouseClickedPoint, Color.red);
                _tractorlength++;
            }

            //holds first object it hits and keeps it from hitting another object
            if (!_hitDebris && hit)
            {
                _tractorStick = hit;
                //if the beam hits an immovable object first it does not count it
                if (!_tractorStick.collider.CompareTag("immovable"))
                {
                    _hitDebris = true;
                }

            }

            //uses the initial object that was hit by the beam
            if (_hitDebris)
            {
                //if the beam hit object called debris the object will move towards the mouse
                if (_tractorStick.collider.name == "debris")
                {
                    Debug.DrawLine(transform.position, _tractorStick.transform.position);
                    //keeps count of how far the length between the object and the ship for graphical purposes later if needed
                    _tractorlength = (int)(Vector2.Distance(transform.position, _tractorStick.transform.position));
                    //Debug.Log(_tractorlength);
                    //if the debris is tagged small then the object moves 2x speen
                    if (_tractorStick.collider.CompareTag("small"))
                    {
                        _tractorStick.rigidbody.AddForce(((_MouseClickedPoint - _tractorStick.rigidbody.transform.position).normalized) * 2);

                    }
                    else if (_tractorStick.collider.CompareTag("big"))//if the object is tagged big the object will move at 1/2 speed
                    {
                        _tractorStick.rigidbody.AddForce(((_MouseClickedPoint - _tractorStick.rigidbody.transform.position).normalized) / 2);

                    }
                    //if the distance between the _mouse clicked point and the object is <1 the object will stop moving
                    if (Vector2.Distance(_MouseClickedPoint, _tractorStick.transform.position) < 1)
                    {
                        _tractorStick.rigidbody.velocity = Vector2.zero;
                    }
                }
            }
        }


        //when the mouse button is released resets all of the necessary variables
        if (Input.GetMouseButtonUp(0))
        {

            //Debug.Log("Click up");
            _hitDebris = false;
            _tractorlength = 0;
        }

    }
}
