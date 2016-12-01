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
        nextScene = scene;
        anim.SetTrigger(-556287998); // "Exit Scene"
    }

    void LoadNextScene()
    {
        if (nextScene != null)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(FALLBACK_SCENE);
        }
    }

    public void LoadSavedGame()
    {
        GameData data = (GameData)SaveLoad.LoadFile(AutoSave.defaultFilePath);
        if (data != null)
        {
            LoadScene(data.Scene);
        }
        else
        {
            LoadScene(null);
        }
    }

    public void Quit()
    {
        Debug.Log("you are leave");
        Application.Quit();
    }
}
