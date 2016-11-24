using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {

    public GameObject GameOverUI;
    private Player player;
	public bool isGameOver;
	private ButtonManagerScript _butManagerScript;
    
    //hides game over canvas at the start and duration of the game
    void Start()
    {
		isGameOver = false;
        GameOverUI.SetActive(false);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		//_butManagerScript = Camera.main.GetComponent<ButtonManagerScript> ();
       // player = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if player's health is 0 or less, stop game time & set game over canvas to active
        if (player.getHealth() <= 0 || player.getFuel01() <= 0)
        {
           	GameOverUI.SetActive(true);
			isGameOver = true;
			//_butManagerScript.selectFirstButtonForGameOverCanvas ();
        }
    }

    //reloads the level that is currently executing
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //loads up title menu level
    public void Quit()
	{	
		SceneManager.LoadScene ("MainMenu");	
        //Application.LoadLevel("TitleMenu");
    }
}
