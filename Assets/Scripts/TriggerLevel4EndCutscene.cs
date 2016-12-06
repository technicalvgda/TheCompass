using UnityEngine;
using System.Collections;

public class TriggerLevel4EndCutscene : MonoBehaviour
{

    private Transform target;
    //all targets along the cutscene path
    public Transform[] allTargets;
    public Transform powerSourceTarget;
    public float[] speedToTarget;
    private int targetIndex = 0;
    private GameObject _player;
    private GameObject upgradeSphere;
    //private bool _reachedDestination = false;
    private float speed = 10;
    private bool _wasTriggered = false;

    private GameObject mapIcon;
    private bool movePlayer = false;
    private float distToTarget;
    private const float arrivalDist = 0.5f;

    public float lookRotation = -90.0f;
    private Quaternion dockRotation;
    private Quaternion playerRotation;
    private float rotationSpeed = 0.1f;

    private bool reachedTarget = false;
    private float pauseTime = 5.0f;

    private LoadingTransition loadingTransition;
    public GameObject transitionBox;
    public GameObject commentaryObject;
    public GameObject commentaryBox;
    public TextBoxManager commentaryScript;

    private AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        commentaryScript = commentaryBox.GetComponent<TextBoxManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        audioSrc = GetComponent<AudioSource>();
        upgradeSphere = transform.FindChild("UpgradeSphere").gameObject;
        upgradeSphere.SetActive(false);
        //subscribe map icon function to part pickup event
        TractorBeamControls.partPickupDelegate += ActivateMapIcon;
        //get map icon components and set inactive at start
        if (mapIcon = transform.FindChild("MapIcon").gameObject)
        {
            mapIcon.SetActive(false);
        }
        if (transitionBox != null)
        {
            loadingTransition = transitionBox.GetComponent<LoadingTransition>();
        }
        target = allTargets[0];
        dockRotation = Quaternion.identity * Quaternion.Euler(0, 0, lookRotation);
    }

    void OnDisable()
    {
        TractorBeamControls.partPickupDelegate -= ActivateMapIcon;
    }
    // Update is called once per frame
    void Update()
    {
        if (movePlayer == true && _player != null)
        {
            distToTarget = Vector2.Distance(_player.transform.position, target.transform.position);
            //if player has reached point
            if (distToTarget <= arrivalDist)
            {
                _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (playerRotation == null)
                { playerRotation = _player.transform.rotation; }

                _player.transform.rotation = Quaternion.Lerp(playerRotation, dockRotation, Time.deltaTime * rotationSpeed);

                //rotate to proper facing direction
                if (_player.transform.rotation == dockRotation)
                {
                    movePlayer = false;
                    StartCoroutine(ReachedTarget());
                }
            }
            else
            {
                //move player to point
                _player.SendMessage("cutSceneMovePlayer", speed);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "TetheredPart" && other.GetComponent<TetheredObject>().tetherOn && !_wasTriggered)
        {
            //set triggered to true (to prevent multiple calls)
            _wasTriggered = true;
            GameObject tether = other.gameObject;
            tether.GetComponent<TetheredObject>().tetherOn = false;
            //move power source to proper location
            StartCoroutine(MovePowerToTarget(tether.transform));

            //disable collisions
            _player.GetComponent<CircleCollider2D>().enabled = false;
            //disable fuel loss
            _player.GetComponent<Player>().DisableFuelLoss();
            //disable camera follow and center camera
            //StartCoroutine(CenterCamera());
            BeginCutscene();
        }

    }

    void BeginCutscene()
    {
        //target.GetComponent<EndCutScene>().SetEndpointActive();
        //setReachedDestination(false);

        //set destination for player to go       
        _player.SendMessage("setPlayerDestination", (Vector2)target.position);
        speed = speedToTarget[targetIndex];

        //start player movement
        movePlayer = true;

    }

    IEnumerator MovePowerToTarget(Transform powerSource)
    {
        powerSource.position = powerSourceTarget.position;//TEMP CODE TO FIX STUCK CORE
        powerSource.GetComponent<SpriteRenderer>().sortingOrder = 0;
        /*
        Vector3 startPos = powerSource.position;
        while(Vector3.Distance(powerSource.position, powerSourceTarget.position) > 0.5)
        {
            powerSource.position = Vector3.Lerp(startPos, powerSourceTarget.position, Time.deltaTime*2);
            yield return new WaitForSeconds(0.05f);
        }
        */
        yield return null;
    }

    IEnumerator ReachedTarget()
    {

        targetIndex++;
        if (targetIndex < allTargets.Length)
        {
            //get next target

            /*
            //swap layer
            SpriteRenderer playerRend = _player.GetComponent<SpriteRenderer>();
            playerRend.sortingLayerName = "Parallax2";
            playerRend.sortingOrder = -2;

            //start shrinking player
            StartCoroutine(ShrinkPlayer());
            */
            //StartCoroutine(PlayAudio());

            commentaryObject.GetComponent<Level4EndDialogue>().ActivateCommentary();
            upgradeSphere.SetActive(true);
            while(commentaryScript.currentlyInCommentary)
            {
                yield return new WaitForSeconds(1f);
            }
            //yield return new WaitForSeconds(pauseTime);
            upgradeSphere.SetActive(false);

            target = allTargets[targetIndex];
            BeginCutscene();
           
        }
        else
        {
            //end level
            //show ending dialogue if this is an end trigger
            if (transitionBox != null)
            {
                loadingTransition.startCommentaryDialogue();
            }
        }
        yield return null;
    }

    IEnumerator ShrinkPlayer()
    {
        Vector3 shrinkVec = new Vector3(0.05f, 0.05f, 0);
        while (_player.transform.localScale.x > 0.1)
        {
            _player.transform.localScale -= shrinkVec;
            yield return new WaitForSeconds(1);
        }
        yield return null;

    }

    void ActivateMapIcon()
    {
        if (mapIcon != null)
        {
            mapIcon.SetActive(true);
        }
    }

    IEnumerator PlayAudio()
    {
        audioSrc.Play();
        yield return new WaitForSeconds(2.0f);
        while (audioSrc.volume > 0)
        {
            audioSrc.volume -= 0.1f;
            yield return new WaitForSeconds(0.3f);
        }
        audioSrc.Stop();
        yield return null;
    }
}
