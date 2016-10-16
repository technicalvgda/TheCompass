using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {

    public GameObject GameOverUI;
    private Player player;
    
    //hides game over canvas at the start and duration of the game
    void Start()
    {
        GameOverUI.SetActive(false);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
       // player = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if player's health is 0 or less, stop game time & set game over canvas to active
        if (player.getHealth() <= 0)
        {
           GameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //reloads the level that is currently executing
    public void Retry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //loads up title menu level
    public void Quit()
	{	
		SceneManager.LoadScene ("TitleScene");	
        //Application.LoadLevel("TitleMenu");
    }
}
