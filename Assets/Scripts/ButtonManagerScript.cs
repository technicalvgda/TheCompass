﻿/*
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
	public GameObject mainMenu, saveMenu, optionMenu, loadMenu,pauseMenu,extrasMenu,cursorSelectionMenu,gameOverMenu;

	/* Stores the active screen (To be used in the onBack()) */ 
	public GameObject activeOnScreen;

	public Dropdown resolutionDropdown;

	public Toggle fullscreenToggle;

	private bool _isPaused;
	public GameObject _pauseCanvasMenuObject;
	private GameOver _gameOverScript;

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
		#if(UNITY_ANDROID)
		{
			resolutionDropdown.gameObject.SetActive(false);
			fullscreenToggle.gameObject.SetActive(false);
			cursorSelectionMenu.gameObject.SetActive(false);
			//resolutionDropdown.enabled = false;
			//fullscreenToggle.enabled = false;
			Debug.Log("mobile");
		}
		#endif
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
		//resolutionDropdownValueChangedHandler(resolutionDropdown);
		//if ESC button is pressed, change the pause state
		if (Application.loadedLevelName == "MVPScene" || Application.loadedLevelName == "Level1Rough") 
		{
			if (_gameOverScript.isGameOver == false) 
			{
				if (Input.GetButtonDown ("Pause")) 
				{
					_isPaused = !_isPaused;
				}

				//if paused, bring up pause menu && stop game time
				if (_isPaused) {
					pauseMenu.SetActive (true);
					_pauseCanvasMenuObject.SetActive (true);
					//PauseUI.enabled = true;
					Time.timeScale = 0;
				}

				//if not paused, deactivate pause menu && continue game time
				if (!_isPaused) {
					pauseMenu.SetActive (false);
					//PauseUI.enabled = false;
					if (optionMenu.activeSelf == true)
						optionMenu.SetActive (false);
					if (saveMenu.activeSelf == true)
						saveMenu.SetActive (false);
					Time.timeScale = 1;

					/*
			if (optionsCanvas.enabled == true)
				optionsCanvas.enabled = false;
			Time.timeScale = 1;*/
				}
			} 
			else if (_gameOverScript.isGameOver == true) 
			{
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
		if (Application.loadedLevelName == "MVPScene" || Application.loadedLevelName == "Level1Rough") 
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
				_pauseCanvasMenuObject.SetActive (true);
				optionMenu.SetActive (false);
				activeOnScreen = pauseMenu;
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
			if (saveMenu.activeSelf == true)
			{
				activeOnScreen.SetActive (false);
				activeOnScreen = optionMenu;
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
				mainMenu.SetActive (true);
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
	public void promptYes()
	{
		//Random number generator to test the saving feature
		PlayerPrefs.SetString("onLevel", "Level " + Random.Range(1,100));
		slotText.text = PlayerPrefs.GetString ("onLevel");
		Debug.Log ("Saving: " + PlayerPrefs.GetString("onLevel"));
		saveMenu.SetActive (false);
	}

	/* Closes the prompt */ 
	public void promptNo() 
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
	public void QuitBtn()
	{
		Application.Quit();
	}

	//Resume button method, changes pause state to original
	public void Resume()
	{
		_isPaused = false;
	}

	// Pause button method, brings up the pause menu 
	public void Pause()
	{
		_isPaused = true;
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
