using UnityEngine;
using System.Collections;

public class GasTrailHandler : MonoBehaviour {

    public Transform GasGiant;
    public Transform BlackHole;

    public GameObject GasTrailObj;

    //linerender attached to this object
    LineRenderer lineRndr;
   
    Vector2 Heading;
    float distance;
    //amount to move particle emission from center of gas giant
    float offset;
    //amount to adjust emitter to match border of gas giant
    float adjustment = 15;

    //variables for texture animation
    //public Transform beamEnd;
    Vector2 uvOffset = Vector2.zero;
    int materialIndex = 0;
    string textureName = "_MainTex";
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);

    // Use this for initialization
    void Start ()
    {
        lineRndr = GetComponent<LineRenderer>();
        lineRndr.sortingLayerName = "Parallax2";

        //gets radius of gas giant, assumes width and height are the same
        offset = GasGiant.GetComponent<SpriteRenderer>().bounds.size.x / 2 - adjustment;
        StartCoroutine(SpawnTrail());
    }
	
	// Update is called once per frame
	void Update ()
    {
        //get the heading from gas giant to black hole
        Heading = BlackHole.position - GasGiant.position;
        distance = Vector2.Distance(BlackHole.position, GasGiant.position);
       

        lineRndr.SetPosition(0, transform.position);
        lineRndr.SetPosition(1, BlackHole.position);
        SetRotationOfTrail();
    }

    //handles texture offset animation
    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (lineRndr.enabled)
        {
            //scale texture to avoid stretching
            lineRndr.material.SetTextureScale("_MainTex", new Vector2((BlackHole.position - transform.position).magnitude / 10, 1));
            //change offset of texture to animate
            lineRndr.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
    }

    //Gets proper direction of trail
    void SetRotationOfTrail()
    {
       
       
        //face gas trail towards vector
        transform.rotation = Quaternion.LookRotation(-Heading);
        //offset position
        // = GasGiantPosition + (unitvector in proper direction * offsetMagnitude)
        transform.position = GasGiant.position + ((Vector3)Heading.normalized * offset);
    }


    public IEnumerator SpawnTrail()
    {
        yield return new WaitForSeconds(2f);

        GameObject trail =  Instantiate(GasTrailObj, GasGiant.transform.position, GasGiant.transform.rotation) as GameObject;
        trail.GetComponent<TrailRenderer>().sortingLayerName = "Background";
        //trail.transform.parent = this.transform;
        //trail.GetComponent<GasTrailMovement>().SetBlackHole(BlackHole.gameObject);
        StartCoroutine(SpawnTrail());

        yield return null;
    }
    
}
