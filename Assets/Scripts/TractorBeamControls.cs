﻿using UnityEngine;
using System.Collections;

public class TractorBeamControls : MonoBehaviour
{

    private Touch _touch;
    private Vector2 _touchPos, _worldPos;
    private Vector2 originalTouch;
    public VirtualJoystickTether joystick;

    //tractor beam variables
    MoveableObject objectScript;
    private RaycastHit2D _tractorStick; //< the object that is stuck to tractor beam
    private Vector3 _MouseClickedPoint;
    private bool _hitDebris = false;
    private int _tractorlength = 0;//<the current length of the tractor beam
    private const float MAX_TRACTOR_LENGTH = 20;
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
    private const float PULL_SPEED = 1;
    
#elif UNITY_IOS || UNITY_ANDROID
    private const float PULL_SPEED = 3;
#endif

    //PLAYER COMPONENTS
    private LineRenderer _tractorLine;


    // Use this for initialization
    void Start()
    {
        _tractorLine = GetComponent<LineRenderer>();

        if (joystick == null && GameObject.Find("VirtualJoystickTether")!= null)
        {
            joystick = GameObject.Find("VirtualJoystickTether").GetComponentInChildren<VirtualJoystickTether>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //update tractor beam line renderer
        TractorBeamRender();
        


#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
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
           
            //get mouse click in world coordinates
            _MouseClickedPoint = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            
            //sends ray out to check if it hits an object when it does it records which object it hit
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _MouseClickedPoint - transform.position, _tractorlength);
            if (_tractorlength < MAX_TRACTOR_LENGTH && !_hitDebris)
            {

                //Debug.DrawLine(transform.position, _MouseClickedPoint, Color.red);
                _tractorlength++;
            }
            
            //holds first object it hits and keeps it from hitting another object
            if (!_hitDebris && hit)
            {
                _tractorStick = hit;
                if (objectScript = _tractorStick.collider.GetComponent<MoveableObject>())
                {
                    
                    _hitDebris = true;
                }
            }

            //uses the initial object that was hit by the beam
            if (_hitDebris)
            {
                //create a script for the held object
                
                
                //if the object has a MoveableObject script, store it and handle physics
               
                    //draw a line to show tractor beam connection
                    Debug.DrawLine(transform.position, _tractorStick.transform.position);

                //move debris in direction of mouse with force (pullspeed/objectsize)
                //_tractorStick.rigidbody.AddForce(((_MouseClickedPoint - _tractorStick.rigidbody.transform.position).normalized) * PULL_SPEED / objectScript.objectSize );

                _tractorStick.rigidbody.velocity = (Vector2.Lerp(_MouseClickedPoint - _tractorStick.transform.position, _tractorStick.transform.position, Time.deltaTime) * PULL_SPEED / objectScript.objectSize) + GetComponent<Rigidbody2D>().velocity;

                //if the distance between the _mouse clicked point and the object is <1 the object will stop moving
                if (Vector2.Distance(_MouseClickedPoint, _tractorStick.transform.position) < 1)
                {
                        _tractorStick.rigidbody.velocity = Vector2.zero;
                }
                

            }
        }
        //if a controller is present 
        else if (Input.GetAxis("RightJoystickVertical") != 0 || Input.GetAxis("RightJoystickHorizontal") != 0)//Input.GetButton("RightBumper"))
        {

                Debug.DrawRay(transform.position, new Vector2(Input.GetAxis("RightJoystickHorizontal"), Input.GetAxis("RightJoystickVertical")) * 10, Color.blue);
                Debug.Log(new Vector2(Input.GetAxis("RightJoystickHorizontal"), Input.GetAxis("RightJoystickVertical")));
                //sends ray out to check if it hits an object when it does it records which object it hit
                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(Input.GetAxis("RightJoystickHorizontal"), Input.GetAxis("RightJoystickVertical")), _tractorlength);
                if (_tractorlength < MAX_TRACTOR_LENGTH && !_hitDebris)
                {
                    //Debug.DrawLine(transform.position, _MouseClickedPoint, Color.red);
                    _tractorlength++;
                }

                //holds first object it hits and keeps it from hitting another object
                if (!_hitDebris && hit)
                {
                    _tractorStick = hit;
                    if (objectScript = _tractorStick.collider.GetComponent<MoveableObject>())
                    {

                        _hitDebris = true;
                    }
                }

                //uses the initial object that was hit by the beam
                if (_hitDebris)
                {
                    //create a script for the held object


                    //if the object has a MoveableObject script, store it and handle physics

                    //draw a line to show tractor beam connection
                    Debug.DrawLine(transform.position, _tractorStick.transform.position);

                    //move debris in direction of mouse with force (pullspeed/objectsize)
                    //_tractorStick.rigidbody.AddForce(((_MouseClickedPoint - _tractorStick.rigidbody.transform.position).normalized) * PULL_SPEED / objectScript.objectSize );
                    Vector2 stick = new Vector2(Input.GetAxis("RightJoystickHorizontal"), Input.GetAxis("RightJoystickVertical")) * 10;
                    _tractorStick.rigidbody.velocity = (stick * PULL_SPEED / objectScript.objectSize) + GetComponent<Rigidbody2D>().velocity;

                    //if the distance between the _mouse clicked point and the object is <1 the object will stop moving
                }
           }
            //when the mouse button is released or joystick returns to center resets all of the necessary variables
            else 
            {

                //Debug.Log("Click up");
                objectScript = null;
                _hitDebris = false;
                _tractorlength = 0;
            }
        
        


        
#elif UNITY_IOS || UNITY_ANDROID


        

        if (joystick.touchPhase() == TouchPhase.Moved)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, joystick.inputValue(), _tractorlength);
            if (_tractorlength < MAX_TRACTOR_LENGTH && !_hitDebris)
            {

                //Debug.DrawRay(transform.position, _worldPos - originalTouch, Color.red);
                _tractorlength++;
            }

            if (!_hitDebris && hit)
            {
                _tractorStick = hit;
                if (objectScript = _tractorStick.collider.GetComponent<MoveableObject>())
                {
                
                _hitDebris = true;
                }
            }

            if (_hitDebris)
            {
                 //draw a line to show tractor beam connection
                 //Debug.DrawLine(transform.position, _tractorStick.transform.position);

                 //move debris in the direction that the joystick is going
                 _tractorStick.rigidbody.velocity = (joystick.inputValue() * PULL_SPEED / objectScript.objectSize)+ GetComponent<Rigidbody2D>().velocity;

            }
        }
        else if (joystick.touchPhase() == TouchPhase.Ended)
        {

            objectScript = null;
            _hitDebris = false;
            _tractorlength = 0;
        }




#endif

    }

    private void TractorBeamRender()
    {

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        //if the tractor beam is active
        if (Input.GetMouseButton(0))
        {
            //enable the beam
            _tractorLine.enabled = true;

            //get mouse click in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            //make sure starting position of tractor beam is at the ship
            _tractorLine.SetPosition(0, transform.position);

            //if the tractor beam is connected
            if (_hitDebris == true)
            {
                //set the color of the beam to white
                _tractorLine.SetColors(Color.white, Color.white);
                //draw a line to show tractor beam connection
                _tractorLine.SetPosition(1, _tractorStick.transform.position);

            }
            else
            {
                //find direction vector from ship to mouse
                Vector2 mouseDir = mousePos - (Vector2)transform.position;
                //make a variable for the end position
                Vector2 endPoint;
                //if the mouse if further away than the max length of the beam
                if (mouseDir.magnitude > MAX_TRACTOR_LENGTH)
                {
                    
                    //get a position in the direction of the mouse 
                    endPoint = (Vector2)transform.position + (mouseDir.normalized * _tractorlength);
                    
                }
                else
                {
                   
                    //get the mouse position
                    endPoint = mousePos;
                }

                //set the end of the beam to be where the endpoint variable is
                _tractorLine.SetPosition(1, endPoint);
                //set the color of the beam to blue
                _tractorLine.SetColors(Color.blue, Color.blue);
            }

        }
        else if (Input.GetAxis("RightJoystickVertical") != 0 || Input.GetAxis("RightJoystickHorizontal") != 0)
        {
            //enable the beam
            _tractorLine.enabled = true;

            //get mouse click in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            //make sure starting position of tractor beam is at the ship
            _tractorLine.SetPosition(0, transform.position);

            //if the tractor beam is connected
            if (_hitDebris == true)
            {
                //set the color of the beam to white
                _tractorLine.SetColors(Color.white, Color.white);
                //draw a line to show tractor beam connection
                _tractorLine.SetPosition(1, _tractorStick.transform.position);

            }
            else
            {
                //make a variable for the end position
                Vector2 endPoint;

                //get a position in the direction of the stick
                endPoint = (Vector2)transform.position + (new Vector2(Input.GetAxis("RightJoystickHorizontal"), Input.GetAxis("RightJoystickVertical")).normalized * _tractorlength);
                

                //set the end of the beam to be where the endpoint variable is
                _tractorLine.SetPosition(1, endPoint);
                //set the color of the beam to blue
                _tractorLine.SetColors(Color.blue, Color.blue);
            }
        }
        else
        {
            _tractorLine.SetPosition(1, transform.position);
            _tractorLine.enabled = false;
        }
#elif UNITY_IOS || UNITY_ANDROID
        if(joystick.touchPhase() == TouchPhase.Began)
        {
            //enable the beam
            _tractorLine.enabled = true;
        }

        if (joystick.touchPhase() == TouchPhase.Moved)
        {
            //make sure starting position of tractor beam is at the ship
            _tractorLine.SetPosition(0, transform.position);

            //if the tractor beam is connected
            if (_hitDebris == true)
            {
                //set the color of the beam to white
                _tractorLine.SetColors(Color.white, Color.white);
                //draw a line to show tractor beam connection
                _tractorLine.SetPosition(1, _tractorStick.transform.position);
            }
            else
            {
                //find direction that the joystick is going 
                Vector2 mouseDir = joystick.inputValue().normalized;
                //make a variable for the end position
                Vector2 endPoint = (Vector2)transform.position + (mouseDir.normalized * _tractorlength);
                

                //set the end of the beam to be where the endpoint variable is
                _tractorLine.SetPosition(1, endPoint);
                //set the color of the beam to blue
                _tractorLine.SetColors(Color.blue, Color.blue);
            }
        }
        if(joystick.touchPhase() == TouchPhase.Ended)
        {
            _tractorLine.SetPosition(1, transform.position);
            _tractorLine.enabled = false;
        }

        
#endif


    }

    public float getObjectSize()
    {
        if (objectScript == null)
        {
            return 0;
        }
        else
        {
            return objectScript.objectSize;
        }

    }
}
