using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TwineTest : MonoBehaviour {
	#region Variable Declaration
	public TextAsset DialogueFile;

	/* UI Stuff */
	public Button ChoiceButtonPrefab;
	public Button ChoiceButtonDisabledPrefab;
	public RectTransform ChoicePanel;
	public float VerticalSpacing = -45f;
	public Text PassageTextDisplay;
	public string NextScene;
	public string ContinueText;

	[HideInInspector]
	public string PassageText;
	[HideInInspector]
	public List<Button> Choices;
	[HideInInspector]
	public TwineDialogue TDialogue;
	[HideInInspector]
	public PassageNode CurrentPassage;

	bool _currentlyTyping;
	#endregion

	void Start () {
		TDialogue = TwineReader.Parse(DialogueFile);
		CurrentPassage = TDialogue.StartPassage;
		PassageText = CurrentPassage.GetContent();
		_currentlyTyping = true;
	}
	void Update()
	{
		if (Input.anyKeyDown && _currentlyTyping)
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
		List<string> choiceList = CurrentPassage.GetChoices();
		List<string> disabledChoices = CurrentPassage.GetChoicesVisited(CurrentPassage.GetChoicesTagged(choiceList, TDialogue.ShowOnceTag));
		for (int i = 0; i < choiceList.Count; i++)
		{
			if (!disabledChoices.Contains(choiceList[i]))
			{
				Button newChoice = (Button)Instantiate(ChoiceButtonPrefab, ChoicePanel, false);
				newChoice.GetComponent<ChoiceButton>().tt = this;
				newChoice.GetComponentInChildren<Text>().text = choiceList[i];
				Choices.Add(newChoice);
				if (i == 0)
					newChoice.tag = "FirstButtonOfMenu";
			}
			else
			{
				Button newChoice = (Button)Instantiate(ChoiceButtonDisabledPrefab, ChoicePanel, false);
				newChoice.GetComponentInChildren<Text>().text = choiceList[i];
				Choices.Add(newChoice);
				if (i == 0)
					newChoice.tag = "FirstButtonOfMenu";
			}
		}
		if (Choices.Count <= 0)
		{
			Button continueButton = (Button)Instantiate(ChoiceButtonPrefab, ChoicePanel, false);
			continueButton.GetComponentInChildren<Text>().text = ContinueText;
			continueButton.GetComponent<ChoiceButton>().tt = this;
			continueButton.tag = "FirstButtonOfMenu";
		}
	}
	public void ChoiceSelect(string choiceContent)
	{
		if (Choices.Count > 0)
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
		else
		{
			SceneManager.LoadScene(NextScene);
		}
	}
}
