using UnityEngine;
using System.Collections;

//This script controls the camera in the dialogue exchange scenes.
//It has an offset field to allow the camera position the ship in the correct spot
public class DialogueExchangeCameraController : MonoBehaviour {

	public Transform target;
	public float cameraOffset;

	void Update()
	{
		transform.position = new Vector3 (target.position.x, target.position.y + cameraOffset, -10);
	}

}
