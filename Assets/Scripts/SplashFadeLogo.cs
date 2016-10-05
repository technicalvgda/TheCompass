using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashFadeLogo : MonoBehaviour
{
    //the logo
    public Image splashImage;

    //the next scene to be loaded
    public string loadscene;

    IEnumerator Start()
    {
        //sets image to invisible
        splashImage.canvasRenderer.SetAlpha(0.0f);

        //fade in method
        FadeIn();

        //waits for 2.5 seconds before fading out
        yield return new WaitForSeconds(2.5f);

        //fade out method
        FadeOut();

        //waits 2.5 seconds before loading next scene
        yield return new WaitForSeconds(2.5f);

        //loads next scene(title menu)
        SceneManager.LoadScene(loadscene);
    }

    void Update()
    {
        //if ESC or SPACE is pressed, skip to next scene(title menu)
        if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(loadscene);
        }
    }

    void FadeIn()
    {
        //goes from invisible(0.0f) to fully visible (1.0f) within 1.5 seconds
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        //goes from visible(1.0f) to invisible (0.0f) within 2.5 seconds
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
