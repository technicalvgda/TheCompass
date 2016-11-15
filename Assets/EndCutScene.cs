using UnityEngine;
using System.Collections;

public class EndCutScene : MonoBehaviour {

	private Collider2D player;
	private string playerName = "PlayerPlaceholder";
	private GameObject parent;

	// Use this for initialization
	void Start () {
		parent = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == playerName) {
			parent.SendMessage("setReachedDestination", true);
		}
	}
}
