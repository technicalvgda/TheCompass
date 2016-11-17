using UnityEngine;
using System.Collections;

public class DetectableObject : MonoBehaviour
{
    //How fast does the object need to be to actually be detectable
    public float SpeedThreshhold;
    //How close the object needs to be to actually be detectable
    public float DetectionRange = 100;
    //Prefab for graphic displaying the direction the object is in.
    public GameObject arrowPrefab;
	private PlayerWithinRangeOfEnemy _playerWithinRangeScript;
    //Cached components to reduce slow GetComponent<T>() calls. 
    Renderer _renderer;
    Rigidbody2D _rb2d;
    GameObject _arrow;

    /*
    Cache all needed components.
    */
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _rb2d = GetComponent<Rigidbody2D>();
        _arrow = (GameObject)Instantiate(arrowPrefab);
        _arrow.SetActive(false);
		_playerWithinRangeScript = GameObject.FindGameObjectWithTag ("CommentaryObject").GetComponentInChildren<PlayerWithinRangeOfEnemy> ();
    }
    /*
    Check if this object meets all conditions. If yes, show arrow and update its position.
    Else, hide arrow.

    ***Technically, this checks the distance between this object and the center of the screen,
    ***not the player's actual position.
    */
    void Update()
    {
        //Gets the center of the screen in world position
        Vector3 playerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 objectPos = transform.position;

        //Gets how far the object is away from center of the screen.
        Vector3 heading = objectPos - playerPos;
        float distance = heading.magnitude;

        //Checks if this object:
        //1) Is close enough to be detected
        //2) Is actually outside the screen/not rendered by the camera
        //3) Is moving faster than the speed threshhold
        if (distance <= DetectionRange && !_renderer.isVisible && _rb2d.velocity.magnitude >= SpeedThreshhold)
        {
			if (transform.tag == "Enemy") 
			{
				if (_playerWithinRangeScript != null) 
				{
					_playerWithinRangeScript.activateCommentary ();
					_playerWithinRangeScript = null;
				}
			}
            _arrow.SetActive(true);
            UpdateArrow();
        }
        else
        {
            _arrow.SetActive(false);
        }
    }
    /*
    Update arrow's position and rotation.
    */
    public void UpdateArrow()
    {
        //Grabs this object's position and clamps it to the edge of the screen.

        Vector3 pos = transform.position;
        pos = Camera.main.WorldToViewportPoint(pos);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);

        _arrow.transform.position = Camera.main.ViewportToWorldPoint(pos);

        pos = _arrow.transform.position;
        float Angle = Mathf.Atan2(transform.position.y - pos.y, transform.position.x - pos.x) * 180 / Mathf.PI;
        _arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle));
    }
}