using System;
using Editor.Utils;
using UnityEditor;
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
        private Tilemap tilemap;

        private void OnEnable()
        {
            tilemap = FindObjectOfType<Tilemap>();

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