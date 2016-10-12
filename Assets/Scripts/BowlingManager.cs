using UnityEngine;
using System.Collections;

public class BowlingManager : MonoBehaviour {

    //ten pin objects
    Transform[] pinSpawners;
    Transform ballSpawner;
    public GameObject pinObject;
    public GameObject ballObject;

    //final score of the player so far
    int finalScore = 0;

    //the amount the player earns during each frame (2 throws)
    int frameScore = 0;

    //current frame (round)
    int currentFrame = 0;
    const int MAX_FRAMES = 10;

    // Use this for initialization
    void Start()
    {
        pinSpawners = GetComponentsInChildren<Transform>();
        //the second index is the ball spawner
        ballSpawner = pinSpawners[1];

        //start first frame
        StartFrame();
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    public void KnockDownPin()
    {
        //increase score by one for current frame
        frameScore++;
    }

    //runs at the beginning of each frame, resets pins and points counter for frame
    void StartFrame()
    {
        //increase frame count
        currentFrame++;
        //if all 10 frames have been finished
        if(currentFrame > MAX_FRAMES)
        {
            //end the game
            EndGame();
        }
        else
        {
            //reset frame score
            frameScore = 0;
            //spawn the ball
            SpawnBall();
            //spawn the pins
            SpawnPins();
        }
    }

    void SpawnBall()
    {
        //instantiate pin
        GameObject ball = Instantiate(ballObject, ballSpawner.position, ballSpawner.rotation) as GameObject;
    }

    void SpawnPins()
    {
        //spawn a pin at each spawner
        foreach(Transform spawner in pinSpawners)
        {
            //skip first one and 2nd one, thats the parent
            if (spawner != pinSpawners[0] && spawner != pinSpawners[1])
            {
                //Debug.Log(spawner.name);

                //instantiate pin
                GameObject pin = Instantiate(pinObject, spawner.position, spawner.rotation) as GameObject;
                //set the pins reference to bowling manager
                pin.GetComponent<Pin>().SetManager(this);
            }
        }
    }
    void EndGame()
    {
        //calculate score

        //display score

        //save score?

        //retry or exit
    }
}
