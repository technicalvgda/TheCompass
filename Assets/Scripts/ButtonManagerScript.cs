/*
Master Script that handles all the UI buttons
*/ 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class ButtonManagerScript : MonoBehaviour {
	/* Stores the Event System GameObject of the scene */ 
	public GameObject eventSystem;

	/* Gets the EventSystem component from eventSystem */ 
	public EventSystem es;

	/* Stores the slotText aka the middle button */ 
	public Text slotText;

	/* Stores the gameobjects of the UI elements */ 
	public GameObject mainMenu, saveMenu, optionMenu, loadMenu,pauseMenu,extrasMenu,cursorSelectionMenu,gameOverMenu,confirmPrompt;

	/* Stores the active screen (To be used in the onBack()) */ 
	public GameObject activeOnScreen;

	public Dropdown resolutionDropdown;

	public Toggle fullscreenToggle;

	private bool _isPaused;
	public GameObject _pauseCanvasMenuObject;
	public GameOver _gameOverScript;
	private string _nameOfButton;

	/* Finds the UI elements and sets them to inactive. Also sets the slot text to the level that the user is on. */ 
	void Start()
	{
		mainMenu = GameObject.FindGameObjectWithTag ("MainMenu");
		saveMenu = GameObject.FindGameObjectWithTag ("SaveMenu");
		optionMenu = GameObject.FindGameObjectWithTag ("OptionMenu");
		loadMenu = GameObject.FindGameObjectWithTag ("LoadMenu");
		cursorSelectionMenu = GameObject.FindGameObjectWithTag ("CursorSelectionMenu");
		pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		extrasMenu = GameObject.FindGameObjectWithTag ("ExtrasMenu");
		_pauseCanvasMenuObject = GameObject.Find ("Pause Menu");
		eventSystem = GameObject.Find ("EventSystem");
		gameOverMenu = GameObject.FindGameObjectWithTag ("GameOverMenu");
		es = eventSystem.GetComponent<EventSystem> ();
		resolutionDropdown = optionMenu.GetComponentInChildren<Dropdown> ();
		fullscreenToggle = optionMenu.GetComponentInChildren<Toggle> ();
		slotText.text = PlayerPrefs.GetString ("onLevel");
		confirmPrompt = GameObject.Find ("ConfirmMenu");

		#if(UNITY_ANDROID)
		{
			resolutionDropdown.gameObject.SetActive(false);
			fullscreenToggle.gameObject.SetActive(false);
			if(cursorSelectionMenu != null)
				cursorSelectionMenu.gameObject.SetActive(false);
			//resolutionDropdown.enabled = false;
			//fullscreenToggle.enabled = false;
			Debug.Log("mobile");
		}
		#endif
		if (confirmPrompt != null)
			confirmPrompt.SetActive (false);
		if(saveMenu != null)
			saveMenu.SetActive (false);
		optionMenu.SetActive(false);
		if(cursorSelectionMenu != null)
			cursorSelectionMenu.SetActive (false);
		if(extrasMenu != null)
			extrasMenu.SetActive (false);
		if(loadMenu != null)
			loadMenu.SetActive(false);
		if(pauseMenu != null)
			pauseMenu.SetActive(false);
		if (gameOverMenu != null) 
		{
			_gameOverScript = gameObject.GetComponent<GameOver> ();
			//gameOverMenu.SetActive (false);
		}
	}
	void Update()
	{
        //START- check if any buttons/ d-pad/ analog stick from gamepad is used. If true, make cursor invisible
		if (Input.GetAxis("GamepadVertical") != 0 || Input.GetAxis("GamepadHorizontal") != 0 || Input.GetAxis("GamepadHorizontalDPad") != 0 || Input.GetAxis("GamepadVerticalDPad") != 0)
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
			Cursor.visible = false;
			
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton8))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton11))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton12))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton13))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton14))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton15))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton16))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton17))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton18))
        {
			Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton19))
        {
			Cursor.visible = false;
        }
        //END- check if any buttons from gamepad is pressed. If true, make cursor invisible

        //if any keyboard key is pressed or the mouse is clicked, make cursor visible again
		if (Input.anyKeyDown)
        {
			if(!Input.GetKeyDown(KeyCode.JoystickButton0) && !Input.GetKeyDown(KeyCode.JoystickButton1)&&
				!Input.GetKeyDown(KeyCode.JoystickButton2) && !Input.GetKeyDown(KeyCode.JoystickButton3)&&
				!Input.GetKeyDown(KeyCode.JoystickButton4) && !Input.GetKeyDown(KeyCode.JoystickButton5)&&
				!Input.GetKeyDown(KeyCode.JoystickButton6) && !Input.GetKeyDown(KeyCode.JoystickButton7)&&
				!Input.GetKeyDown(KeyCode.JoystickButton8) && !Input.GetKeyDown(KeyCode.JoystickButton9))
            	Cursor.visible = true;
        }


        //resolutionDropdownValueChangedHandler(resolutionDropdown);
        //if ESC button is pressed, change the pause state
		if (theseScenesAreActive ()) {
			if (_gameOverScript.isGameOver == false) {
				if (Input.GetButtonDown ("Pause")) {
					_isPaused = !_isPaused;
					//if paused, bring up pause menu && stop game time
					if (_isPaused)
					{
						Pause ();
					}
					else 
					{
						Resume ();
					}

				}

			} else if (_gameOverScript.isGameOver == true) {
				Time.timeScale = 0;
			}
		}

		//Switches to joystick/keyboard mode when a vertical/horizontal axis is moved. See Edit>Project Settings>Input
		if((Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0) && es.currentSelectedGameObject == null) 
		{
			selectFirstButton (); 
		}

		//Sets up the "Cancel" input which the B button is conncected to to call onBack(). See Edit>Project Settings>Input
		if(Input.GetButtonUp("Cancel") && activeOnScreen != null) 
		{
			onBack ();
		}
	}

	/* Returns true if these scenes are active */ 
	static bool theseScenesAreActive ()
	{
		return SceneManager.GetActiveScene ().name == "MVPScene" || SceneManager.GetActiveScene ().name == "Level1Rough" || SceneManager.GetActiveScene ().name == "Level 0 Tutorial" || SceneManager.GetActiveScene ().name == "Level 1" || SceneManager.GetActiveScene ().name == "Level 2" || SceneManager.GetActiveScene ().name == "Level 3" || SceneManager.GetActiveScene ().name == "Level 4" || SceneManager.GetActiveScene ().name == "Level 5";
	}

	/* Loads the level */ 
	public void onLoad() 
	{
		Debug.Log ("Loading: " + PlayerPrefs.GetString("onLevel"));
	}

	/* Prompt the user if they want to overwrite */ 
	public void onSave() 
	{
		saveMenu.SetActive (true);
		activeOnScreen = saveMenu;
	}
		
	/* Universal Back button that uses the activeOnScreen gameObject */ 
	public void onBack()
	{
		if (theseScenesAreActive ()) 
		{
			if (saveMenu.activeSelf == true) 
			{
				activeOnScreen.SetActive (false);
				activeOnScreen = optionMenu;
			} 
			else if (cursorSelectionMenu != null) 
			{
				if (cursorSelectionMenu.activeSelf == true) 
				{
					activeOnScreen.SetActive (false);
					activeOnScreen = extrasMenu;
				}
			} 
			else if (_pauseCanvasMenuObject.activeSelf == false) 
			{
				activeOnScreen.SetActive (false);
				_pauseCanvasMenuObject.SetActive (true);
				activeOnScreen = _pauseCanvasMenuObject;
				selectFirstButton ();
			}
			else 
			{
				activeOnScreen.SetActive (false);
				activeOnScreen = null;
			}
		} 
		else 
		{
			if (saveMenu != null) {
				if (saveMenu.activeSelf == true) {
					activeOnScreen.SetActive (false);
					activeOnScreen = optionMenu;
				}
			}
			else if (cursorSelectionMenu.activeSelf == true) 
			{
				activeOnScreen.SetActive (false);
				extrasMenu.SetActive (true);
				activeOnScreen = extrasMenu;
			}
			else 
			{
				activeOnScreen.SetActive (false);
				activeOnScreen = null;
			}
		}
		if(activeOnScreen == null)
		{
			if(mainMenu != null) 
			{
				mainMenu.SetActive (true);	
			}
			if(pauseMenu != null)
			{
				_pauseCanvasMenuObject.SetActive (true);
			}
			
			selectFirstButton (); 

		}
	}

	/* If onClick event is not triggered on button click then set the selected button to the FirstButtonOfMenu */ 
	void selectFirstButton ()
	{
		if (!Input.GetMouseButtonUp (0)) {
			es.SetSelectedGameObject (GameObject.FindGameObjectWithTag ("FirstButtonOfMenu"));
		}
		else 
		{
			es.SetSelectedGameObject (null);
		}
	}

	/* Saves the level */ 
	public void savePromptYes()
	{
		//Random number generator to test the saving feature
		PlayerPrefs.SetString("onLevel", "Level " + Random.Range(1,100));
		slotText.text = PlayerPrefs.GetString ("onLevel");
		Debug.Log ("Saving: " + PlayerPrefs.GetString("onLevel"));
		saveMenu.SetActive (false);
	}

	/* Closes the prompt */ 
	public void savePromptNo() 
	{
		saveMenu.SetActive (false);
	}

	//Button Manager
	// Allows the play button to switch to TestScene
	public void PlayBtn(string playGame)
	{
		SceneManager.LoadScene(playGame);
	}

	// Enables the option menu gameObject and sets the activeOnscreen to that
	public void OptionBtnEnable()
	{
		optionMenu.SetActive (true);
		if(mainMenu != null)
			mainMenu.SetActive (false);
		if (_pauseCanvasMenuObject != null)
			_pauseCanvasMenuObject.SetActive (false);
		selectFirstButton (); 
		activeOnScreen = optionMenu;

	}

	// Enables the load menu gameObject and sets the activeOnscreen to that
	public void LoadBtnEnable()
	{
		loadMenu.SetActive(true);
		mainMenu.SetActive (false);
		selectFirstButton (); 
		activeOnScreen = loadMenu;
	}

	// Filler for the credit scene
	public void CreditBtn(string startCredit)
	{
		// Debug.Log("Credits");
		SceneManager.LoadScene(startCredit);
	}

	// Application closes
	public void LeaveGameBtn(string command)
	{
		confirmPrompt.SetActive (true);
		_pauseCanvasMenuObject.SetActive (false);
		activeOnScreen = confirmPrompt;
		selectFirstButton (); 
		_nameOfButton = command;

		//Application.Quit();
	}
	public void confirmYesButton()
	{
		if (_nameOfButton == "Quit")
			Application.Quit ();
		else if (_nameOfButton == "MainMenu")
			SceneManager.LoadScene ("TitleMenu");
	}


	//Resume button method, changes pause state to original
	public void Resume()
	{
		pauseMenu.SetActive (false);
		//PauseUI.enabled = false;
		if (optionMenu.activeSelf == true)
			optionMenu.SetActive (false);
		if (saveMenu.activeSelf == true)
			saveMenu.SetActive (false);
		if (confirmPrompt.activeSelf == true)
			confirmPrompt.SetActive (false);
		Time.timeScale = 1;
	}

	// Pause button method, brings up the pause menu 
	public void Pause()
	{
		pauseMenu.SetActive (true);
		_pauseCanvasMenuObject.SetActive (true);
		//PauseUI.enabled = true;
		Time.timeScale = 0;
	}
	public void extrasMenuEnable()
	{
		extrasMenu.SetActive (true);
		mainMenu.SetActive (false);
		selectFirstButton (); 
		activeOnScreen = extrasMenu;
	}
	public void cursorSelectionMenuEnable()
	{
		cursorSelectionMenu.SetActive (true);
		extrasMenu.SetActive (false);
		activeOnScreen = cursorSelectionMenu;
	}
	public void cursorSelectionMenuDisable()
	{
	}
	public void resolutionDropdownValueChangedHandler(Dropdown target)
	{
		switch (target.value)
		{
		case 0:
			Screen.SetResolution(1920, 1080, fullscreenToggle.isOn);
			break;
		case 1:
			Screen.SetResolution(1680, 1050, fullscreenToggle.isOn);
			break;
		case 2:
			Screen.SetResolution(1600, 900, fullscreenToggle.isOn);
			break;
		case 3:
			Screen.SetResolution(1440, 900, fullscreenToggle.isOn);
			break;
		case 4:
			Screen.SetResolution(1400, 1050, fullscreenToggle.isOn);
			break;
		case 5:
			Screen.SetResolution(1366, 768, fullscreenToggle.isOn);
			break;
		case 6:
			Screen.SetResolution(1360, 768, fullscreenToggle.isOn);
			break;
		case 7:
			Screen.SetResolution(1280, 1024, fullscreenToggle.isOn);
			break;
		case 8:
			Screen.SetResolution(1280, 960, fullscreenToggle.isOn);
			break;
		case 9:
			Screen.SetResolution(1280, 800, fullscreenToggle.isOn);
			break;
		case 10:
			Screen.SetResolution(1280, 768, fullscreenToggle.isOn);
			break;
		case 11:
			Screen.SetResolution(1280, 720, fullscreenToggle.isOn);
			break;
		case 12:
			Screen.SetResolution(1280, 600, fullscreenToggle.isOn);
			break;
		case 13:
			Screen.SetResolution(1152, 864, fullscreenToggle.isOn);
			break;
		case 14:
			Screen.SetResolution(1024, 768, fullscreenToggle.isOn);
			break;
		case 15:
			Screen.SetResolution(800, 600, fullscreenToggle.isOn);
			break;
		case 16:
			Screen.SetResolution(640, 480, fullscreenToggle.isOn);
			break;
		case 17:
			Screen.SetResolution(640, 400, fullscreenToggle.isOn);
			break;
		case 18:
			Screen.SetResolution(512, 384, fullscreenToggle.isOn);
			break;
		case 19:
			Screen.SetResolution(400, 300, fullscreenToggle.isOn);
			break;
		case 20:
			Screen.SetResolution(320, 240, fullscreenToggle.isOn);
			break;
		case 21:
			Screen.SetResolution(320, 200, fullscreenToggle.isOn);
			break;

		}
	}


}
