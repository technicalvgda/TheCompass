using UnityEngine;
using System.Collections;

public class ParallaxHandlerScript : MonoBehaviour {

    //1 is closest layer, 3 is furthest layer (background panel)
    public GameObject layer1, layer2, layer3;

    //Transform[] layer1Children;
    //Transform[] layer2Children;
    //Transform[] layer3Children;

    //unit to measure size of map by (5 is value used by editor grid for 1 cell)
    public float unit = 5.0f;

    //dimensions of level in units (same units as above)
    public float levelSizeX = 200.0f;
    public float levelSizeY = 200.0f;

    Camera mainCamera;
    GameObject player;
    Vector2 playerPos;

    //variables for layer math
    //can add position and half width or height to get edge pos of layer
    private float halfWidth1, halfWidth2, halfWidth3;
    private float halfHeight1, halfHeight2, halfHeight3;

    float maxX1, maxX2, maxX3;
    float maxY1, maxY2, maxY3;


    Vector3 bottomLeft;
    Vector3 bottomRight;
    Vector3 topLeft;
    Vector3 topRight;
    /*
    //draw lines to show map size
    void OnDrawGizmos()
    {
      
            Gizmos.DrawLine(new Vector3(-mapSizeX, y, 0.0f),
                            new Vector3(mapSizeX, y, 0.0f));
      
            Gizmos.DrawLine(new Vector3(x, -mapSizeY, 0.0f),
                            new Vector3(x, mapSizeY, 0.0f));
        

    }
    */
    // Use this for initialization
    void Start ()
    {

        bottomLeft = new Vector3(-levelSizeX / 2 * unit, -levelSizeY / 2 * unit, 0);
        bottomRight = new Vector3(levelSizeX / 2 * unit, -levelSizeY / 2 * unit, 0);
        topLeft = new Vector3(-levelSizeX / 2 * unit, levelSizeY / 2 * unit, 0);
        topRight = new Vector3(levelSizeX / 2 * unit, levelSizeY / 2 * unit, 0);
        //draw lines for map borders
        Debug.DrawLine(bottomLeft, bottomRight,Color.green);//< bottom line
        Debug.DrawLine(topLeft, topRight, Color.green);//< top line
        Debug.DrawLine(bottomLeft, topLeft, Color.green);//< left line
        Debug.DrawLine(bottomRight, topRight, Color.green);//< right line


        //assign camera and player variable
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");

        //layer1Children = new Transform[layer1.transform.childCount];
        //layer2Children = new Transform[layer2.transform.childCount];
        //layer3Children = new Transform[layer3.transform.childCount];

       //set sorting layers of all children to each layer
        foreach (Transform child in layer1.transform)
        {child.GetComponent<SpriteRenderer>().sortingLayerName = "Parallax1";}

        foreach (Transform child in layer2.transform)
        { child.GetComponent<SpriteRenderer>().sortingLayerName = "Parallax2"; }

        foreach (Transform child in layer3.transform)
        { child.GetComponent<SpriteRenderer>().sortingLayerName = "Background"; }

        //set layer math variables
        halfWidth1 = layer1.GetComponent<RectTransform>().rect.width / 2;
        halfWidth2 = layer2.GetComponent<RectTransform>().rect.width / 2;
        halfWidth3 = layer3.GetComponent<RectTransform>().rect.width / 2;
        halfHeight1 = layer1.GetComponent<RectTransform>().rect.height / 2;
        halfHeight2 = layer2.GetComponent<RectTransform>().rect.height / 2;
        halfHeight3 = layer3.GetComponent<RectTransform>().rect.height / 2;

        
    }
	
	// Update is called once per frame
	void Update ()
    {

        //draw lines for map borders
        Debug.DrawLine(bottomLeft, bottomRight, Color.green);//< bottom line
        Debug.DrawLine(topLeft, topRight, Color.green);//< top line
        Debug.DrawLine(bottomLeft, topLeft, Color.green);//< left line
        Debug.DrawLine(bottomRight, topRight, Color.green);//< right line

        playerPos = player.transform.position;

        //handle shifting of all 3 layers
        HandleLayerMovement(layer1.transform, halfWidth1, halfHeight1, maxX1, maxY1);
        HandleLayerMovement(layer2.transform, halfWidth2, halfHeight2, maxX2, maxY2);
        HandleLayerMovement(layer3.transform, halfWidth3, halfHeight3, maxX3, maxY3);
       
    }

    void HandleLayerMovement(Transform currentLayer, float halfWidth, float halfHeight,
                             float maxX, float maxY)
    {
        float width = currentLayer.GetComponent<RectTransform>().rect.width;
        float height = currentLayer.GetComponent<RectTransform>().rect.height;

        float shiftRatioX = halfWidth / (levelSizeX * unit);  //0.2f;
        float shiftRatioY = halfHeight / (levelSizeY * unit);//0.2f;
        //needs to shift by a certain degree so that it starts centered
        //and shifts just enough to reach its edge at the edge of the map
        currentLayer.position = mainCamera.transform.position + new Vector3
                                (-mainCamera.transform.position.x * shiftRatioX, 
                                 -mainCamera.transform.position.y * shiftRatioY, 
                                  transform.position.z);

        

    }
}
