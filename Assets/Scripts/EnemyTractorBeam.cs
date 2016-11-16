using UnityEngine;
using System.Collections;
/* EnemyTractorBeam.cs 
 * 
 * A highly modified TractorBeamControl.cs changed to work with Vision.cs and no mouse input.
 * Makes a drone fire a tractor beam towards the player if they are in range. If the beam connects, the player will recieve
 * a short, but intense pull force towards the enemy.
 *
 */
public class EnemyTractorBeam : MonoBehaviour {
    public float pullDelay = 5.0f, pullForce = 15.0f;
	public bool deflection = false;

	private Vision _vision;
	private float _tractorlength;
	private RaycastHit2D _tractorStick;
    private bool _hitPlayer, _canPull = true;
	private float MAX_TRACTOR_LENGTH = 20.0f;
	private const float PULL_SPEED = 1.0f;
	private LineRenderer _tractorLine, _tractorJunk;
	private Vector3 _playerPos, _junkPos;
	private GameObject _player;
    private Rigidbody2D _playerRgdbdy, _junkRdgdbdy;
	private CircleCollider2D _collider;
	private Vector3[] directions = {new Vector3(1, 0 ,0), new Vector3(-1,0,0), new Vector3(0,1,0), new Vector3(0,-1,0)};

	// Use this for initialization
	void Start () 
	{
        MAX_TRACTOR_LENGTH = GetComponent<Vision>().viewDist;
		_collider = GetComponent<CircleCollider2D>();
		if(deflection)
		{
			_collider.enabled = true;
		}
		else
		{
			_collider.enabled = false;
		}
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRgdbdy = _player.GetComponent<Rigidbody2D>();
        _vision = GetComponent<Vision>();
		_tractorLine = GetComponent<LineRenderer>();
        _tractorLine.SetPosition(0, transform.position);
    }
	
	// Update is called once per frame
	void Update () 
	{
		//can see the player
		if(_vision.sight)
		{
			_playerPos = _vision.sightPos;
            //cast a 0 length ray that grows (quickly) until it either hits the player or the max length
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _playerPos - transform.position, _tractorlength);
            if (_tractorlength < MAX_TRACTOR_LENGTH && !_hitPlayer)
            {
                _tractorlength++;
            }
            //we hit the player
            if (hit)
            {
                if (hit.collider.gameObject == _player)
                {
                    _hitPlayer = true; //it may look weird, but Raycast2D forces hit identifiers to work this way
                }
            }
			//uses the initial object hit by the beam
			if(_hitPlayer)
			{
                _tractorlength = Vector2.Distance(_playerPos, transform.position);
                //Pull in the player here for a short time with increased force
                if (_canPull)
                {
					Enemy_TractorBeamRender(_playerPos, _tractorlength);
                    _playerRgdbdy.AddForce(transform.right * -pullForce, ForceMode2D.Impulse);
                    StartCoroutine("impulsePullWait");
                }
            }
		}
        else
        {
            _tractorlength = 0;
            _tractorLine.SetPosition(1, transform.position);
            _hitPlayer = false;
        }
    }

	//Renders a line along the tractor beam
	private void Enemy_TractorBeamRender(Vector3 target, float length)
	{
        _tractorLine.enabled = true;
        _tractorLine.SetColors(Color.red, Color.red);
        //unsure if these vector calculations are necessary? It worked fine with using just target.
        Vector2 rayDir = (Vector2)target - (Vector2)transform.position;
        Vector2 endPoint = (Vector2)transform.position + (rayDir.normalized * length);
        _tractorLine.SetPosition(0, endPoint);
	}

    //handles the delay between pulls
    IEnumerator impulsePullWait()
    {
        _canPull = false;
        yield return new WaitForSeconds(0.25f);
        _tractorLine.SetPosition(0, transform.position);
        yield return new WaitForSeconds(pullDelay - 0.25f);
        _canPull = true;
    }

	//for the junker, will fling objects not tagged the player into a random direction
    //as long as the object is within the circle of pushing
	void OnTriggerStay2D(Collider2D col){
		int rand = Random.Range(0, directions.Length);
		if(col.gameObject.tag != "Player")
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, col.transform.position - transform.position, _collider.radius);
			if(hit && _canPull){
				Enemy_TractorBeamRender(hit.collider.gameObject.transform.position, hit.distance);
                col.attachedRigidbody.AddForce(directions[rand] * pullForce, ForceMode2D.Impulse);
				StartCoroutine("impulsePullWait");
			}
		}
	}
}
