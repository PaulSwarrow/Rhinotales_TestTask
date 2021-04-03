using System;
using Editor.Utils;
using UnityEditor;
using UnityEditor.EditorTools;
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
        
        private enum BrushMode
        {
            DrawWalkable,
            DrawObstacles,
            Erase
        
        }
        
        private static readonly (string name, BrushMode mode)[] BrushOptions = {
            ("Draw walkable", BrushMode.DrawWalkable),
            ("Draw obstacles", BrushMode.DrawObstacles),
            ("Erase", BrushMode.Erase)
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
        }
        
    }
}