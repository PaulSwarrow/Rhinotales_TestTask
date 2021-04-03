using System;
using Data;
using Editor.Utils;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR;

namespace Editor
{
    public class LevelEditorBrushWindow : EditorWindow
    {

        [MenuItem ("LevelEditor/BrushWindow")]

        public static void  ShowWindow () {
            GetWindow(typeof(LevelEditorBrushWindow));
        }
        
        private static readonly (string name, CellType mode)[] BrushOptions = {
            ("Draw walkable", CellType.Walkable),
            ("Draw obstacles", CellType.Obstacle),
            ("Erase", CellType.Empty)
        };
    
        private bool active;
        private int brushMode;
        private GameLevelActor level;

        private void OnEnable()
        {
            level = FindObjectOfType<GameLevelActor>();
            EditorTools.SetActiveTool<LevelEditorBrush>();
            LevelEditorBrush.Target = level;

        }

        private void OnDisable()
        {
        }
        

        private void OnGUI () {
            
            active = EditorGUILayout.Toggle("Brush active", active);
            brushMode = EditorGuiExtension.DrawSwitcher(brushMode, BrushOptions);
            LevelEditorBrush.Mode = BrushOptions[brushMode].mode;
        }
        
    }
}