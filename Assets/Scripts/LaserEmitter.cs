using UnityEngine;
using System.Collections;
/* LaserEmitter.cs
 * 
 * This script should draw an indefinite line. The line with will have damage falloff for enemies and the environment, but not the player (instant death).
 * When the laser hits an asteroid it will split the asteroid into multiple pieces.
 * 
 */
public class LaserEmitter : MonoBehaviour
{
    public LineRenderer line;
    public float maxFalloffDist;    //don't set this past 10
    public float rayDamage;
    public Transform laserEndPt;

    private GameObject _player;
    private RaycastHit2D _hit;
    private Vector3 _startVec, _startVecFwd;
    private Player _playerscript;
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
    }

    // Update is called once per frame
    void Update()
    {
        _startVec = transform.position; //ray origins
        _startVecFwd = transform.up; //ray direction

        line.SetPosition(0, _startVec);

        Debug.DrawRay(_startVec, _startVecFwd, Color.black, 1.0f);
        _hit = Physics2D.Raycast(_startVec, _startVecFwd);
        if (_hit)
        {
            //laser hits something, so the endpoint is the collision point
            line.SetPosition(1, _hit.point);
            //TODO: Make the damage have a cooldown using the coroutine fire methods.

            //Hitting the player
            Debug.Log(_hit.point);
            if (_hit.collider.gameObject == _player)
            {
                _playerscript.takeDamage(10000.0f); //the laser hit is lethal
            }
            //hitting an enemy or the asteroid, change the tags when necessary
            if (_hit.collider.gameObject.tag == "Enemy" || _hit.collider.gameObject.tag == "SplitAsteroid")
            {
                //The enemy should be taking damage scaled to distance. It should also be hitting the asteroid.
                //I will update this when the asteroids and enemies get written.
                if (_hit.distance < maxFalloffDist)
                {
                    /**Linear Fall off scaling
                    * 1 Unit = 10% damage reduction
                    **/
                    rayDamage = rayDamage * (100 - (100 * (0.1f * _hit.distance)));
                }
            }
        }
        else
        {
            //laser hits nothing, so it will continue to its endpoint
            line.SetPosition(1, laserEndPt.position);
        }
    }
}

 
