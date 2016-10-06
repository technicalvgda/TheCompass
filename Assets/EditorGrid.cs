using UnityEngine;
using System.Collections;

public class EditorGrid : MonoBehaviour
{
    public float width = 5.0f;//< width of grid cell
    public float height = 5.0f;//< height of grid cell

    public Color color = Color.white;

    private float mapSizeX;
    private float mapSizeY;

    public float numOfCellsX = 200;
    public float numOfCellsY = 200;

    public int xIndex = 1 ;
    public string yIndex = "A";

    public GameObject cellSelector;

    void OnDrawGizmos()
    {
        //each variables is half the size of the grid
        mapSizeX = (numOfCellsX * width)/2;
        mapSizeY = (numOfCellsY * height)/2;

        Gizmos.color = color;

        Vector3 pos = new Vector3(0,0,0);//Camera.current.transform.position;

        for (float y = pos.y - (numOfCellsY/2 *height); y <= (numOfCellsY/2* height) ; y += height)//(float y = pos.y - mapSizeY; y < pos.y + mapSizeY +1; y += height)
        {
            Gizmos.DrawLine(new Vector3(-mapSizeX, y, 0.0f),
                            new Vector3(mapSizeX, y , 0.0f));
        }
      
        for (float x = pos.x - (numOfCellsX/2* width) ; x <= (numOfCellsX/2 * width); x += width)//(float x = pos.x - mapSizeX; x < pos.x + mapSizeX +1; x += width)
        {
            Gizmos.DrawLine(new Vector3(x, -mapSizeY, 0.0f),
                            new Vector3(x, mapSizeY, 0.0f));
        }
        
    }
}