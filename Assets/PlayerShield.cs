using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    /*
    public GameObject shield, shieldPrefab;
    private bool collision;
    */
    private float initialWaitTime = 2.0f;
    private float waitTime;
	private int _numbTimesActivated;
    Animator anim;

    //Shield Audio Source
    AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        //collision = true;
        waitTime = initialWaitTime;
        
        anim = GetComponent<Animator>();

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (waitTime <= 0 )
        {
            anim.SetBool("Colliding", false);
        }

        waitTime -= Time.deltaTime;
        
    }

    public void ActivateShield(bool playerDamaged)
    {
		_numbTimesActivated++;
        StopCoroutine(ResetAnim());
        waitTime = initialWaitTime;
       
        if (anim.GetBool("Colliding") == false)
        {
            anim.SetBool("Damage", playerDamaged);
            anim.SetBool("Colliding", true);
           
        }
        audioSrc.volume = SoundSettingCompare("FXSlider");
        // Activate Sound on collision
        if (audioSrc != null)
        {
            audioSrc.Play();
        }
        StartCoroutine(ResetAnim());
    }

    IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Damage", false);
        yield return null;
    }
	public int numbTimesShieldActivated()
	{
		return _numbTimesActivated;
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
