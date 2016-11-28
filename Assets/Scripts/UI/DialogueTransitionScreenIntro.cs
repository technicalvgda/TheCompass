using UnityEngine;
using System.Collections;

public class DialogueTransitionScreenIntro : MonoBehaviour {
	public TwineTest twineTest;
	public RectTransform transitionRectTransform;
	public float movementSpeed;
	// Use this for initialization
	void Start () {
		StartCoroutine (SlidePanel ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SlidePanel()
	{
		//twineTest.enabled = false;
		//Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(0.5f);
		//get stop position
		Vector2 _newPos = new Vector2(-800, transitionRectTransform.anchoredPosition.y);
		//move the box up
		while (transitionRectTransform.anchoredPosition.x > -800)
		{
			transitionRectTransform.anchoredPosition = Vector2.Lerp(transitionRectTransform.anchoredPosition, _newPos, Time.unscaledDeltaTime * movementSpeed);
			yield return new WaitForSecondsRealtime(0.01f);
		}
		//Time.timeScale = 1;
		//twineTest.enabled = true;
		//enable the commentary
		//isActive = true;
		//EnableTextBox();
		//wait while text is still active
		//while (isActive == true)
		//{
		//	yield return new WaitForSecondsRealtime(0.01f);
		//}

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
}
