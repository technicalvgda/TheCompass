using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	private bool isInBarrier = false;
	private Collider2D player;
	private Vector2 playerPos = new Vector2 (0, 0);
	private string playerName = "PlayerPlaceholder";


	void Update()
	{
		//Calls the player function to extend U-Turn time
		if (isInBarrier && player != null) {
			player.SendMessage ("resetUTurnTime");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == playerName) {
			//Sends info about where the player entered
			player = other;
			playerPos = player.transform.position;
			player.SendMessage ("setPlayerExitPos", playerPos);
			isInBarrier = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == playerName) {
			player = other;
			isInBarrier = false;
		}
		
	}
}
