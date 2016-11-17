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
    

    // Use this for initialization
    void Start () {
        audio2.volume = 0f;
        audio1.Play();
	}
	
	// Update is called once per frame
	void Update () {
        ChangeAux1to2();
	}

    private void ChangeAux1to2()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(FadeSoundAndEnd(audio1));
            StartCoroutine(FadeSoundAndStart(audio2));
        }
    }

    private IEnumerator FadeSoundAndEnd(AudioSource source)
    {
        StopCoroutine(FadeSoundAndStart(source));
        if (source.isPlaying)
        {
            while (source.volume > 0)
            {
                source.volume -= 0.1f;
                yield return new WaitForSeconds(0.3f);
            }
            source.Stop();
        }
        yield return null;
    }
    private IEnumerator FadeSoundAndStart(AudioSource source)
    {
        StopCoroutine(FadeSoundAndEnd(source));
        if (!source.isPlaying)
        {
            source.volume = 0;
            source.Play();
            while (source.volume < 1)
            {
                source.volume += 0.1f;
                yield return new WaitForSeconds(0.3f);
            }

        }
        yield return null;
    }
}
