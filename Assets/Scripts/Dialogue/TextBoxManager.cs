using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public Text speakerText,mainBodyText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public bool isActive;

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed = 0.05f;
	private RectTransform _rectTransform;
	public float movementSpeed;
	private bool  _dialogueIsFinished;
    // Use this for initialization
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
		if (!isActive) {
			return;
		} 

        //theText.text = textLines[currentLine];
       // toContinueText.color = new Color(toContinueText.color.r, toContinueText.color.g, toContinueText.color.b, Mathf.PingPong(Time.time, 1));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isTyping)
            {


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
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
		mainBodyText.text = "";
        isTyping = true;
        cancelTyping = false;
        //toContinueTextBox.SetActive(false);
        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
			mainBodyText.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }
        //toContinueTextBox.SetActive(true);
		mainBodyText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableTextBox()
    {
        //textBox.SetActive(true);
        
        StartCoroutine(TextScroll(textLines[currentLine]));

    }

    public void DisableTextBox()
    {
        //textBox.SetActive(false);
        isActive = false;
    }

    //make it so that we can use different dialogue scripts
    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('@'));
        }
    }
	public void setSpeakerNameText(string speakerName)
	{
		speakerText.text = speakerName;
	}
	public void startCommentaryDialogue()
	{
		
		StartCoroutine (StartCommentary ());
	}
	IEnumerator StartCommentary()
	{
		//get stop position
		Vector2 _newPos = new Vector2(_rectTransform.anchoredPosition.x, -15f);
		//move the box up
		while (_rectTransform.anchoredPosition.y < -16f) 
		{
			_rectTransform.anchoredPosition = Vector2.Lerp (_rectTransform.anchoredPosition, _newPos, Time.deltaTime * movementSpeed);
			yield return new WaitForSeconds (0.01f);
		}
		//enable the commentary
		isActive = true;
		EnableTextBox ();
		//wait while text is still active
		while (isActive == true) 
		{
			yield return new WaitForSeconds (0.01f);
		}
		//get position off screen
		_newPos = new Vector2(_rectTransform.anchoredPosition.x, -145f);
		//move the box back down
		while (_rectTransform.anchoredPosition.y > -144f) 
		{
			_rectTransform.anchoredPosition = Vector2.Lerp (_rectTransform.anchoredPosition, _newPos, Time.deltaTime * movementSpeed);
			yield return new WaitForSeconds (0.01f);
		}
	}
}