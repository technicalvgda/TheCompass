using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    const string FALLBACK_SCENE = "Level 0 Tutorial";
    public static string nextScene { private get; set; }
    Animator anim;

    void Awake()
    {
        if (nextScene == null)
            nextScene = FALLBACK_SCENE;

        anim = GetComponent<Animator>();
    }

    public void LoadScene(string scene)
	{
        Time.timeScale = 1;
        Debug.Log("asdada");
        nextScene = scene;
        anim.SetTrigger(-556287998); // "Exit Scene"
    }

    public void LoadSceneInstant(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    void LoadNextScene()
    {
        Time.timeScale = 1;
        Debug.Log("LoadNextScene()");
        if (nextScene != null)
        {
			Debug.Log("NEXT SCENE");
			SaveLoad.SaveGameWithScene(nextScene);
            SceneManager.LoadScene(nextScene);
        }
        else
        {
			Debug.Log("FALLBACK");
			SaveLoad.SaveGameWithScene(FALLBACK_SCENE);
            SceneManager.LoadScene(FALLBACK_SCENE);
        }
    }

    public void LoadSavedGame()
    {
        Time.timeScale = 1;
        GameData data = (GameData)SaveLoad.LoadFile(SaveLoad.defaultFilePath);
		Debug.Log(data.Scene);
        //GameData data = (GameData)SaveLoad.LoadFile(AutoSave.defaultFilePath);
        //GameData data = null;
        if (data != null)
        {
            LoadScene(data.Scene);
        }
        else
        {
            LoadScene(null);
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("you are leave");
        Application.Quit();
    }
}
