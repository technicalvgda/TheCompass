using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	private bool isInBarrier = false;
	private Collider2D player;
	private Vector2 playerPos = new Vector2 (0, 0);

	void Update()
	{
		if (isInBarrier && player != null) {
			player.SendMessage ("resetUTurnTime");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "PlayerPlaceholder") {
			//Debug.Log (other.name + " is outta there");
			player = other;
			playerPos = player.transform.position;
			player.SendMessage ("setPlayerExitPos", playerPos);
			Debug.Log (playerPos);
			isInBarrier = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == "PlayerPlaceholder") {
			//Debug.Log (other.name + " is back in there babieeee");
			player = other;
			isInBarrier = false;
		}
		
	}
}
