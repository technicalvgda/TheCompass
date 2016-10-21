using UnityEngine;
using System.Collections;
//using UnityEditor;

public class ParallaxHandlerScript : MonoBehaviour
{


    public bool isTractored = false;

    //1 is closest layer, 3 is furthest layer (background panel)
    public GameObject layer1, layer2, layer3;

    //unit to measure size of map by (5 is value used by editor grid for 1 cell)
    public float unit = 5.0f;

    //dimensions of level in units (same units as above)
    public float levelSizeX = 200.0f;
    public float levelSizeY = 200.0f;

    Vector3 centerPos;

    Camera mainCamera;
    float cameraHalfWidth;
    float cameraHalfHeight;

    //variables for layer math
    //can add position and half width or height to get edge pos of layer
    private float width1, width2, width3;
    private float height1, height2, height3;

    private float halfWidth1, halfWidth2, halfWidth3;
    private float halfHeight1, halfHeight2, halfHeight3;

    Vector3 bottomLeft;
    Vector3 bottomRight;
    Vector3 topLeft;
    Vector3 topRight;
   
    // Use this for initialization
    void Start ()
    {
       


        //assign camera and player variable
        mainCamera = Camera.main;
        centerPos = mainCamera.transform.position;
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height ;
        

        //set sorting layers of all children to each layer
        foreach (Transform child in layer1.transform)
        {child.GetComponent<SpriteRenderer>().sortingLayerName = "Parallax1";}

        foreach (Transform child in layer2.transform)
        { child.GetComponent<SpriteRenderer>().sortingLayerName = "Parallax2"; }

        foreach (Transform child in layer3.transform)
        { child.GetComponent<SpriteRenderer>().sortingLayerName = "Background"; }

        //set layer math variables
        width1 = layer1.GetComponent<RectTransform>().rect.width;
        width2 = layer2.GetComponent<RectTransform>().rect.width;
        width3 = layer3.GetComponent<RectTransform>().rect.width;
        height1 = layer1.GetComponent<RectTransform>().rect.height;
        height2 = layer2.GetComponent<RectTransform>().rect.height;
        height3 = layer3.GetComponent<RectTransform>().rect.height;

        halfWidth1 = width1 / 2;
        halfWidth2 = width2 / 2;
        halfWidth3 = width3 / 2;
        halfHeight1 = height1 / 2;
        halfHeight2 = height2 / 2;
        halfHeight3 = height3 / 2;

        /*
        //draw box for level border
        StartCoroutine(DrawDebugBox(Color.green, levelSizeX * unit, levelSizeY * unit, null));//Vector3.zero);

        //draw box for layer 1
        StartCoroutine(DrawDebugBox(Color.yellow, width1, height1, layer1));//currentLayer.position);
        //draw box for layer 2
        StartCoroutine(DrawDebugBox(Color.blue, width2, height2, layer2));//currentLayer.position);
        //draw box for layer 3
        StartCoroutine(DrawDebugBox(Color.red, width3, height3, layer3)); //currentLayer.position);
        */


    }
	
	// Update is called once per frame
	void Update ()
    {
        


        //handle shifting of all 3 layers
        HandleLayerMovement(layer1.transform, halfWidth1, halfHeight1);
        HandleLayerMovement(layer2.transform, halfWidth2, halfHeight2);
        HandleLayerMovement(layer3.transform, halfWidth3, halfHeight3);
       
    }

    void HandleLayerMovement(Transform currentLayer, float halfWidth, float halfHeight)
    {

        float shiftRatioX = 1  - (((levelSizeX * unit / 2) - mainCamera.transform.position.x) / (levelSizeX * unit / 2) - centerPos.x);
        float shiftRatioY = 1 - (((levelSizeY * unit / 2) - mainCamera.transform.position.y) / (levelSizeY * unit / 2) - centerPos.y);
        //needs to shift by a certain degree so that it starts centered
        //and shifts just enough to reach its edge at the edge of the map

      
        //prevent layer from shifting off screen
        if(shiftRatioX > 1)
        {shiftRatioX = 1;}
        if (shiftRatioX < -1)
        { shiftRatioX = -1; }
        if (shiftRatioY > 1)
        { shiftRatioY = 1; }
        if (shiftRatioY < -1)
        { shiftRatioY = -1; }
        
        //move the layer to the position of camera with offset
        float layerXPos = mainCamera.transform.position.x - ((halfWidth- cameraHalfWidth)* shiftRatioX);
        float layerYPos = mainCamera.transform.position.y - ((halfHeight -cameraHalfHeight)* shiftRatioY);

        currentLayer.position = new Vector2(layerXPos,layerYPos);


    }

    /*
    IEnumerator DrawDebugBox(Color color, float xSize, float ySize, GameObject layer)//Vector3 shift)
    {
        Vector3 shift = Vector3.zero;
        if (layer != null)
        {
            shift = layer.transform.position;
        }
        

        //Get corner positions
        bottomLeft = new Vector3(-xSize / 2, -ySize / 2, 0) + shift;
        bottomRight = new Vector3(xSize / 2, -ySize / 2, 0) + shift;
        topLeft = new Vector3(-xSize / 2, ySize / 2, 0) + shift;
        topRight = new Vector3(xSize / 2, ySize / 2 , 0) + shift;
        
        //draw lines for map borders
        Debug.DrawLine(bottomLeft, bottomRight, color);//< bottom line
        Debug.DrawLine(topLeft, topRight, color);//< top line
        Debug.DrawLine(bottomLeft, topLeft, color);//< left line
        Debug.DrawLine(bottomRight, topRight, color);//< right line

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(DrawDebugBox(color, xSize, ySize, layer));
        yield return null;

    }
    */
    void OnDrawGizmos()
    {
        Vector3 shift = Vector3.zero;
        //DRAW MAP BORDER
        Gizmos.color = Color.green;
        //Get corner positions of map
        float xSize = levelSizeX * unit;
        float ySize = levelSizeY * unit;
        bottomLeft = new Vector3(-xSize / 2, -ySize / 2, 0);
        bottomRight = new Vector3(xSize / 2, -ySize / 2, 0);
        topLeft = new Vector3(-xSize / 2, ySize / 2, 0);
        topRight = new Vector3(xSize / 2, ySize / 2, 0);
        //draw lines for map borders
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(bottomRight, topRight);

        //DRAW LAYER 1
        Gizmos.color = Color.yellow;
        shift = layer1.transform.position;
        //Get corner positions of map
        xSize = layer1.GetComponent<RectTransform>().rect.width;
        ySize = layer1.GetComponent<RectTransform>().rect.height;
        bottomLeft = new Vector3(-xSize / 2, -ySize / 2, 0)+shift;
        bottomRight = new Vector3(xSize / 2, -ySize / 2, 0) + shift;
        topLeft = new Vector3(-xSize / 2, ySize / 2, 0) + shift;
        topRight = new Vector3(xSize / 2, ySize / 2, 0) + shift;
        //draw lines for map borders
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(bottomRight, topRight);

        //DRAW LAYER 2
        Gizmos.color = Color.blue;
        shift = layer2.transform.position;
        //Get corner positions of map
        xSize = layer2.GetComponent<RectTransform>().rect.width;
        ySize = layer2.GetComponent<RectTransform>().rect.height;
        bottomLeft = new Vector3(-xSize / 2, -ySize / 2, 0) + shift;
        bottomRight = new Vector3(xSize / 2, -ySize / 2, 0) + shift;
        topLeft = new Vector3(-xSize / 2, ySize / 2, 0) + shift;
        topRight = new Vector3(xSize / 2, ySize / 2, 0) + shift;
        //draw lines for map borders
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(bottomRight, topRight);

        //DRAW LAYER 3
        Gizmos.color = Color.red;
        shift = layer3.transform.position;
        //Get corner positions of map
        xSize = layer3.GetComponent<RectTransform>().rect.width;
        ySize = layer3.GetComponent<RectTransform>().rect.height;
        bottomLeft = new Vector3(-xSize / 2, -ySize / 2, 0) + shift;
        bottomRight = new Vector3(xSize / 2, -ySize / 2, 0) + shift;
        topLeft = new Vector3(-xSize / 2, ySize / 2, 0) + shift;
        topRight = new Vector3(xSize / 2, ySize / 2, 0) + shift;
        //draw lines for map borders
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(bottomRight, topRight);





    }

}
