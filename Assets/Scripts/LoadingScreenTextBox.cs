using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreenTextBox : MonoBehaviour
{
    public Text mainBodyText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    private RectTransform _rectTransform;
    public float movementSpeed;

    private bool isTyping;
    private bool isDone = false;
    public bool isActive ;

    public float typeSpeed = 0.05f;


    // Use this for initialization
    void Start()
    {
        _rectTransform = transform.GetComponent<RectTransform>();
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }


    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartCommentary());



    }



    IEnumerator StartCommentary()
    {
        //get stop position
        Vector2 _newPos = new Vector2(-0.25f, _rectTransform.anchoredPosition.y);
        //move the box right
        while (_rectTransform.anchoredPosition.x < -0.26f)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _newPos, Time.deltaTime * movementSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        isActive = true;
        
        //wait while text is still active
        while (isActive == true)
        {
            StartCoroutine(TextScroll(textLines[currentLine]));
            yield return new WaitForSeconds(0.01f);
        }
        //get new stop position
        _newPos = new Vector2(-792f, _rectTransform.anchoredPosition.y);
        //move the box to the left
        while (_rectTransform.anchoredPosition.x > -0.26f)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _newPos, Time.deltaTime * movementSpeed);
            yield return new WaitForSeconds(0.01f);
        }

    }

    IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        mainBodyText.text = "";
        while (letter < lineOfText.Length)
        {
            mainBodyText.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }
        isActive = false;
    }
}