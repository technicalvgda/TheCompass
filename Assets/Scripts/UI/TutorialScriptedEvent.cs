using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScriptedEvent : MonoBehaviour {
	public ButtonManagerScript buttonManagerScript;
	public Image blackScreen;
	public Text text;
	public AudioSource backgroundAudioSource;
	public float colorFadeSpeed;
	public TextBoxManager textBoxManager;
	public GameObject commentaryDialogueBox;
	public Text speakerText, mainBodyText;
	public float movementSpeed;
	private Color _color;
	public TextAsset[] textFiles;
	public string[] speakers;
	private RectTransform _rectTransform;
	private float _backgroundVolume;
	private float _masterVolume;
	private float _volume;
	public bool skipIntro;
	// Use this for initialization
	void Start () {
		buttonManagerScript.enterCutscene ();
		_rectTransform = commentaryDialogueBox.GetComponent<RectTransform> ();
		_backgroundVolume = PlayerPrefs.GetFloat ("BGSlider");
		_masterVolume = PlayerPrefs.GetFloat ("MSTRSlider");
		if (_masterVolume > _backgroundVolume)
			_volume = _backgroundVolume;
		else
			_volume = _masterVolume;
		backgroundAudioSource.volume = 0;
		if (skipIntro == true) 
		{
			blackScreen.color = Color.clear;
			text.color = Color.clear;
		}
		else
			StartCoroutine (TutorialIntro ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator TutorialIntro()
	{
		Time.timeScale = 0;
		float startTime = Time.unscaledTime;
		float fade;
		while (text.color.a <= 1) 
		{
			_color = new Color(1,1,1, Mathf.Lerp (0f, 1.01f, (Time.unscaledTime - startTime) / 1f));
			text.color = _color;
			yield return new WaitForSecondsRealtime (0.01f);
		}
		yield return new WaitForSecondsRealtime (3f);
		StartCoroutine (FadeInBackgroundMusic ());
		while (text.color.a >= 0) 
		{
			_color = new Color (text.color.r, text.color.g, text.color.b, text.color.a - Time.unscaledDeltaTime*colorFadeSpeed);
			text.color = _color;
			yield return new WaitForSecondsRealtime (0.01f);
		}
		
		//Time.timeScale = 1;
		Vector2 _newPos = new Vector2(_rectTransform.anchoredPosition.x, -15f);
		//move the box up
		while (_rectTransform.anchoredPosition.y < -16f) 
		{
			_rectTransform.anchoredPosition = Vector2.Lerp (_rectTransform.anchoredPosition, _newPos, Time.unscaledDeltaTime * movementSpeed);
			yield return new WaitForSecondsRealtime (0.01f);
		}
		int i = 0;
		bool readyForNextText = false;
		while (i < textFiles.Length) 
		{
			if (textBoxManager.isActive == false) 
			{
				textBoxManager.ReloadScript (textFiles [i]);
				textBoxManager.currentLine = 0;
				textBoxManager.endAtLine = 0;
				textBoxManager.setSpeakerNameText (speakers [i]);
				textBoxManager.EnableTextBox ();
				i++;
			}

			yield return new WaitForSecondsRealtime (0.1f);
		}
		while (textBoxManager.isActive == true) 
		{
			yield return new WaitForSecondsRealtime (0.1f);
		}
		_newPos = new Vector2(_rectTransform.anchoredPosition.x, -145f);
		//move the box back down
		while (_rectTransform.anchoredPosition.y > -144f) 
		{
			_rectTransform.anchoredPosition = Vector2.Lerp (_rectTransform.anchoredPosition, _newPos, Time.unscaledDeltaTime * movementSpeed);
			yield return new WaitForSecondsRealtime (0.01f);
		}
        _color = new Color(1, 1, 1, 1);
        while (blackScreen.color.a >= 0)
        {
            _color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreen.color.a - Time.unscaledDeltaTime * colorFadeSpeed);
            blackScreen.color = _color;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        buttonManagerScript.exitCutscene ();
		Time.timeScale = 1;

	}

	IEnumerator FadeInBackgroundMusic()
	{
		backgroundAudioSource.Play ();
		while (backgroundAudioSource.volume < _volume) 
		{
			backgroundAudioSource.volume += Time.unscaledDeltaTime;
			yield return new WaitForSecondsRealtime (0.1f);
		}
	}
}