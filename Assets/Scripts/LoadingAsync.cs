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
                loadingText.text = "Press 'Space' to Continue"; // prompts the user to press space in order
                loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    aSyncOp.allowSceneActivation = true;
                }
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
