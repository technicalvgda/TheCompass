using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObjectiveMech : MonoBehaviour {

    //public GameObject transitionTextBox;

    //Put the name of the scene to jump to here
    public string nextLevelName;
    public LoadingTransition loadingTransition;
    public GameObject transitionBox;

    void Start()
    {
        loadingTransition = transitionBox.GetComponent<LoadingTransition>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
                loadingTransition.startCommentaryDialogue();
            if(loadingTransition.isActive)
            {
                loadingTransition.EnableTextBox();
            }
           /*
            // win condition met if part collected
            if (col.GetComponent<TractorBeamControls>().partCollected)
            {
                

               

                
                Debug.Log("COMPLETE,COMPLETE,COMPLETE");
                if (nextLevelName != null)
                {
                    SceneManager.LoadSceneAsync(nextLevelName);
                }
                else
                {
                    Debug.Log("No scene set for next level on waypoint");
                }
                

            }
        */
        }
        else if(col.tag == "TetheredPart")
        {
            Debug.Log("COMPLETE,COMPLETE,COMPLETE");
            if (nextLevelName != null)
            {
                SceneManager.LoadSceneAsync(nextLevelName);
            }
            else
            {
                Debug.Log("No scene set for next level on waypoint");
            }
        }

        else if (col.tag == "RepairStation")
        {
            Debug.Log("COMPLETE,COMPLETE,COMPLETE");
            if (nextLevelName != null)
            {
                SceneManager.LoadSceneAsync(nextLevelName);
            }
            else
            {
                Debug.Log("No scene set for next level on waypoint");
            }
        }

    }
}
