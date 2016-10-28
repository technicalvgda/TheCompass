using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*This script implements everything regarding to test HUD. Controls
The hurt effect effect, virtual joystick, and enemy wave countdown timer. 
Parts of this script could be moved to scripts
if needed to consolidate*/
public class HUDManager : MonoBehaviour 
{
	//The canvas game object for the hurt gradient
	public GameObject hurtEffectObject;
	//The text object for the countdown timer
	public GameObject timerTextBox;
	//The speed of which the flashing happens
	public float flashSpeed;
	//The starting time for each wave
	public float startingWaveTimer;
	//The temporary slider that controls the player's health to test the gradient
	public Slider slider;
	//The number at which the manager recognizes the player is in critical health
	public float criticalHealthLevel;
	//The actual count down timer
	private float _waveTimer;
	//The text box to display the time
	public Text _waveTimerText;
	//The player
	private GameObject _player;
	//The temporary player controller, switch to actual player controller after merging, delete this controller script
	private Player _playerCont;
	//The image of the gradient
	private Image _hurtEffectImg;
	//Temp color for alpha changing
	private Color _tempColor = new Color(1,1,1,1);
	//Bool to test if the coroutine for health flashing has been activated to avoid multiple coroutines
	private bool _flashingCoroutineActive = false;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.Find ("PlayerPlaceholder");
		_playerCont = _player.GetComponent<Player> ();
		_hurtEffectImg = hurtEffectObject.GetComponent<Image> ();
		_waveTimerText = timerTextBox.GetComponent<Text> ();
		_waveTimer = startingWaveTimer;
	}

	
	// Update is called once per frame
	void Update () 
	{
		//countdown and display the timer
		_waveTimer -= Time.deltaTime;
		_waveTimerText.text = _waveTimer.ToString("F1");
		//Reset the timer and do its thing
		if (_waveTimer <= 0f) 
		{
			//Debug.Log ("Enemy wave incoming.");
			_waveTimer = startingWaveTimer;
		}
		//If the player is not in critical health
		if (_playerCont.getHealth() > criticalHealthLevel) 
		{
			//stops the coroutine if it is active
			if (_flashingCoroutineActive) 
			{
				_flashingCoroutineActive = false;
				StopCoroutine ("FlashingHealth");
			}
			//increase the intensity of the gradient as the player gets hurt
			_tempColor.a = Mathf.Abs (_playerCont.getHealth() - 100f) * 0.01f;
			_hurtEffectImg.color = _tempColor;
		} 
		else 
		{
			//If the player is in critical health initiate the flashing coroutine
			if (_flashingCoroutineActive == false) 
			{
				Debug.Log ("STARTING COROUTINE");
				_flashingCoroutineActive = true;
				StartCoroutine ("FlashingHealth");
			}
		}	
	}
	//Used to test the gradient code in Update by using a slider bar to control health
	public void sliderBarChangeHealth()
	{
		_playerCont.setHealth (slider.value * 100f);
	}

	//The IEnumertor to control the flashing. Oscillates back and forth between the extremes of min and max alpha
	IEnumerator FlashingHealth()
	{
		bool _fadeIn = true;
		_tempColor.a = _hurtEffectImg.color.a;
		while (_playerCont.getHealth() <= criticalHealthLevel) 
		{
			if (_fadeIn) 
			{
				if (_hurtEffectImg.color.a <= 1f) 
				{
					_tempColor.a += Time.deltaTime * flashSpeed;
					_hurtEffectImg.color = _tempColor;
				} else
					_fadeIn = false;
			} 
			else 
			{
				if (_hurtEffectImg.color.a >= 0f) 
				{
					_tempColor.a -= Time.deltaTime * flashSpeed;
					_hurtEffectImg.color = _tempColor;
				} 
				else
					_fadeIn = true;
			}
			yield return new WaitForSeconds (0.1f);
		}
		yield return null;
	}
}