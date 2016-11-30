using UnityEngine;
using System.Collections;

public class GateAudioStart : MonoBehaviour {
	public AudioSource audioSource;
	public float playbackPosition;
	// Use this for initialization
	void Start () {
		audioSource.time = playbackPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
