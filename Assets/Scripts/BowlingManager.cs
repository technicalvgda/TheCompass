using UnityEngine;
using System.Collections;
/*
* Script to handle bowling minigame
* Game consists of 10 frames, 2 rounds per frame
* Hitting all pins on first throw is a strike (10pts + next 2 throws)
* Hitting all pins on second throw is a spare (10pts + next throw)
* If a strike is bowled on 10th round, player gets 2 additional throws
* If a spare is bowled on 10th round, player gets 1 additional throw
*/

public class BowlingManager : MonoBehaviour {

    //event declaration for spawning pins
    public delegate void SpawnAction(GameObject obj);
    public static event SpawnAction SpawnPin;

    //event declaration for cleaning up pins
    public delegate void CleanAction();
    public static event CleanAction DestroyPin;



    public Transform ballSpawner;
    public GameObject pinObject;
    public GameObject ballObject;

    //final score of the player so far
    int finalScore = 0;

    //the amount the player earns during each frame (2 throws)
    //12 frames for 10 frames + 2 bonus rounds
    int[] frameScore = new int[12] {0,0,0,0,0,0,0,0,0,0,0,0};

    //if a particular frame was a strike
    bool[] frameStrike = new bool[12] { false, false, false, false, false, false, false, false, false, false, false, false };



    //checks if this frame is on its second round
    bool round2 = false;

    //current frame (round)
    int currentFrame = 0;
    const int MAX_FRAMES = 10;
    //number of bonus throws for the player (increased if a strike on 10th round)
    int bonusThrows = 0;

    //subscribe BallDrop function to LostBall Event
    //When a ball hits a gutter, calls BallDrop() to bring it back
    void OnEnable()
    {Gutter.LostBall += BallDrop; Gutter.HitPin += IncreaseScore;}
    void OnDisable()
    {Gutter.LostBall -= BallDrop; Gutter.HitPin += IncreaseScore;}

    // Use this for initialization
    void Start()
    {
        //start first frame
        StartFrame();
    }



    //runs at the beginning of each frame, resets pins and points counter for frame
    void StartFrame()
    {
        //indicate that this frame is on its first round
        round2 = false;


        //if all 10 frames have been finished
        if(currentFrame > MAX_FRAMES && bonusThrows == 0)
        {
            //end the game
            EndGame();
        }
        else
        {
            Debug.Log("Frame "+ (currentFrame + 1)+" begin!");
            //reset frame score
            frameScore[currentFrame] = 0;

            //spawn the ball
            StartCoroutine(RespawnBall());
            //spawn the pins
            StartCoroutine(RespawnPins());
            //output current scores
            OutputScore();
        }
    }
    //start second round of current frame
    void Round2()
    {
        //indicate that this frame has started its second round
        round2 = true;

        Debug.Log("Frame " + currentFrame + " round 2!");
       
        //spawn the ball
        StartCoroutine(RespawnBall());
       
        //output current scores
        OutputScore();
    }

    void EndFrame()
    {
        //calculate final score so far
        ScoreHandler();

        //if anything is subscribed to DestroyPin
        if (DestroyPin != null)
        {
            //clean up remaining pins by calling DestroyPin Event
            DestroyPin();
        }


        //increase frame count
        currentFrame++;

        //start next frame
        StartFrame();
    }

    
    //handles proper behavior when a ball is destroyed
    //Gutterball or ball lands in back pit
    void BallDrop()
    {
        StartCoroutine(EndRound());
    }

    IEnumerator EndRound()
    {
        yield return new WaitForSeconds(3f);
        //if this was the first round
        if (!round2)
        {
            //check if a strike was made
            if (frameScore[currentFrame] == 10)
            {
                Debug.Log("STRIKE!!!!!!!!!!!!!!!");
                //indicate that a strike occured on this frame
                frameStrike[currentFrame] = true;
                //if this is the 10th frame
                if (currentFrame == 9)
                {
                    bonusThrows += 2;
                }
                //End this frame
                EndFrame();
            }
            else
            {
                Debug.Log("You hit " + frameScore[currentFrame] + " pins!");
                //start round 2
                Round2();
            }

        }
        else //if this was the second round
        {
            //check if a spare was made
            if (frameScore[currentFrame] == 10)
            {
                Debug.Log("SPARE!");
                //if this is the 10th frame
                if (currentFrame == 9)
                {
                    bonusThrows += 1;
                }
            }
            else
            {
                Debug.Log("You hit " + frameScore[currentFrame] + " pins!");
            }
            //End this frame
            EndFrame();
        }
    }

    void ScoreHandler()
    {
        //CALCULATE FINAL SCORING
        //check for strike or spare last frame (if this isnt the first frame)
        if (currentFrame > 0 && frameScore[currentFrame-1] == 10)
        {
            frameScore[currentFrame - 1] += frameScore[currentFrame];     
        }

        //check for strike 2 frames ago (if this isnt the first or second frame)
        if (currentFrame > 1 && frameStrike[currentFrame - 2] == true)
        {
            frameScore[currentFrame - 2] += frameScore[currentFrame];    
        }

        //if this was a strike
        if(frameStrike[currentFrame] == true)
        {
            //start at 1 since a strike just occurred
            int strikeCounter = 1;
            //count back from the current frame to see how many strikes occured in sequence
            for(int i = currentFrame-1; i >= 0; i-- )
            {
                //if the frame being checked was a strike
                if(frameStrike[i] == true)
                {
                    strikeCounter++;
                }
                else
                {
                    break;
                }
            }

            switch(strikeCounter)
            {
                case 2:
                    Debug.Log("Double!!!");
                    break;
                case 3:
                    Debug.Log("Turkey!!!");
                    break;
                case 6:
                    Debug.Log("Wild Turkey!!!");
                    break;
                case 9:
                    Debug.Log("Gold Turkey!!!");
                    break;
                case 12:
                    Debug.Log("PERFECT GAME!!!");
                    break;
                default:
                    Debug.Log("strike counter is at: "+strikeCounter);
                    break;
            }

           
        }

        //calculate current final score
        //add all scores together
        for (int i = 0; i <= currentFrame; i++)
        {
            finalScore += frameScore[i];
        }
    }

    //increases the player score
    void IncreaseScore()
    {
        //increase the frame score by one
        frameScore[currentFrame]++;
    }


    IEnumerator RespawnBall()
    {
        yield return new WaitForSeconds(2f);
        //if the ball object has been assigned
        if (ballObject != null)
        {
            //instantiate pin
            GameObject ball = Instantiate(ballObject, ballSpawner.position, ballSpawner.rotation) as GameObject;
        }
        yield return null;
    }

    IEnumerator RespawnPins()
    {
        //wait 2 seconds
        yield return new WaitForSeconds(2f);
        //if anything is subscribed to SpawnPins
        if (SpawnPin != null)
        {
            //spawn pins using the pin object
            SpawnPin(pinObject);
        }
        yield return null;
    }

    void OutputScore()
    {
        Debug.Log("Frame Score: "+ frameScore[currentFrame]);
    }
    void EndGame()
    {
        //calculate score

        //display score

        //save score?

        //retry or exit
    }
}
