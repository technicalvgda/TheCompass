using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIFlow : MonoBehaviour
{
    public string nextScene { private get; set; }

    public void Transition(GameObject target)
    {
        UIHelper.SetSelected(null);
        this.gameObject.SetActive(false);
        target.SetActive(true);
    }

    public void DisableSelf()
    {
        this.gameObject.SetActive(false);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadNextScene()
    {
        if (nextScene != null)
            SceneManager.LoadScene(nextScene);
    }
}
