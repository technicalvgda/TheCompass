using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingAsync : MonoBehaviour {

    public GameObject widget;
    public Text loadingText;

    public string levelToLoad; // string for scene name
    
	// Use this for initialization
	void Start ()
    {
        loadingText.text = "Loading...";
        StartCoroutine(LoadLevelWithRealProgress(levelToLoad));        
	}
	
	// Update is called once per frame
	void Update ()
    {
 
	}

    IEnumerator LoadLevelWithRealProgress(string levelToLoad)
    {
        yield return new WaitForSeconds(1);

        AsyncOperation aSyncOp = SceneManager.LoadSceneAsync(levelToLoad);
        aSyncOp.allowSceneActivation = false;

        while(!aSyncOp.isDone)
        {
            

            if(aSyncOp.progress >= 0.9f)
            {
                widget.transform.Rotate(0,0,0);

#if UNITY_STANDALONE || UNITY_WEBPLAYER
                //if in the web player will prompt user to press spacebar
                loadingText.text = "Press any button to Continue"; // prompts the user to press space in order
                loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
				if (Input.anyKeyDown)
                {
                    aSyncOp.allowSceneActivation = true;
                }
#elif UNITY_IOS || UNITY_ANDROID
                //when built to mobile will prompt user to touch the screen to continue 
                loadingText.text = "Touch Screen to Continue"; // prompts the user to press space in order
                loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
				if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
                {
                    aSyncOp.allowSceneActivation = true;
                }
#endif

}
            else
            {
                widget.transform.Rotate(0,0,15); // while the scene is loading, the widget will rotate at a fixed rate of 15(could have aSyncOp.progress if loading was longer)
            }

            //Debug.Log(aSyncOp.progress);
            yield return null;
        }

    }
}
