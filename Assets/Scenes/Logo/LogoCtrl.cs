using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Can probably reuse for other UIs
public class LogoCtrl : MonoBehaviour
{
    public void Transition(GameObject target)
    {
        this.gameObject.SetActive(false);
        target.SetActive(true);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
