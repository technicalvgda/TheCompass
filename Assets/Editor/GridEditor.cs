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
        GUILayout.Label("Coordinates of Cell (X,Y)");
        grid.xIndex = EditorGUILayout.IntField(grid.xIndex, GUILayout.Width(50));
        grid.yIndex = EditorGUILayout.IntField(grid.yIndex, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Jump To Cell", GUILayout.Width(255)))
        {
            float startingPosX = -grid.numOfCellsX/2 * grid.width;
            float startingPosy = -grid.numOfCellsY/2 * grid.height;
            float offsetX = grid.width / 2;
            float offsetY = grid.height / 2;

            Vector3 position = SceneView.lastActiveSceneView.pivot;
            position.x = startingPosX + grid.xIndex * grid.width + offsetX;
            position.y = startingPosy + grid.yIndex * grid.height + offsetY;
            position.z = -100;
            grid.cellSelector.transform.position = position;

            SceneView.lastActiveSceneView.pivot = position;
            SceneView.lastActiveSceneView.size = 10;
            SceneView.lastActiveSceneView.Repaint();
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
