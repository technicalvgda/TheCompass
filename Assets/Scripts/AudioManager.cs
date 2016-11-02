using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public GameObject[] gameAudio;
    public float sliderVolumeLevel;
    public Slider slider;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void volumeControl()
    {
        sliderVolumeLevel = GetComponent<Slider>().value;

        for (int i =0; i < gameAudio.Length; i++)
        {
            gameAudio[i].GetComponent<AudioSource>().volume = sliderVolumeLevel;
        }
    }

}
