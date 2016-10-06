using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(EditorGrid))]
public class GridEditor : Editor
{
    EditorGrid grid;

    public void OnEnable()
    {
        grid = (EditorGrid)target;
        // Remove first so it doesnt stack
        SceneView.onSceneGUIDelegate -= GridUpdate;
        // Then add delegate
        SceneView.onSceneGUIDelegate += GridUpdate;

        grid.cellSelector = grid.transform.GetChild(0).gameObject;
    }

    void GridUpdate(SceneView sceneview)
    {
        /*
        Event e = Event.current;

        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = r.origin;
        
        if (e.isKey && e.character == 'a')
        {
            GameObject obj;
            Object prefab = PrefabUtility.GetPrefabParent(Selection.activeObject);

            if (prefab)
            {
                obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.numOfCellsX) * grid.numOfCellsX + grid.numOfCellsX / 2.0f,
                                              Mathf.Floor(mousePos.y / grid.numOfCellsY) * grid.numOfCellsY + grid.numOfCellsY / 2.0f, 0.0f);
                obj.transform.position = aligned;
            }
        }
        */
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(" Number of Cells along X ");
        grid.numOfCellsX = EditorGUILayout.FloatField(grid.numOfCellsX, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(" Number of Cells along Y ");
        grid.numOfCellsY = EditorGUILayout.FloatField(grid.numOfCellsY, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Coordinates of Cell (1-200, A-GR)");
        //type an int for x coordinate
        grid.xIndex = EditorGUILayout.IntField(grid.xIndex, GUILayout.Width(50));
        //type a string for y coordinate (A- GR for sheets)
        grid.yIndex = EditorGUILayout.TextField(grid.yIndex.ToUpper(), GUILayout.Width(50));
        GUILayout.EndHorizontal();

       
      
        
       

        if (GUILayout.Button("Jump To Cell", GUILayout.Width(255)))
        {

            if (grid.xIndex > 200 || grid.xIndex < 1)
            {
                Debug.Log("Invalid coordinate for X index on editor grid");
            }
            else if (grid.yIndex.Length > 2)
            {
                Debug.Log("Invalid coordinate for y index on editor grid");
            }
            else
            {
                int firstInt = 0;
                int secondInt = 0;
                //get int values from Y index
                if (grid.yIndex.Length == 1)
                {
                   
                    //Get characters from y index
                    char firstLetter = grid.yIndex[0];
                    //convert to ints (ASCII A is 65, so subtract 64 to start at 1)
                    firstInt = System.Convert.ToInt32(firstLetter) - 64;
                    
                    
                }
                else if (grid.yIndex.Length > 1)
                {
                    
                    //Get characters from y index
                    char firstLetter = grid.yIndex[1];
                    //convert to ints (ASCII A is 65, so subtract 64 to start at 1)
                    firstInt = System.Convert.ToInt32(firstLetter) - 64;
                   
                    char secondLetter = grid.yIndex[0];
                    //convert to ints
                    //get value of second int
                    secondInt = System.Convert.ToInt32(secondLetter) - 64;
                    //each letter up is another set of 26
                    secondInt *= 26;
                   
                }

                int yIndexInt = firstInt + secondInt;

                

                if(grid.xIndex > 200 || grid.xIndex < 1)
                {
                    Debug.Log("Invalid X Index Value");
                    return;
                }
                if (yIndexInt >200 || yIndexInt < 1)
                {
                    Debug.Log("Invalid Y Index Value");
                    return;
                }

                //start in upper left corner
                float startingPosX = -grid.numOfCellsX / 2 * grid.width;
                float startingPosy = grid.numOfCellsY / 2 * grid.height;
                float offsetX = grid.width / 2;
                float offsetY = grid.height / 2;

                Vector3 position = SceneView.lastActiveSceneView.pivot;
                position.x = startingPosX + (yIndexInt-1) * grid.width + offsetX;
                position.y = startingPosy - (grid.xIndex) * grid.height + offsetY;
                position.z = -100;
                grid.cellSelector.transform.position = position;

                SceneView.lastActiveSceneView.pivot = position;
                SceneView.lastActiveSceneView.size = 10;
                SceneView.lastActiveSceneView.Repaint();
            }
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Show Cell Selector: ");
        if (GUILayout.Button("On", GUILayout.Width(50)))
        {
            grid.cellSelector.SetActive(true);
        }
        if (GUILayout.Button("Off", GUILayout.Width(50)))
        {
            grid.cellSelector.SetActive(false);
        }
        GUILayout.EndHorizontal();

        SceneView.RepaintAll();
    }
}
