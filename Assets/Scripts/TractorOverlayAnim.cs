using UnityEngine;
using System.Collections;

public class TractorOverlayAnim : MonoBehaviour {


    public Transform beamEnd;
    Vector2 uvOffset = Vector2.zero;
    LineRenderer rend;
    int materialIndex = 0;
    string textureName = "_MainTex";
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);


    void Start()
    {
        rend = GetComponent<LineRenderer>();
    }
   
    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (rend.enabled)
        {
            //scale texture to avoid stretching
            rend.material.SetTextureScale("_MainTex", new Vector2((beamEnd.position - transform.position).magnitude / 10, 1));
            //change offset of texture to animate
            rend.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
    }
}
