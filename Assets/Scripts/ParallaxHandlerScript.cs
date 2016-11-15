using UnityEngine;
using System.Collections;
//using UnityEditor;

public class ParallaxHandlerScript : MonoBehaviour
{
    //public GameObject boundTile;
    public GameObject edgeBoundTile;

    public bool hasForeground = true;

    //1 is closest layer, 3 is furthest layer (background panel)
    //front layer is the foreground
    public GameObject frontLayer,layer1, layer2, layer3;

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
    private float width1, width2, width3, widthF;
    private float height1, height2, height3, heightF;

    private float halfWidth1, halfWidth2, halfWidth3, halfWidthF;
    private float halfHeight1, halfHeight2, halfHeight3, halfHeightF;

    Vector3 bottomLeft;
    Vector3 bottomRight;
    Vector3 topLeft;
    Vector3 topRight;

    // Use this for initialization
    void Start()
    {



        //assign camera and player variable
        mainCamera = Camera.main;
        centerPos = new Vector3(0, 0, transform.position.z);//mainCamera.transform.position;
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;


        //set sorting layers of all children to each layer
        foreach (Transform child in layer1.transform)
        { child.GetComponent<SpriteRenderer>().sortingLayerName = "Parallax1"; }

        foreach (Transform child in layer2.transform)
        { child.GetComponent<SpriteRenderer>().sortingLayerName = "Parallax2"; }

        foreach (Transform child in layer3.transform)
        { child.GetComponent<SpriteRenderer>().sortingLayerName = "Background"; }

        if(hasForeground)
        {
            foreach (Transform child in frontLayer.transform)
            { child.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground"; }
        }
        

        //set layer math variables
        width1 = layer1.GetComponent<RectTransform>().rect.width;
        width2 = layer2.GetComponent<RectTransform>().rect.width;
        width3 = layer3.GetComponent<RectTransform>().rect.width;
        height1 = layer1.GetComponent<RectTransform>().rect.height;
        height2 = layer2.GetComponent<RectTransform>().rect.height;
        height3 = layer3.GetComponent<RectTransform>().rect.height;

        if(hasForeground)
        {
            widthF = frontLayer.GetComponent<RectTransform>().rect.width;
            heightF = frontLayer.GetComponent<RectTransform>().rect.height;
            halfWidthF = widthF / 2;
            halfHeightF = heightF / 2;
        }
        else
        {
            widthF = 0;
            heightF = 0;
            halfWidthF = 0;
            halfHeightF = 0;
        }
       

        halfWidth1 = width1 / 2;
        halfWidth2 = width2 / 2;
        halfWidth3 = width3 / 2;
        halfHeight1 = height1 / 2;
        halfHeight2 = height2 / 2;
        halfHeight3 = height3 / 2;
       

        //spawn boundary tiles
        SpawnBoundaries();
    }
	
	// Update is called once per frame
	void Update ()
    {
        


        //handle shifting of all 3 layers
        HandleLayerMovement(layer1.transform, halfWidth1, halfHeight1);
        HandleLayerMovement(layer2.transform, halfWidth2, halfHeight2);
        HandleLayerMovement(layer3.transform, halfWidth3, halfHeight3);

        if(hasForeground)
        {
            HandleLayerMovement(frontLayer.transform, halfWidthF, halfHeightF);
        }
       

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

    void SpawnBoundaries()
    {
        float borderWidth = edgeBoundTile.GetComponent<SpriteRenderer>().bounds.size.x/2; //half the width of the border  
        float borderHeight = edgeBoundTile.GetComponent<SpriteRenderer>().bounds.size.y / 2; //half the width of the border //start pos
        //Instantiate(edgeBoundTile, new Vector3(transform.position.x - (levelSizeX / 2 * unit) - borderWidth, transform.position.y - (levelSizeY / 2 * unit) - borderWidth, 0), transform.rotation);
        
        for(int i = 0; i <= 13; i++)
        {    
            //Left border                                               
            Instantiate(edgeBoundTile, new Vector3(transform.position.x - (levelSizeX / 2 * unit) - borderWidth, transform.position.y - (levelSizeY / 2 * unit) - borderHeight + (i * borderHeight*2)-borderHeight, 0), transform.rotation);
            //top border
            Instantiate(edgeBoundTile, new Vector3(transform.position.x - (levelSizeX / 2 * unit) +  borderWidth + (i * borderWidth * 2), transform.position.y - (levelSizeY / 2 * unit) - borderHeight, 0), transform.rotation);
            //right border
            Instantiate(edgeBoundTile, new Vector3(transform.position.x + (levelSizeX / 2 * unit) + borderWidth, transform.position.y - (levelSizeY / 2 * unit) + borderHeight + (i * borderHeight * 2), 0), transform.rotation);
            //bottom border
            Instantiate(edgeBoundTile, new Vector3(transform.position.x - (levelSizeX / 2 * unit) - borderWidth + (i * borderWidth * 2)-borderWidth, transform.position.y + (levelSizeY / 2 * unit) + borderHeight, 0), transform.rotation);
            /*
            //spawn left side, row2 row 3
            Instantiate(edgeBoundTile, new Vector3(transform.position.x - (levelSizeX/2 * unit), transform.position.y + i * unit, 0), transform.rotation );
            Instantiate(boundTile,  new Vector3(transform.position.x - (levelSizeX / 2 * unit) - unit, transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x - (levelSizeX / 2 * unit) - (unit*2), transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x - (levelSizeX / 2 * unit) - (unit * 3), transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x - (levelSizeX / 2 * unit) - (unit * 4), transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x - (levelSizeX / 2 * unit) - (unit * 5), transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x - (levelSizeX / 2 * unit) - (unit * 6), transform.position.y + i * unit, 0), transform.rotation);
            //spawn right side
            Instantiate(edgeBoundTile, new Vector3(transform.position.x + (levelSizeX / 2 * unit), transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x + (levelSizeX / 2 * unit) + unit, transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x + (levelSizeX / 2 * unit) + (unit * 2), transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x + (levelSizeX / 2 * unit) + (unit * 3), transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x + (levelSizeX / 2 * unit) + (unit * 4), transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x + (levelSizeX / 2 * unit) + (unit * 5), transform.position.y + i * unit, 0), transform.rotation);
            Instantiate(boundTile, new Vector3(transform.position.x + (levelSizeX / 2 * unit) + (unit * 6), transform.position.y + i * unit, 0), transform.rotation);

            //spawn top side
            Instantiate(edgeBoundTile, new Vector3( transform.position.x + i * unit, transform.position.y + (levelSizeY / 2 * unit), 0), Quaternion.Euler(new Vector3( 0, 180, -90)));
            Instantiate(boundTile, new Vector3(transform.position.x + i * unit, transform.position.y + (levelSizeY / 2 * unit) + unit, 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, new Vector3(transform.position.x + i * unit, transform.position.y + (levelSizeY / 2 * unit) + (unit*2), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, new Vector3(transform.position.x + i * unit, transform.position.y + (levelSizeY / 2 * unit) + (unit * 3), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, new Vector3(transform.position.x + i * unit, transform.position.y + (levelSizeY / 2 * unit) + (unit * 4), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, new Vector3(transform.position.x + i * unit, transform.position.y + (levelSizeY / 2 * unit) + (unit * 5), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, new Vector3(transform.position.x + i * unit, transform.position.y + (levelSizeY / 2 * unit) + (unit * 6), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            //spawn bottom side
            Instantiate(edgeBoundTile, transform.position + new Vector3(transform.position.x + i * unit, transform.position.y - (levelSizeY / 2 * unit), 0), Quaternion.Euler( new Vector3(0, 180, 90)));
            Instantiate(boundTile, transform.position + new Vector3(transform.position.x + i * unit, transform.position.y - (levelSizeY / 2 * unit) - unit, 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, transform.position + new Vector3(transform.position.x + i * unit, transform.position.y - (levelSizeY / 2 * unit) - (unit * 2), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, transform.position + new Vector3(transform.position.x + i * unit, transform.position.y - (levelSizeY / 2 * unit) - (unit * 3), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, transform.position + new Vector3(transform.position.x + i * unit, transform.position.y - (levelSizeY / 2 * unit) - (unit * 4), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, transform.position + new Vector3(transform.position.x + i * unit, transform.position.y - (levelSizeY / 2 * unit) - (unit * 5), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
            Instantiate(boundTile, transform.position + new Vector3(transform.position.x + i * unit, transform.position.y - (levelSizeY / 2 * unit) - (unit * 6), 0), Quaternion.Euler(new Vector3(0, 180, -90)));
        */
        }
        

    }
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

        //DRAW FRONT LAYER
        Gizmos.color = Color.cyan;
        shift = frontLayer.transform.position;
        //Get corner positions of map
        xSize = frontLayer.GetComponent<RectTransform>().rect.width;
        ySize = frontLayer.GetComponent<RectTransform>().rect.height;
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
