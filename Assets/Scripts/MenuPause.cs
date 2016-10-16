using UnityEngine;
using System.Collections;

public class MenuPause : MonoBehaviour {

	public Canvas PauseUI;
	public Canvas optionsCanvas;

    //boolean value determining pause state
    private bool isPaused = false;

    //hides pause menu at the start and duration of the game
    void Start()
    {
		PauseUI.enabled = false;
		optionsCanvas.enabled = false;
    }

    //runs once every frame
    void Update()
    {
        //if ESC button is pressed, change the pause state
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
        }

        //if paused, bring up pause menu && stop game time
        if (isPaused)
        {
			PauseUI.enabled = true;
            Time.timeScale = 0;
        }

        //if not paused, deactivate pause menu && continue game time
        if (!isPaused)
        {
			PauseUI.enabled = false;
			if (optionsCanvas.enabled == true)
				optionsCanvas.enabled = false;
            Time.timeScale = 1;
        }
    }

    //Resume button method, changes pause state to original
    public void Resume()
    {
        isPaused = false;
    }

    //Options button method, pushes log message to assure button has been pressed. Will implement later
    public void Options()
    {
        //Debug.Log("Options menu has been accessed");
		optionsCanvas.enabled = !optionsCanvas.enabled;
    }

    //Quit button method, closes game
    public void Quit()
    {
        Application.Quit();
    }
}

