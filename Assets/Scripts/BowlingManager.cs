using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
* Script to handle bowling minigame
* Game consists of 10 frames, 2 rounds per frame
* Hitting all pins on first throw is a strike (10pts + next 2 throws)
* Hitting all pins on second throw is a spare (10pts + next throw)
* If a strike is bowled on 10th round, player gets 2 additional throws
* If a spare is bowled on 10th round, player gets 1 additional throw
*/

public class BowlingManager : MonoBehaviour {

    GameObject explosion;//< spawn effect for ball dispenser
    ///TEXT OBJECTS
    public Text textBox;
    public RectTransform _rectTransform;
    public bool isActive = false;
    private float movementSpeed = 10f;

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
    //10 frames
    int[] frameScore = new int[10] {0,0,0,0,0,0,0,0,0,0};

    //the amount earned in each round
    int[] round1Score = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] round2Score= new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

    //if a particular frame was a strike
    //bool[] frameStrike = new bool[12] { false, false, false, false, false, false, false, false, false, false, false, false };



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
        explosion = Resources.Load("Explosion") as GameObject;
        StartCoroutine(StartCommentary());
        StartCoroutine(StartGame());
        //start first frame
        //StartFrame();
    }

    IEnumerator StartGame()
    {
        setText(OutputScoreBox("Welcome to Bowling!"));
        yield return new WaitForSeconds(3f);
        StartFrame();
        yield return null;
    }

    //runs at the beginning of each frame, resets pins and points counter for frame
    void StartFrame()
    {
        //indicate that this frame is on its first round
        round2 = false;

        //if all 10 frames have been finished
        if(currentFrame > MAX_FRAMES)
        {
            //if bonus rounds are available
            if (bonusThrows > 0)
            {
                //handle bonus rounds
                StartFrame();
            }
            else //if there are no bonus rounds
            {
                //end the game
                EndGame();
            }         
        }
        else
        {

            Debug.Log("Frame "+ (currentFrame + 1)+" begin!");
            setText(OutputScoreBox("Frame " + (currentFrame + 1) + " begin!"));
            //spawn the ball
            StartCoroutine(RespawnBall());
            //spawn the pins
            StartCoroutine(RespawnPins());
           
        }
    }
    //start second round of current frame
    void Round2()
    {
        //indicate that this frame has started its second round
        round2 = true;

        Debug.Log("Frame " + (currentFrame+1) + " round 2!");
        setText(OutputScoreBox("Frame " + (currentFrame + 1) + " round 2!"));
        //spawn the ball
        StartCoroutine(RespawnBall());
        
    }


    //Handles all functions for ending a frame
    void EndFrame()
    {
        //calculate final score so far
        ScoreHandler();

        //if anything is subscribed to DestroyPin
        //clean up remaining pins by calling DestroyPin Event
        if (DestroyPin != null)
        { DestroyPin();}
            
        //Advance counter to next frame
        currentFrame++;
        //reduce bonus throws if any
        if(bonusThrows > 0)
        {
            bonusThrows--;
        }
        //start next frame
        StartFrame();
    }

    
    //handles proper behavior when a ball is destroyed:
    //Gutterball or ball lands in back pit
    void BallDrop()
    {
       
        //End the round
        StartCoroutine(EndRound());
    }

    public void GutterBall()
    {
        setText(OutputScoreBox("Gutterball (>_<)"));
    }
    //Handles the end of a round
    //checks whether to continue to round 2 or end frame
    IEnumerator EndRound()
    {
        //wait for remaining pins to fall
        yield return new WaitForSeconds(3f);

        //if this is round one or a bonus round
        if(!round2 || bonusThrows > 0)
        {
            //if this was a bonus throw
            if(bonusThrows > 0)
            {
                //add to the final frame
                frameScore[9] += round1Score[currentFrame];
            }
            else
            {
                //add the score for round one of this frame to the total frame score
                frameScore[currentFrame] += round1Score[currentFrame];
            }

            //output number of pins hit
            Debug.Log("You hit " + round1Score[currentFrame] + " pins!");
            setText(OutputScoreBox("You hit " + round1Score[currentFrame] + " pins!"));
            //if the total is at 10, its a STRIKE!
            if (frameScore[currentFrame] == 10)
            {
                //if this is the 10th (final) frame
                if (currentFrame == 9)
                {
                    bonusThrows += 2;
                }
                //End this frame, there are no remaining pins
                EndFrame();
            }
            else
            {
                if(bonusThrows > 0)
                {
                    //End this frame, bonus rounds only give you one throw
                    EndFrame();
                }
                else
                {
                    //start round 2
                    Round2();
                }
                
            }
        }
        else
        {
            //add the score for round two of this frame to the total frame score
            //round 1 will already have been added
            frameScore[currentFrame] += round2Score[currentFrame];
            //output number of pins hit
            Debug.Log("You hit " + round2Score[currentFrame] + " pins!");        
            //if the total is at 10, its a spare
            if (frameScore[currentFrame] == 10)
            {
                Debug.Log("SPARE!");
                setText(OutputScoreBox("SPARE!"));
                //if this is the 10th (final) frame
                if (currentFrame == 9)
                {
                    bonusThrows += 1;
                }
            }
            else
            {
                setText(OutputScoreBox("You hit " + round2Score[currentFrame] + " pins!"));
            }
            //End this frame
            EndFrame();

        }
       

    }

      
    void ScoreHandler()
    {
        //CALCULATE FINAL SCORING

        //check for strike 2 frames ago (if this isnt the first or second frame)
        if (currentFrame > 1 && round1Score[currentFrame - 2] == 10)
        {
            frameScore[currentFrame - 2] += frameScore[currentFrame];
        }

        //check for strike or spare last frame (if the last frame had a total score of 10)
        //(if this isnt the first frame or 2nd bonus round)
        if (currentFrame > 0 && currentFrame < 11 && frameScore[currentFrame-1] == 10)
        {
            frameScore[currentFrame - 1] += frameScore[currentFrame];     
        }

        //if this frame was a strike
        if(!round2)
        {
            //start at 1 since a strike just occurred
            int strikeCounter = 1;
            //count back from the current frame to see how many strikes occured in sequence
            for(int i = currentFrame-1; i >= 0; i-- )
            {
                //if the frame being checked was a strike
                if(round1Score[i] == 10)
                {
                    //increase the strike counter
                    strikeCounter++;
                }
                else
                {break;}
            }

            //output the proper strike term based on streak
            switch(strikeCounter)
            {
                case 1:
                    Debug.Log("Strike!!!");
                    setText(OutputScoreBox("Strike!!!"));
                    break;
                case 2:
                    Debug.Log("Double!!!");
                    setText("Double!!!");
                    break;
                case 3:
                    Debug.Log("Turkey!!!");
                    setText("Turkey!!!");
                    break;
                case 6:
                    Debug.Log("Wild Turkey!!!");
                    setText("Wild Turkey!!!");
                    break;
                case 9:
                    Debug.Log("Gold Turkey!!!");
                    setText("Gold Turkey!!!");
                    break;
                case 12:
                    Debug.Log("PERFECT GAME!!!");
                    setText("PERFECT GAME!!!");
                    break;
                default:
                    Debug.Log("strike counter is at: "+strikeCounter);
                    setText("strike counter is at: " + strikeCounter);
                    break;
            }

           
        }

        //DEBUG CODE!!!
        //calculate current final score
        //add all scores together
        finalScore = 0;
        for (int i = 0; i < MAX_FRAMES; i++)
        {
            //if this is the last frame
            if (i == MAX_FRAMES - 1)
            {
                //tack on the last 2 round1 scores (amount for bonus rounds)
                Debug.Log("Frame " + (i + 1) + " rounds: "
                    + round1Score[i] + "/" + round2Score[i] + "/" +round1Score[10] + "/" + round1Score[11]
                    + ". Total Frame score: " + frameScore[i]);
                setText("Frame " + (i + 1) + " rounds: "
                    + round1Score[i] + "/" + round2Score[i] + "/" + round1Score[10] + "/" + round1Score[11]
                    + ". Total Frame score: " + frameScore[i]);
                finalScore += frameScore[i];
                
            }
            else
            {
                Debug.Log("Frame " + (i + 1) + " rounds: "
                    + round1Score[i] + "/" + round2Score[i] + ". Total Frame score: " + frameScore[i]);
                setText("Frame " + (i + 1) + " rounds: "
                    + round1Score[i] + "/" + round2Score[i] + ". Total Frame score: " + frameScore[i]);
                finalScore += frameScore[i];
            }
        }
        setText("Score for this frame: " + frameScore[currentFrame]+"\n" + "Current total score: " + finalScore);
        Debug.Log("Score for this frame: "+frameScore[currentFrame]);
        Debug.Log("Current total score: "+finalScore);
    }

    //increases the player score
    //adds points to the proper round at the index for the frame
    void IncreaseScore()
    {
        //increase the round score by one
        if (!round2)
        {
            round1Score[currentFrame]++;
        }
        else
        {
            round2Score[currentFrame]++;
        }   
    }


    IEnumerator RespawnBall()
    {
        yield return new WaitForSeconds(2f);
        //if the ball object has been assigned
        if (ballObject != null)
        {
            Instantiate(explosion, ballSpawner.position, Quaternion.identity);
            //instantiate ball
            //GameObject ball = Instantiate(ballObject, ballSpawner.position, ballSpawner.rotation) as GameObject;
            Instantiate(ballObject, ballSpawner.position, ballSpawner.rotation);
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

   
    void EndGame()
    {
        Debug.Log("GAME COMPLETE");
        //calculate final score
        //add all scores together
        for (int i = 0; i <= MAX_FRAMES; i++)
        {
            Debug.Log("Frame 1: "+ frameScore[i]);
            setText("Frame 1: " + frameScore[i]+"\n");
            finalScore += frameScore[i];
        }

        //display score
        Debug.Log("Final Score: " + finalScore);
        setText("Final Score: " + finalScore);
        //save score?

        //retry or exit
    }

    public void setText(string text)
    {
        textBox.text = text;
    }

    IEnumerator StartCommentary()
    {
        //get stop position
        Vector2 _newPos = new Vector2(_rectTransform.anchoredPosition.x, -15f);
        //move the box up
        while (_rectTransform.anchoredPosition.y < -16f)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _newPos, Time.deltaTime * movementSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        //enable the commentary
        isActive = true;

        //EnableTextBox();

        //wait while text is still active
        while (isActive == true)
        {
            yield return new WaitForSeconds(0.01f);
        }
        //get position off screen
        _newPos = new Vector2(_rectTransform.anchoredPosition.x, -145f);
        //move the box back down
        while (_rectTransform.anchoredPosition.y > -144f)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _newPos, Time.deltaTime * movementSpeed);
            yield return new WaitForSeconds(0.01f);
        }
    }

    //returns ASCII designed score box
    string OutputScoreBox(string announcement)
    {
        return ("@@@ "+announcement+" @@@\n\n"
                +" __1__ __2__ __3__ __4__ __5__ __6__ __7__ __8__ __9__ ___10____\n"
                +GetRoundScores(1)+GetRoundScores(2) + GetRoundScores(3) + GetRoundScores(4) + GetRoundScores(5)
                        +GetRoundScores(6)+GetRoundScores(7)+GetRoundScores(8)+GetRoundScores(9) + GetRoundScores(10)+"|\n"
                +"|_"+GetFrameScore(1)+"_|_" + GetFrameScore(2) + "_|_" + GetFrameScore(3) + "_|_" + GetFrameScore(4) + "_|_" + GetFrameScore(5) + "_|_" + GetFrameScore(6)
                + "_|_" + GetFrameScore(7) + "_|_" + GetFrameScore(8) + "_|_" + GetFrameScore(9) + "_|_" + GetFrameScore(10)+ "_|");
    }
    string GetRoundScores(int frame)
    {
        int frameIndex = frame - 1;
        string scoreString = "";
        if(frame == 10)
        {
            //if strike, get all 3 frames
            if(round1Score[frameIndex] == 10)
            {
                scoreString += "| [ X ]";
                if (round1Score[frameIndex + 1] == 10)
                { scoreString += "[ X ]"; }
                else { scoreString += "[ " + frameIndex + 1 + " ]"; }
                if(round1Score[frameIndex + 2] == 10)
                { scoreString += "[ X ]"; }
                else { scoreString += "[ "+frameIndex + 2+" ]"; }
            }
            //if spare, do 2 frames
            else if ((round1Score[frameIndex] + round2Score[frameIndex]) == 10)
            {
                scoreString += "| [ "+round1Score[frameIndex]+" ][ / ]";
                if (round1Score[frameIndex + 1] == 10)
                { scoreString += "[ X ]"; }
                else { scoreString += "[ " + frameIndex + 1 + " ]"; }
               
            }
            else
            {
                scoreString += ("| [ "+round1Score[frameIndex]+ " ][ " + round2Score[frameIndex] + " ][   ]");
            }
            return scoreString;
           
        }
        //strike
        else if(round1Score[frameIndex] == 10)
        {
            return ("|   [ X ]");
        }
        else if((round1Score[frameIndex] + round2Score[frameIndex]) == 10)
        {
            return ("| "+round1Score[frameIndex]+" [ / ]");
        }
        else
        {
            return ("| "+round1Score[frameIndex] + " [ "+ round2Score[frameIndex]+" ]");
        }
       
    }
    //add on padding to score 
    string GetFrameScore(int frame)
    {
        string tenthPadding = "";
        if(frame == 10)
        {
            tenthPadding = "____";
        }
        string frameScoreRough = frameScore[frame - 1].ToString();
        if(frameScoreRough.Length == 1)
        {
            return tenthPadding+"00" + frameScoreRough;
        }
        else if(frameScoreRough.Length == 2)
        {
            return tenthPadding + "0" + frameScoreRough;
        }
        else
        {
            return tenthPadding + frameScoreRough;
        }
    }
}
