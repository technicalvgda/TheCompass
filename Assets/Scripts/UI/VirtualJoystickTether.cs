#if UNITY_IOS || UNITY_ANDROID
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
/*This script controls the tether joystick. It allows the joysticks to move
to the location wherever the player taps on the screen. When coding for player controls
return inputVector when getting the value of the joystick. The range of an inputVector axis goes
from -1 to 1. The values of the joystick is like an (X,Y) coordinate system. 
For example: If the joystick is on the left, then the value of inputVector is (-1.0,0,0)
If the joystick is on the right, then the value of inputVector is (1.0,0,0)*/
public class VirtualJoystickTether : MonoBehaviour
{

	//The color that the joystick when it appears
	public Color joystickColor;
	//The background image tha the joystick is on top of
	private Image _bgImg;
	//The joystick image
	private Image _joystickImg;
	//The vector representing the value of how much the joystick is moved in a direction
	private Vector3 _inputVector;
	//Bool to check if the joystick needs to be visible
	private bool _joystickVisible;
	//temp position vector
	private Vector2 _pos;
	//temp color. Used to change the alpha of the joystick
	private Color _tempColor;
	//Bool to check if the color of the joystick has changed. Prevents multiple execution that doesn't change anything
	private bool _changeJoystickColor;
	//The position of where the screen was tapped 
	private Vector3 _clickedJoystickPos;
	//Touch variable
	private Touch _touch;
	//Touch position vectors
	private Vector3 _touchPos, _worldPos, _padPos;

	//Initialization
	void Start()
	{
		_bgImg = GetComponent<Image> ();
		_joystickImg = transform.GetChild (0).GetComponent<Image> ();
		_changeJoystickColor = true;
		_joystickVisible = false;
	}

	//Update is called once per frame
	void Update()
	{
		//Mobile touch controls
		if (Input.touchCount > 0) 
		{
			for (int i = 0; i < Input.touchCount; i++) 
			{
				_touch = Input.GetTouch (i);
				//If the touch happened on the left side of the screen
				if (_touch.position.x > Screen.width / 1.5f) 
				{
					//Get the touch position and calculate where in the world the touch is
					_touchPos = _touch.position;
					_worldPos = Camera.main.ScreenToWorldPoint (new Vector3 (_touchPos.x, _touchPos.y, this.transform.position.z));
					_padPos = new Vector3 (_worldPos.x, _worldPos.y, this.transform.position.z);
					//If the touch began
					if (_touch.phase == TouchPhase.Began) 
					{
						//move the joystick to the position of the touch
						_joystickVisible = true;
						_changeJoystickColor = true;
						transform.position = _padPos;
					}
					//If the touch is moving
					if (_touch.phase == TouchPhase.Moved) 
					{
						//Most of the action is here - calculates the value of inputVector and moves the joystick
						if (RectTransformUtility.ScreenPointToLocalPointInRectangle (_bgImg.rectTransform, _touch.position, Camera.main, out _pos)) 
						{
							_pos.x = (_pos.x / _bgImg.rectTransform.sizeDelta.x);
							_pos.y = (_pos.y / _bgImg.rectTransform.sizeDelta.y);

							_inputVector = new Vector3 (_pos.x, _pos.y, 0);
							_inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

							//Move the joystick image
							_joystickImg.rectTransform.anchoredPosition = 
								new Vector3 (_inputVector.x * (_bgImg.rectTransform.sizeDelta.x / 2)
									, _inputVector.y * (_bgImg.rectTransform.sizeDelta.y / 2));

							Debug.Log (_inputVector);
						}
					} 
					//if the touch has ended
					else if (_touch.phase == TouchPhase.Ended) 
					{
						//reset the inputVector and the joystick position. Changes bools so that the joystick disappears
						_inputVector = Vector3.zero;
						_joystickImg.rectTransform.anchoredPosition = Vector3.zero;
						_joystickVisible = false;
						_changeJoystickColor = true;
					}
				}
			}
		}
		//handles the alpha changing of the joystick
		if (_changeJoystickColor == true) 
		{
			if (_joystickVisible == false) 
			{
				_tempColor = new Color (1, 1, 1, 0);
				_bgImg.color = _tempColor;
				_joystickImg.color = _tempColor;
				_changeJoystickColor = false;
			} 
			else 
			{
				_tempColor = new Color (1, 1, 1, 1);
				_bgImg.color = _tempColor;
				_joystickImg.color = joystickColor;
				_changeJoystickColor = false;
			}
		}
	}
}

#endif