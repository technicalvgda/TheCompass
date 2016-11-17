using UnityEngine;
using System.Collections;

//This script handles the sliding in of the minimap
public class MinimapSlideController : MonoBehaviour {
	private RectTransform _rectTransform;
	private Vector2 _tempVec;
	public float slideSpeed;
	// Use this for initialization
	void Start () {
		_rectTransform = transform.GetComponent<RectTransform> ();
		StartCoroutine (SlideMinimap ());
	
	}

	IEnumerator SlideMinimap()
	{
		while (_rectTransform.anchoredPosition.x <= 0) 
		{
			_tempVec = new Vector2 (_rectTransform.anchoredPosition.x + Time.deltaTime*slideSpeed, _rectTransform.anchoredPosition.y);
			_rectTransform.anchoredPosition = _tempVec;
			yield return null;
		}
		//minimap can slide past the exact corner so after sliding it set the position to be the corner
		_tempVec = new Vector2 (0, 0);
		_rectTransform.anchoredPosition = _tempVec;
	}
}
