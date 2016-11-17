using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {

    RectTransform playerArrow;
    Transform player;
    
    float playerXPos;
    float playerYPos;

    const float BORDER_DIST = 500f;
    float mapBorderDist;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerArrow = transform.FindChild("PlayerArrow").GetComponent<RectTransform>();
        mapBorderDist = GetComponent<RectTransform>().rect.width/2;
      
        Debug.Log(mapBorderDist);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CalculatePlayerPos();
        //playerArrow.localPosition = new Vector2(50, 50);
        //playerArrow.localPosition = new Vector2(playerXPos*mapBorderDist, playerYPos * mapBorderDist);
        playerArrow.anchoredPosition = new Vector2(playerXPos * mapBorderDist, playerYPos * mapBorderDist);
        playerArrow.rotation = player.rotation;
	}

    //gets value between -1 and 1 for each 
    void CalculatePlayerPos()
    {
        playerXPos = Mathf.Clamp(player.position.x / BORDER_DIST,-1,1);
        playerYPos = Mathf.Clamp(player.position.y / BORDER_DIST,-1,1);
        //Debug.Log("Player pos ratio: "+playerXPos+"/"+playerYPos);
    }
}
