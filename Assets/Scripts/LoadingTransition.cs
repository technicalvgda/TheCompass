using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingTransition : MonoBehaviour
{

    public Text mainBodyText;
    //public Text speakerText;

    public string levelToLoad;

    public TextAsset textFile;
    public string[] textLines;
    //public AudioSource voiceOverAudioSource;
    public AudioSource typingSoundAudioSource;

    public int currentLine;
    public int endAtLine;

    public bool isActive;

    private bool isTyping = false;
    //private bool cancelTyping = false;

    public float typeSpeed = 0.05f;
    private RectTransform _rectTransform;
    public float movementSpeed;
    private bool _dialogueIsFinished;
    //private float _timer;
    //private bool _timedCommentaryActive;
    // Use this for initialization
    void Start()
    {
       // _timedCommentaryActive = false;
        _rectTransform = transform.GetComponent<RectTransform>();

       
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
       

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
        ReloadScript(textFile);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        /*
        //theText.text = textLines[currentLine];
        // toContinueText.color = new Color(toContinueText.color.r, toContinueText.color.g, toContinueText.color.b, Mathf.PingPong(Time.time, 1));
        if (_timedCommentaryActive)
        {
            _timer -= Time.deltaTime;
            //Debug.Log (_timer);
            if (_timer <= 0)
            {
                _timedCommentaryActive = false;
                DisableTextBox();
                Debug.Log("Timed commentary done");
            }
        } 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTyping)
            {

                AudioSource audio = GetComponent<AudioSource>();
                audio.Stop();
                currentLine += 1;
                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }

        }
        */
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        Time.timeScale = 0;
        StartCoroutine(LoadLevelWithRealProgress(levelToLoad));
        int letter = 0;
        mainBodyText.text = "";
        isTyping = true;
        //cancelTyping = false;
        //toContinueTextBox.SetActive(false);
        while (isTyping && /*!cancelTyping &&*/ (letter < lineOfText.Length - 1))
        {
            mainBodyText.text += lineOfText[letter];
            letter++;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        //toContinueTextBox.SetActive(true);
        mainBodyText.text = lineOfText + "\n";
        isTyping = false;
        //cancelTyping = false;
    }

    /*
    public void setVoiceOverSourceClip(AudioClip clip)
    {
        //AudioSource audio = GetComponent<AudioSource>();
        voiceOverAudioSource.clip = clip;
        voiceOverAudioSource.Play();
        if (Input.GetKeyDown("space")) voiceOverAudioSource.Stop();
    }
    */

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
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('#'));
        }
    }
    /*
    public void setSpeakerNameText(string speakerName)
    {
        speakerText.text = speakerName;
    }
    */
    public void startCommentaryDialogue()
    {

        StartCoroutine(StartCommentary());
    }
    IEnumerator StartCommentary()
    {
        
        //get stop position
        Vector2 _newPos = new Vector2(0f, _rectTransform.anchoredPosition.y);
        //move the box up
        while (_rectTransform.anchoredPosition.x < -1f)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _newPos, Time.deltaTime * movementSpeed);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        //enable the commentary
        isActive = true;
        EnableTextBox();
        //wait while text is still active
        while (isActive == true)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }

        /*
        //get position off screen
        _newPos = new Vector2(_rectTransform.anchoredPosition.x, -145f);
        //move the box back down
        while (_rectTransform.anchoredPosition.y > -144f)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _newPos, Time.deltaTime * movementSpeed);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        */
    }

    IEnumerator LoadLevelWithRealProgress(string levelToLoad)
    {
        yield return new WaitForSecondsRealtime(1);

        AsyncOperation aSyncOp = SceneManager.LoadSceneAsync(levelToLoad);
        aSyncOp.allowSceneActivation = false;

        while (!aSyncOp.isDone)
        {
            if (aSyncOp.progress >= 0.9f)
            {

#if UNITY_STANDALONE || UNITY_WEBPLAYER
                //if in the web player will prompt user to press spacebar
                //loadingText.text = "Press any button to Continue"; // prompts the user to press space in order
                //loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
                if (Input.anyKeyDown)
                {
                    Time.timeScale = 1;
                    aSyncOp.allowSceneActivation = true;
                }
#elif UNITY_IOS || UNITY_ANDROID
                //when built to mobile will prompt user to touch the screen to continue 
                //loadingText.text = "Touch Screen to Continue"; // prompts the user to press space in order
                //loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
				if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
                {
                    aSyncOp.allowSceneActivation = true;
                }
#endif

            }

            //Debug.Log(aSyncOp.progress);
            yield return null;
        }

    }


    /*
    public void activateTimedCommentary(float time)
    {
        Debug.Log("TIMED COMMENTARY");
        _timer = time;
        Debug.Log("TIMER: " + _timer);
        _timedCommentaryActive = true;
    }
    

    public void pressedOnDialogueBox()
    {
        if (!isTyping)
        {

            AudioSource audio = GetComponent<AudioSource>();
            audio.Stop();
            currentLine += 1;
            if (currentLine > endAtLine)
            {
                DisableTextBox();
            }
            else
            {
                StartCoroutine(TextScroll(textLines[currentLine]));
            }
        }
        else if (isTyping && !cancelTyping)
        {
            cancelTyping = true;
        }
    }*/
}