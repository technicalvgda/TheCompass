using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Allows to load scenes using specific syntax


public class ButtonManager : MonoBehaviour {

    public Canvas optionMenu,loadMenu;
    
	// Use this for initialization
	void Start ()
    {
        optionMenu = optionMenu.GetComponent<Canvas>();  // Refer to the option canvas
        optionMenu.enabled = false;
		loadMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // When option button is clicked, you can press the ESC key to go back to the menu
        if(optionMenu.enabled == true && Input.GetKey(KeyCode.Escape))
        {
            optionMenu.enabled = false;
        }
	}

    // Allows the play button to switch to TestScene
    public void PlayBtn(string playGame)
    {
        SceneManager.LoadScene(playGame);
    }

    // Enables the option menu canvas
    public void OptionBtnEnable()
    {
        optionMenu.enabled = true;

    }
	public void OptionBtnDisable()
	{
		optionMenu.enabled = false;

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

	public void LoadBtnEnable()
	{
		loadMenu.enabled = true;
	}
	public void LoadBtnDisable()
	{
		loadMenu.enabled = false;
	}


}
