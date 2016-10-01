using UnityEngine;
using System.Collections;

public class TriggerByBoundary : MonoBehaviour 
{
	private bool isOutOfBounds = false;
	private Collider2D player;

	void Update()
	{
		if (isOutOfBounds && player != null) {
			player.SendMessage ("resetUTurnTime");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "PlayerPlaceholder") {
			Debug.Log (other.name + " is back in there babieeee");
			player = other;
			Debug.Log (other + ", " + player);
			isOutOfBounds = false;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == "PlayerPlaceholder") {
			Debug.Log (other.name + " is outta there");
			player = other;
			isOutOfBounds = true;
		}
	}


}
