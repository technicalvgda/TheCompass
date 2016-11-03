using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAudioManager : MonoBehaviour
{
    public GameObject[] gameAudio;
    private float _sliderVolumeLevel,_masterVolumeLevel;
    public Slider slider,masterVolumeSlider;
	private AudioSource _audioSource;
	// Use this for initialization
	void Awake()
	{
		slider.value = PlayerPrefs.GetFloat (slider.name);
		//volumeControl ();
		//initVolume ();
	}
	void Start ()
    {
		//slider.value = PlayerPrefs.GetFloat (slider.name);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
    }

    public void volumeControl()
	{		
		_masterVolumeLevel = masterVolumeSlider.value;
		_sliderVolumeLevel = slider.value;
		// sliderVolumeLevel = GetComponent<Slider>().value;
		//Debug.Log ("Master: " + masterVolumeLevel + "/ " + sliderVolumeLevel);
		/*
		if (slider.name == "MSTRSlider") {
			for (int i = 0; i < gameAudio.Length; i++) {
				if (gameAudio [i].activeSelf == true) {
					if (gameAudio [i].GetComponent<AudioSource> () != null) {
						if (slider.value < gameAudio [i].GetComponent<AudioSource> ().volume)
							gameAudio [i].GetComponent<AudioSource> ().volume = slider.value;				
					}
				}
			}
		} else {*/
		for (int i = 0; i < gameAudio.Length; i++) 
		{
			_audioSource = gameAudio [i].GetComponent<AudioSource> ();
			if (gameAudio [i].activeSelf == true) 
			{
				if( _audioSource != null) 
				{
					if (_masterVolumeLevel > _sliderVolumeLevel) 
					{
						_audioSource.volume = _sliderVolumeLevel;
					} 
					else
						_audioSource.volume = _masterVolumeLevel;
				}
			}
		}
	}
	public void initVolume()
	{
		for (int i = 0; i < gameAudio.Length; i++) 
		{
			_audioSource = gameAudio [i].GetComponent<AudioSource> ();
			//Debug.Log (this.name + ": " + _audioSource.name);
			if (gameAudio [i].activeSelf == false) 
			{
				if( _audioSource != null) 
				{
					if (_masterVolumeLevel > _sliderVolumeLevel) 
					{
						_audioSource.volume = _sliderVolumeLevel;
					} 
					else
						_audioSource.volume = _masterVolumeLevel;
				}
			}
		}
	}
	public void saveValue()
	{
		PlayerPrefs.SetFloat (slider.name, slider.value);
		PlayerPrefs.Save ();
	}
}
//}