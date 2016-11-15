using UnityEngine;
using System.Collections;

public class TriggerCutScene : MonoBehaviour {

	public Transform target;
	private Collider2D player;
	private string playerName = "PlayerPlaceholder";
	private bool reachedDestination = false;
	public float speed = 10;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!reachedDestination && player != null) {
			player.SendMessage ("movePlayer", speed);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == playerName) {
			Debug.Log ("Touchy touch");
			setReachedDestination (false);
			player = other;
			player.SendMessage ("setPlayerDestination", (Vector2)target.position);


		}
		
	}

	public void setReachedDestination( bool hasReached){
		reachedDestination = hasReached;
		if (player != null && reachedDestination == true) {
			player.SendMessage ("setDisablePlayerControl", false);
		}
	}
}
