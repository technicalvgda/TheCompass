using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*This script implements everything regarding to test HUD. Controls
The hurt effect effect, virtual joystick, and enemy wave countdown timer. 
Parts of this script could be moved to scripts
if needed to consolidate*/
public class HUDManager : MonoBehaviour {
	public GameObject hurtEffectObject;
	public GameObject timerTextBox;
	public float flashSpeed;
	public float startingWaveTimer;
	public Slider slider;
	private float _waveTimer;
	private Text _waveTimerText;
	private GameObject _player;
	private TEMP_PLAYER_SCRIPT _playerCont;
	private Image _hurtEffectImg;
	private Color _tempColor = new Color(1,1,1,1);
	private bool _flashingCoroutineActive = false;
	// Use this for initialization
	void Start () {
		_player = GameObject.Find ("Player");
		_playerCont = _player.GetComponent<TEMP_PLAYER_SCRIPT> ();
		_hurtEffectImg = hurtEffectObject.GetComponent<Image> ();
		_waveTimerText = timerTextBox.GetComponent<Text> ();
	}

	
	// Update is called once per frame
	void Update () {
		_waveTimer -= Time.deltaTime;
		_waveTimerText.text = _waveTimer.ToString("F1");
		if (_waveTimer <= 0f) 
		{
			Debug.Log ("Enemy wave incoming.");
			_waveTimer = startingWaveTimer;
		}

		if (_playerCont.playerHealth > 10f) {
			if (_flashingCoroutineActive) 
			{
				_flashingCoroutineActive = false;
				StopCoroutine ("FlashingHealth");
			}
			_tempColor.a = Mathf.Abs (_playerCont.playerHealth - 100f) * 0.01f;
			//Debug.Log (_tempColor);
			_hurtEffectImg.color = _tempColor;
		} 
		else 
		{
			if (_flashingCoroutineActive == false) 
			{
				Debug.Log ("STARTING COROUTINE");
				_flashingCoroutineActive = true;
				StartCoroutine ("FlashingHealth");
			}
		}
		/*
		if (_playerHealth <= 10f) {
		} else if (_playerHealth > 10f && _playerHealth <= 50f) {
		}
		else if(_playerHealth > 50f && < 100f)
		{
		}
		if (_playerHealth == 100f) 
		{
			_tempColor.a = 0f;
			_hurtEffectImg.color = _tempColor;
		}*/
	
	}
	public void sliderBarChangeHealth()
	{
		_playerCont.playerHealth = slider.value * 100f;
	}

	IEnumerator FlashingHealth()
	{
		bool _fadeIn = true;
		_tempColor.a = _hurtEffectImg.color.a;
		while (_playerCont.playerHealth <= 10f) 
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
			Debug.Log ("HERE");
			yield return new WaitForSeconds (0.1f);
		}
		yield return null;
	}
}
