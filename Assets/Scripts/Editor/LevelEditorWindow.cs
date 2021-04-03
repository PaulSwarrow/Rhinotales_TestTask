using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{

    [MenuItem ("LevelEditor/MainWindow")]

    public static void  ShowWindow () {
        GetWindow(typeof(LevelEditorWindow));
    }
    
    private bool showGrid;
    private bool showWalkableTiles;
    private bool showObstacleTiles;
    void OnGUI ()
    {
        showGrid = EditorGUILayout.Toggle("Show grid", showGrid);
        showWalkableTiles = EditorGUILayout.Toggle("Show walkable tiles", showWalkableTiles);
        showObstacleTiles = EditorGUILayout.Toggle("Show obstacle tiles", showObstacleTiles);
        if (GUILayout.Button("Save"))
        {
            
        }
        
    }
}
