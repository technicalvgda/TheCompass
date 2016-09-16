using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
    public float viewAngle;
    public float viewDist = 50.0f;

    private RaycastHit hit;
    private Vector3 playerLoc;
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 startVec = transform.position;
        Vector3 startVecFwd = transform.forward;    //the raycast direction

        //construct a new vector2, since position returns a vector3
        playerLoc = new Vector2(player.transform.position.x , player.transform.position.y);

        Vector3 rayDirection = playerLoc - startVec;
        //Debug.DrawRay(startVec, rayDirection, Color.red, 5.0f);
        //raycast to the player, checking our angle with the first condition and the distance/hit with the second
        if ((Vector3.Angle(rayDirection, startVecFwd)) < viewAngle)
        {
            Debug.Log("A");
            if (Physics.Raycast(startVec, rayDirection, out hit, viewDist))
            {
                Debug.Log("B");
                //Debug.DrawRay(startVec, rayDirection, Color.green, 5.0f);
                if (hit.collider.gameObject == player.gameObject)
                {
                    Debug.DrawRay(startVec, rayDirection, Color.blue, 5.0f);
                    Debug.Log("I see the player.");
                }
                else
                {
                    Debug.Log("I don't see the player.");
                }
            }
        }
	}
}
