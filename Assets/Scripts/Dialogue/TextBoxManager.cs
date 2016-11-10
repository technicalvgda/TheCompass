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
    // Use this for initialization
    void Start()
    {
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
        if(!isActive)
        {
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
            textLines = (theText.text.Split('\n'));
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
		//TODO: IMPLEMENT DIALOGUE BOX SLIDING AND TEXT TYPING
		//1) Slide box up
		//2) Start text typing
		//3) Once dialogue is done scroll text box back down
		yield return null;
	}
}