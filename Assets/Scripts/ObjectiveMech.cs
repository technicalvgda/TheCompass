using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObjectiveMech : MonoBehaviour {

    //Put the name of the scene to jump to here
    public string nextLevelName;

    void OnTriggerEnter2D(Collider2D col)
    {
        // win condition met if part collected
        TractorBeamControls tbc = col.GetComponent<TractorBeamControls>();
        bool part = tbc.partCollected;

        if (part)
        {
            Debug.Log("COMPLETE,COMPLETE,COMPLETE");
            if(nextLevelName != null)
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
