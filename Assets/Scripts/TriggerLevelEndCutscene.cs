using UnityEngine;
using System.Collections;

public class TriggerLevelEndCutscene : MonoBehaviour
{

    public Transform target;
    private Collider2D _player;
    private bool _reachedDestination = false;
    public float speed = 10;
    private bool _wasTriggered = false;

    private GameObject mapIcon;


    // Use this for initialization
    void Start()
    {
        TractorBeamControls.partPickupDelegate += ActivateMapIcon;
        if(mapIcon = transform.FindChild("MapIcon").gameObject)
        {
            mapIcon.SetActive(false);
        }  
    }

    void OnDisable()
    {
        TractorBeamControls.partPickupDelegate -= ActivateMapIcon;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_reachedDestination && _player != null)
        {
            _player.SendMessage("cutSceneMovePlayer", speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !_wasTriggered)
        { 
            // win condition met if part collected
            if (other.GetComponent<TractorBeamControls>().partCollected)
            {
                target.GetComponent<EndCutScene>().SetEndpointActive();
                setReachedDestination(false);
                _player = other;
                _player.SendMessage("setPlayerDestination", (Vector2)target.position);
                _wasTriggered = true;
            }     
        }
    }
    
    public void setReachedDestination(bool hasReached)
    {
        _reachedDestination = hasReached;
        /*
        if (_player != null && _reachedDestination == true)
        {
            _player.SendMessage("setDisablePlayerControl", false);
        }
        */
    }

    void ActivateMapIcon()
    {
        if (mapIcon != null)
        {
            mapIcon.SetActive(true);
        }
    }
    
}
