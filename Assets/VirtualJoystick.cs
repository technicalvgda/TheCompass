using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler{

	private Image _bgImg;
	private Image _joystickImg;
	private Vector3 inputVector;

	void Start()
	{
		_bgImg = GetComponent<Image> ();
		_joystickImg = transform.GetChild (0).GetComponent<Image> ();
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (_bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) 
		{
			pos.x = (pos.x / _bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / _bgImg.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x, 0, pos.y);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			//Move the joystick image
			_joystickImg.rectTransform.anchoredPosition = 
				new Vector3 (inputVector.x * (_bgImg.rectTransform.sizeDelta.x / 3)
					, inputVector.z * (_bgImg.rectTransform.sizeDelta.y / 3));
				
			Debug.Log (pos);
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector3.zero;
		_joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float Horizontal()
	{
		if (inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis ("Horizontal");
	}
	public float Vertical()
	{
		if (inputVector.z != 0)
			return inputVector.z;
		else
			return Input.GetAxis ("Vertical");
	}
	public Vector3 right()
	{
		if (inputVector.x > 0)
			return transform.right;
		else
			return Vector3.zero;
	}
	public Vector3 left()
	{
		if (inputVector.x > 0)
			return transform.left;
		else
			return Vector3.zero;
	}
	public Vector3 up()
	{
		if (inputVector.x > 0)
			return transform.right;
		else
			return Vector3.zero;
	}
	public Vector3 right()
	{
		if (inputVector.x > 0)
			return transform.right;
		else
			return Vector3.zero;
	}

}
