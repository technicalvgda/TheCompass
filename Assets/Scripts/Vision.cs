using UnityEngine;
using System.Collections;
/* Vision.cs
 * 
 * Creates a cone of vision from the scripted object to the player using raycasts with a give angle and distance.
 * If the player is seen, rotates to face them.
 * - Joel Lee
 */
public class Vision : MonoBehaviour {
    public float viewAngle;				//The angle of our cone. Should be half the intended since we calculate the angle of between the player and the foward.
    public float viewDist = 50.0f;		//How far our raycast will be drwn

    private RaycastHit _hit;
    private Vector3 _playerLoc, _rayDirection, _startVec, _startVecFwd;
	private GameObject _player;

	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        _startVec = transform.position;
		_startVecFwd = transform.right;    //the raycast direction. right is in the direction of the X axis.

		_playerLoc = _player.transform.position;
        _rayDirection = _playerLoc - _startVec;

		Debug.DrawRay(_startVec, _startVecFwd, Color.black, 1.0f); //Black line to show our ""forward"" (which is actually the right since we are in 2d)
		Debug.DrawRay(_startVec, _rayDirection, Color.red, 5.0f); 

        //raycast to the player, checking our angle with the first condition and the distance/hit with the second (Raycast returns a boolean)
		//please use the 3D colliders over the 2D. it breaks raycasts for some unknown reason.
		if ((Vector3.Angle(_rayDirection, _startVecFwd)) < viewAngle && Physics.Raycast(_startVec, _rayDirection, out _hit, viewDist))
        {
			//Debug.Log(hit.collider.name);
			Debug.DrawRay(_startVec, _rayDirection, Color.green, 5.0f);
            if (_hit.collider.gameObject == _player)
			{
				//because our forward is on the unused z axis, we can't use lookAt()
				//so we have to get a (distance) vector by subtracting the two points and rotate to this vector 
				transform.right = _player.transform.position - transform.position;
            	Debug.DrawRay(_startVec, _rayDirection, Color.blue, 5.0f);
                Debug.Log("I see the player.");
			}
            else
            {
			    Debug.Log("I don't see the player.");
            }
        }
	}
}
