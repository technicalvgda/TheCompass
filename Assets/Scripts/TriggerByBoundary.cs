using UnityEngine;
using System.Collections;

public class TriggerByBoundary : MonoBehaviour 
{
	private bool isOutOfBounds = false;
	private Collider2D player;
	private Vector2 playerPos = new Vector2 (0, 0);
	private string playerName = "PlayerPlaceholder";

	void Update()
	{
		//Calls the player function to extend U-Turn time
		if (isOutOfBounds && player != null) {
			player.SendMessage ("resetUTurnTime");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == playerName) {
			player = other;
			isOutOfBounds = false;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == playerName) {
			player = other;
			playerPos = player.transform.position;
			//Sends info about where the player entered
			player.SendMessage ("setPlayerExitPos", playerPos);
			Debug.Log (playerPos);
			isOutOfBounds = true;
		}
	}


}
