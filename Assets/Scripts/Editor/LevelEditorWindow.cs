using Editor.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelEditorWindow : EditorWindow
{
    [MenuItem("LevelEditor/MainWindow")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelEditorWindow));
    }

    private bool showGrid;
    private bool showWalkableTiles;
    private bool showObstacleTiles;
    private int gridSize = 20;

    private Grid grid;

    private void OnEnable()
    {
        grid = FindObjectOfType<Grid>();
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

    void OnGUI()
    {
        showGrid = EditorGUILayout.Toggle("Show grid", showGrid);
        showWalkableTiles = EditorGUILayout.Toggle("Show walkable tiles", showWalkableTiles);
        showObstacleTiles = EditorGUILayout.Toggle("Show obstacle tiles", showObstacleTiles);
        if (GUILayout.Button("Save"))
        {
        }
    }

    void OnSceneGUI(SceneView sceneView)
    {
        if (showGrid)
        {
            GridDrawer.Draw(grid, gridSize);
        }
    }
}