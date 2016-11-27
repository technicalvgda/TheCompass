using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    Transform player;
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(Player.playerPos.x, Player.playerPos.y, transform.position.z);
	}

}
