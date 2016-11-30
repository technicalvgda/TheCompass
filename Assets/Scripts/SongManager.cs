using UnityEngine;
using System.Collections;


public class SongManager : MonoBehaviour {

    //You would need to create an empty game object that has 2 audio sources on it.
    //Each one would have a different music clip attached to it. 
    //It would also have a music manager script, which has references to both audio sources.
    //Inside the script, it should set the second audio source to 0 volume at the start.
    //It should also have a static function that calls an IEnumerator function to fade the 
    //first audio source out (by slowly lowering the volume to 0) and fades the second audio 
    //source in (by slowly raising the volume to 1). I already have code in the player script 
    //that fades the acceleration sound effect in and out, you can look at that for reference on fading tracks

    public AudioSource audio1;
    public AudioSource audio2;

    float MAX_VOLUME = 1;
    

    // Use this for initialization
    void Start ()
    {
        PlayerPrefs.SetFloat("MSTRSlider", 1);
        PlayerPrefs.SetFloat("BGSlider", 1);
        MAX_VOLUME = SoundSettingCompare("BGSlider");
        audio2.volume = 0f;
        audio1.volume = MAX_VOLUME;
        //audio1.Play();
        //subscribe music shift function to part pickup event
        TractorBeamControls.partPickupDelegate += ChangeAux1to2;
	}

    void OnDisable()
    {
        TractorBeamControls.partPickupDelegate -= ChangeAux1to2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //ChangeAux1to2();
	}

    public void ChangeAux1to2()
    {      
       StartCoroutine(FadeOneToTwo());      
    }

    private IEnumerator FadeOneToTwo()
    {
        if (audio1.isPlaying)
        {
            //set max volume to the current max
            MAX_VOLUME = SoundSettingCompare("BGSlider");

            audio2.volume = 0;
            audio2.Play();
            while (audio1.volume > 0 && audio2.volume < MAX_VOLUME)
            {
                audio1.volume -= 0.1f;
                audio2.volume += 0.1f;
                yield return new WaitForSeconds(0.3f);
            }
            audio1.Stop();
            
        }
        yield return null;
    }


    //send in the playerpref for "BGSlider" or "FXSlider" and compare it against master volume
    //return the lower of the two
    private float SoundSettingCompare(string prefName)
    {
        float compareVolume = PlayerPrefs.GetFloat(prefName);
        float masterVolume = PlayerPrefs.GetFloat("MSTRSlider");
        if (masterVolume > compareVolume)
        {
            return compareVolume;
        }
        else
        {
            return masterVolume;
        }

    }
}
