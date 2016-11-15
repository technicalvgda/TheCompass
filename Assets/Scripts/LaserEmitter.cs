using UnityEngine;
using System.Collections;
/* LaserEmitter.cs
 * 
 * This script should draw an indefinite line. The line with will have damage falloff for enemies and the environment, but not the player.
 * When the laser hits an asteroid it will split the asteroid into multiple pieces.
 * 
 */
public class LaserEmitter : MonoBehaviour
{
    public LineRenderer line;
    public float maxFalloffDist;    //don't set this past 10
    public float rayDamage;
    public Transform laserEndPt;
    private GameObject laserSparks;
    public float bounceIntensity;  //how much the player gets bounced back upon collision with the laser
    public float timeLimit = 0.5f;  //amount of time the player is paralyzed for

    private GameObject _player;
    private RaycastHit2D _hit;
    private Vector3 _startVec, _startVecFwd;
    private Player _playerscript;
    private float _calcDamage;
    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
        //line.GetComponent<Renderer>().material = _lineMat;
        line.SetVertexCount(2);
        line.SetWidth(1.0f, 1.0f);

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerscript = _player.GetComponent<Player>();
        line.enabled = true;
        bounceIntensity = 15.0f;
        laserSparks = transform.Find("Sparks").gameObject;
        laserSparks.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _startVec = transform.position; //ray origins
        _startVecFwd = laserEndPt.position;//transform.up; //ray direction

        line.SetPosition(0, _startVec);

        Debug.DrawLine(_startVec, _startVecFwd, Color.black, 1.0f);
        _hit = Physics2D.Linecast(_startVec, _startVecFwd);
        if (_hit)
        {
            //laser hits something, so the endpoint is the collision point
            line.SetPosition(1, _hit.point);
            laserSparks.SetActive(true);
            laserSparks.transform.position = _hit.point;
            //TODO: Make the damage have a cooldown using the coroutine fire methods.
            if (_hit.distance < maxFalloffDist)
            {
                /**Linear Fall off scaling
                * 1 Unit = 10% damage reduction
                **/
                _calcDamage = rayDamage * (100 - (100 * (0.1f * _hit.distance)));
            }
            else
            {
                _calcDamage = 0;
            }
            
            //Hitting the player
            //Debug.Log(_hit.point);
            if (_hit.collider.gameObject == _player)
            {
                _playerscript.takeDamage(_calcDamage);

                //applies force to the player in the opposite direction with which it is hit by the laser
                Vector2 bounceBack = _playerscript.transform.position - transform.position;
                bounceBack = new Vector2((bounceBack.x > 0) ? 1 : -1, (bounceBack.y > 0) ? 1 : -1);
                _playerscript.GetComponent<Rigidbody2D>().AddForce(bounceBack * bounceIntensity, ForceMode2D.Impulse);

                //deactivates the playerscript in order to simulate paralysis
                _playerscript.enabled = false;
            }

            //hitting an enemy or the asteroid, change the tags when necessary
            if (_hit.collider.gameObject.tag == "Enemy" || _hit.collider.gameObject.tag == "SplitAsteroid")
            {
                //The enemy should be taking damage scaled to distance. It should also be hitting the asteroid.
                //I will update this when the asteroids and enemies get written.
                
            }
        }
        else
        {
            //laser hits nothing, so it will continue to its endpoint
            line.SetPosition(1, laserEndPt.position);
            if(laserSparks != null)
            {
                laserSparks.SetActive(false);
            }
        }

        //conditions to reactivate player script after the paralysis time has elapsed
        if (timeLimit > 0 && !_playerscript.enabled)
        {
            timeLimit -= Time.deltaTime;
        }
        else
        {
            _playerscript.enabled = true;
            timeLimit = 0.5f;
        }
    }
    void OnDrawGizmos()
    {
        if (laserEndPt != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, laserEndPt.position);
        }
    }
}

 
