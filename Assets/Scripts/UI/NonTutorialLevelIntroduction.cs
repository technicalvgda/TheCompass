using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//This scene serves as the handler for the black screen fade out introduction for each non-tutorial level 
public class NonTutorialLevelIntroduction : MonoBehaviour {
	public ButtonManagerScript buttonManagerScript;
	public Image blackScreen;
	public float colorFadeSpeed;
	private Color _color;
	public bool skipIntro;
	// Use this for initialization
	void Start () {
		buttonManagerScript.enterCutscene ();
		if (skipIntro == true) 
		{
			blackScreen.color = Color.clear;
		}
		else
			StartCoroutine (NonTutorialIntro ());
	}

	IEnumerator NonTutorialIntro()
	{
		Time.timeScale = 0;
		float startTime = Time.unscaledTime;
		float fade;
		yield return new WaitForSecondsRealtime (1f);

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
}