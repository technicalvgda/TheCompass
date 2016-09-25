using UnityEngine;
using System.Collections;

public class TractorBeamControls : MonoBehaviour
{

    private Touch _touch;
    private Vector2 _touchPos, _worldPos;
    private Vector2 originalTouch;

    //tractor beam variables
    private RaycastHit2D _tractorStick; //< the object that is stuck to tractor beam
    private Vector3 _MouseClickedPoint;
    private bool _hitDebris = false;
    private int _tractorlength = 0;//<the current length of the tractor beam

    private const float PULL_SPEED = 1;
    private const float MAX_TRACTOR_LENGTH = 20;

    //PLAYER COMPONENTS
    private LineRenderer _tractorLine;


    // Use this for initialization
    void Start()
    {
        _tractorLine = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //update tractor beam line renderer
        TractorBeamRender();


#if UNITY_STANDALONE || UNITY_WEBPLAYER
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
                _hitDebris = true;
            }

            //uses the initial object that was hit by the beam
            if (_hitDebris)
            {
                //create a script for the held object
                MoveableObject objectScript;
                //if the object has a MoveableObject script, store it and handle physics
                if (objectScript = _tractorStick.collider.GetComponent<MoveableObject>())
                {
                    //draw a line to show tractor beam connection
                    Debug.DrawLine(transform.position, _tractorStick.transform.position);

                    //move debris in direction of mouse with force (pullspeed/objectsize)
                    //_tractorStick.rigidbody.AddForce(((_MouseClickedPoint - _tractorStick.rigidbody.transform.position).normalized) * PULL_SPEED / objectScript.objectSize );

                    _tractorStick.rigidbody.velocity = Vector2.Lerp(_MouseClickedPoint - _tractorStick.transform.position, _tractorStick.transform.position, Time.deltaTime) * PULL_SPEED / objectScript.objectSize;

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
#elif UNITY_IOS || UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            //Debug.Log("touch1");
            for (int i = 0; i< Input.touchCount; i++)
            {
                
                _touch = Input.GetTouch(i);
                if(_touch.position.x > Screen.width / 2F)
                {
                    //Debug.Log("touch2");
                    _touchPos = _touch.position;
                    _worldPos = Camera.main.ScreenToWorldPoint(new Vector2(_touchPos.x, _touchPos.y));
                    
                    if(_touch.phase == TouchPhase.Began)
                    {
                        //Debug.Log("touch3");
                        originalTouch = _worldPos;
                    }

                    if(_touch.phase == TouchPhase.Moved)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, _worldPos - originalTouch, _tractorlength);
                        if (_tractorlength < MAX_TRACTOR_LENGTH && !_hitDebris)
                        {

                            //Debug.DrawRay(transform.position, _worldPos - originalTouch, Color.red);
                            _tractorlength++;
                        }

                        if (!_hitDebris && hit)
                        {
                            _tractorStick = hit;
                            _hitDebris = true;
                        }

                        if (_hitDebris)
                        {
                            //create a script for the held object
                            MoveableObject objectScript;
                            //if the object has a MoveableObject script, store it and handle physics
                            if (objectScript = _tractorStick.collider.GetComponent<MoveableObject>())
                            {
                                //draw a line to show tractor beam connection
                                Debug.DrawLine(transform.position, _tractorStick.transform.position);

                                //move debris in direction of mouse with force (pullspeed/objectsize)
                                //_tractorStick.rigidbody.velocity = Vector2.Lerp(_MouseClickedPoint - _tractorStick.transform.position, _tractorStick.transform.position, PULL_SPEED / objectScript.objectSize * Time.deltaTime);
                                _tractorStick.rigidbody.velocity = new Vector2(_worldPos.x - originalTouch.x, _worldPos.y- originalTouch.y)* PULL_SPEED / objectScript.objectSize;
                                //if the distance between the _mouse clicked point and the object is <1 the object will stop moving
                                /*if (Vector2.Distance(_MouseClickedPoint, _tractorStick.transform.position) < 1)
                                {
                                    _tractorStick.rigidbody.velocity = Vector2.zero;
                                }*/

                            }

                        }
                    }
                    else if (_touch.phase == TouchPhase.Ended)
                    {
                        
                        _hitDebris = false;
                        _tractorlength = 0;
                    }
                    
                }
            }
        }

#endif

    }

    private void TractorBeamRender()
    {

#if UNITY_STANDALONE || UNITY_WEBPLAYER
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
                    endPoint = (Vector2)transform.position + (mouseDir.normalized * MAX_TRACTOR_LENGTH);
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
        else
        {
            _tractorLine.SetPosition(1, transform.position);
            _tractorLine.enabled = false;
        }
#elif UNITY_IOS || UNITY_ANDROID

#endif


    }
}
