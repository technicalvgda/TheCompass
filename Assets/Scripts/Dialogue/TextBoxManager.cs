using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
	
    public Text speakerText,mainBodyText;

    public TextAsset textFile;
    public string[] textLines;
    public AudioSource voiceOverAudioSource,secondVoiceOverAudioSource;
    public AudioSource typingSoundAudioSource;
	//the number of seconds to wait until each beep
	public float typingWaitSec;
	private bool _activatedTypingBeepCoroutine;
    public int currentLine;
    public int endAtLine;

    public bool isActive;
	public bool currentlyInCommentary;

    public bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed = 0.05f;
	private RectTransform _rectTransform;
	public float movementSpeed;
	private bool  _dialogueIsFinished;
	private float _timer;
	private bool _timedCommentaryActive;

	public LoadingTransition loadingTransition;
	public bool activateTransition;
	private bool _playedSecondAudioClipOnce;
    // Use this for initialization
    void Start()
    {
		_playedSecondAudioClipOnce = false;
		loadingTransition = GameObject.FindGameObjectWithTag ("TransitionObject").GetComponent<LoadingTransition>();
		_timedCommentaryActive = false;
		_activatedTypingBeepCoroutine = false;
		_rectTransform = transform.GetComponent<RectTransform> ();
		if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if(isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
		//TEMPORARY DELETE AFTER LAUNCH
		typingSoundAudioSource.volume = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
		if (!isActive) {
			return;
		} 

        //theText.text = textLines[currentLine];
       // toContinueText.color = new Color(toContinueText.color.r, toContinueText.color.g, toContinueText.color.b, Mathf.PingPong(Time.time, 1));
		if (_timedCommentaryActive) 
		{
			_timer -= Time.unscaledDeltaTime;
			//Debug.Log (_timer);
			if (_timer <= 0) 
			{
				_timedCommentaryActive = false;
				DisableTextBox ();
				Debug.Log ("Timed commentary done");
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!isTyping) {

				//AudioSource audio = GetComponent<AudioSource>();
				//audio.Stop();
				currentLine += 1;
				if (currentLine > endAtLine) {
					DisableTextBox ();
				} else {
					StartCoroutine (TextScroll (textLines [currentLine]));
					if (secondVoiceOverAudioSource.clip != null && currentLine == 1 && !_playedSecondAudioClipOnce) {
						voiceOverAudioSource.Stop ();
						secondVoiceOverAudioSource.Play ();
						_playedSecondAudioClipOnce = true;
					}
				}
			} else if (isTyping && !cancelTyping) {
				cancelTyping = true;
				if (voiceOverAudioSource.isPlaying)
					voiceOverAudioSource.Stop ();
				if (secondVoiceOverAudioSource.isPlaying)
					secondVoiceOverAudioSource.Stop ();
			}            
		} else if (voiceOverAudioSource.clip != null && !voiceOverAudioSource.isPlaying) 
		{
			DisableTextBox ();
			cancelTyping = true;
		}
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
		mainBodyText.text = "";
        isTyping = true;
        cancelTyping = false;
		if (_activatedTypingBeepCoroutine == false) 
		{
			_activatedTypingBeepCoroutine = true;
			StartCoroutine (TypingBeeping (typingWaitSec));

		}
        //toContinueTextBox.SetActive(false);
        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
			mainBodyText.text += lineOfText[letter];
            letter++;
			yield return new WaitForSecondsRealtime(typeSpeed);
        }
        //toContinueTextBox.SetActive(true);
		mainBodyText.text = lineOfText;
        isTyping = false;
		_activatedTypingBeepCoroutine = false;
        cancelTyping = false;
    }

    public void setVoiceOverSourceClip(AudioClip clip)
    {
        //AudioSource audio = GetComponent<AudioSource>();
		if(voiceOverAudioSource.isPlaying)
			voiceOverAudioSource.Stop ();
        voiceOverAudioSource.clip = clip;
        voiceOverAudioSource.Play();
        if (Input.GetKeyDown("space")) voiceOverAudioSource.Stop();
    }
	public void setVoiceOverSourceClip(AudioClip clip,float sec)
	{
		//AudioSource audio = GetComponent<AudioSource>();
		if(voiceOverAudioSource.isPlaying)
			voiceOverAudioSource.Stop ();
		voiceOverAudioSource.clip = clip;
		voiceOverAudioSource.Play();
		voiceOverAudioSource.time = sec;
		if (Input.GetKeyDown("space")) voiceOverAudioSource.Stop();
	}
	public void setSecondVoiceOverSourceClip(AudioClip clip)
	{
		//AudioSource audio = GetComponent<AudioSource>();
		secondVoiceOverAudioSource.clip = clip;
		_playedSecondAudioClipOnce = false;
	}

    public void EnableTextBox()
    {
        //textBox.SetActive(true);
		isActive = true;
	
        StartCoroutine(TextScroll(textLines[currentLine]));

    }

    public void DisableTextBox()
    {
        //textBox.SetActive(false);
        isActive = false;
		mainBodyText.text = " ";
    }

    //make it so that we can use different dialogue scripts
    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('@'));
			mainBodyText.text = " ";
			StopCoroutine(TextScroll(" "));
        }
    }
	public void setSpeakerNameText(string speakerName)
	{
		speakerText.text = speakerName;
	}
	public void startCommentaryDialogue()
	{		
		if (currentlyInCommentary) 
		{
			StopAllCoroutines ();
		}
		StartCoroutine (StartCommentary ());
	}
	IEnumerator StartCommentary()
	{
		currentlyInCommentary = true;
		//get stop position
		Vector2 _newPos = new Vector2(0,-145);
		_rectTransform.anchoredPosition = _newPos;
		_newPos = new Vector2(_rectTransform.anchoredPosition.x, -15f);
		//move the box up
		while (_rectTransform.anchoredPosition.y < -16f) 
		{
			_rectTransform.anchoredPosition = Vector2.Lerp (_rectTransform.anchoredPosition, _newPos, Time.unscaledDeltaTime * movementSpeed);
			yield return new WaitForSecondsRealtime (0.01f);
		}
		//enable the commentary
		isActive = true;
		EnableTextBox ();
		//wait while text is still active
		while (isActive == true) 
		{
			yield return new WaitForSecondsRealtime (0.01f);
		}
		//get position off screen
		_newPos = new Vector2(_rectTransform.anchoredPosition.x, -145f);
		//move the box back down
		while (_rectTransform.anchoredPosition.y > -144f) 
		{
			_rectTransform.anchoredPosition = Vector2.Lerp (_rectTransform.anchoredPosition, _newPos, Time.unscaledDeltaTime * movementSpeed);
			yield return new WaitForSecondsRealtime (0.01f);
		}
		currentlyInCommentary = false;
		if (activateTransition == true) 
		{
			Time.timeScale = 1;
			loadingTransition.startCommentaryDialogue ();
		}
	}
	public void activateTimedCommentary(float time)
	{
		//Debug.Log ("TIMED COMMENTARY");
		_timer = time;
		//Debug.Log ("TIMER: " + _timer);
		_timedCommentaryActive = true;
	}

	public void pressedOnDialogueBox()
	{
		if(!isTyping)
		{

			AudioSource audio = GetComponent<AudioSource>();
			audio.Stop();
			currentLine += 1;
			if(currentLine > endAtLine)
			{
				DisableTextBox();
			}
			else
			{
				StartCoroutine(TextScroll(textLines[currentLine]));
			}
		}
		else if(isTyping && !cancelTyping)
		{
			cancelTyping = true;
		}
	}
	public void activateTransitionOnFinished()
	{
		activateTransition = true;
		Time.timeScale = 0;
	}
	IEnumerator TypingBeeping(float sec)
	{
		while (isTyping) 
		{
			typingSoundAudioSource.PlayOneShot (typingSoundAudioSource.clip);
			yield return new WaitForSecondsRealtime (sec);
		}
	}
}