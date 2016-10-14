using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TwineTest : MonoBehaviour {
    public TextAsset DialogueFile;
    public TwineDialogue TDialogue;
    public PassageNode CurrentPassage;

    /* UI Stuff */
    public Button ChoiceButtonPrefab;
    public RectTransform ChoicePanel;
    public float VerticalSpacing = -45f;
    public Text PassageTextDisplay;
    public string PassageText;
    public List<Button> Choices;
    bool _currentlyTyping;
    
	void Start () {
        TDialogue = TwineReader.Parse(DialogueFile);
        CurrentPassage = TDialogue.StartPassage;
        PassageText = CurrentPassage.GetContent();
        _currentlyTyping = true;
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentlyTyping = false;
            PassageTextDisplay.text = PassageText;
            AddChoiceButtons();
        }
        if (_currentlyTyping)
        {
            string currentText = PassageTextDisplay.text;
            if (currentText.Length < PassageText.Length)
            {
                PassageTextDisplay.text = PassageText.Substring(0, currentText.Length + 1);
            }
            else
            {
                _currentlyTyping = false;
                AddChoiceButtons();
            }
        }
    }
    public void AddChoiceButtons()
    {
        List<string> choiceString = CurrentPassage.GetChoices();
        for (int i = 0; i < choiceString.Count; i++)
        {
            Button newChoice = (Button)Instantiate(ChoiceButtonPrefab, ChoicePanel, false);
            newChoice.GetComponent<ChoiceButton>().tt = this;
            newChoice.GetComponentInChildren<Text>().text = choiceString[i];
            Choices.Add(newChoice);
        }
    }
    public void ChoiceSelect(string choiceContent)
    {
        PassageNode decision = CurrentPassage.GetDecision(choiceContent);
        if (decision != null)
        {
            CurrentPassage = decision;
            foreach (Button b in Choices)
            {
                Destroy(b.gameObject);
            }
            Choices.Clear();
            PassageTextDisplay.text = "";
            PassageText = CurrentPassage.GetContent();
            _currentlyTyping = true;
        }
        else
        {
            Debug.Log("ChoiceSelect failed!");
        }
    }
}
