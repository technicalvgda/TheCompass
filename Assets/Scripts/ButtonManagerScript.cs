/*
Master Script that handles all the UI buttons
*/ 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonManagerScript : MonoBehaviour {

	/* Stores the slotText aka the middle button */ 
	public Text slotText;

	/* Stores the gameobjects of the UI elements */ 
	public GameObject saveMenu, optionMenu, loadMenu,pauseMenu;

	/* Stores the active screen (To be used in the onBack()) */ 
	public GameObject activeOnScreen;

	private bool _isPaused;

	/* Finds the UI elements and sets them to inactive. Also sets the slot text to the level that the user is on. */ 
	void Start()
	{
		saveMenu = GameObject.FindGameObjectWithTag ("SaveMenu");
		optionMenu = GameObject.FindGameObjectWithTag ("OptionMenu");
		loadMenu = GameObject.FindGameObjectWithTag ("LoadMenu");
		pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		slotText.text = PlayerPrefs.GetString ("onLevel");
		saveMenu.SetActive (false);
		optionMenu.SetActive(false);
		if(loadMenu != null)
			loadMenu.SetActive(false);
		if(pauseMenu != null)
			pauseMenu.SetActive(false);
	}
	void Update()
	{
		//if ESC button is pressed, change the pause state
		if (Application.loadedLevelName == "MVPScene") {
			if (Input.GetButtonDown ("Pause")) {
				_isPaused = !_isPaused;
			}

			//if paused, bring up pause menu && stop game time
			if (_isPaused) {
				pauseMenu.SetActive (true);
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
		if (saveMenu.activeSelf == true) 
		{
			activeOnScreen.SetActive (false);
			activeOnScreen = optionMenu;
		} 
		else 
		{
			activeOnScreen.SetActive (false);
			activeOnScreen = null;
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
		activeOnScreen = optionMenu;

	}

	// Enables the load menu gameObject and sets the activeOnscreen to that
	public void LoadBtnEnable()
	{
		loadMenu.SetActive(true);
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



}
