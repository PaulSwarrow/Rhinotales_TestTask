using System;
using Data;
using Editor.Utils;
using Ui;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using View;

namespace Editor
{
    public class LevelEditorBrushWindow : EditorWindow
    {
        [MenuItem("LevelEditor/BrushWindow")]
        public static void ShowWindow()
        {
            GetWindow(typeof(LevelEditorBrushWindow), false, "Level editor brush settings");
        }

        private static readonly (string name, CellType mode)[] BrushOptions =
        {
            ("Draw walkable", CellType.Walkable),
            ("Draw obstacles", CellType.Obstacle),
            ("Erase", CellType.Empty)
        };

        private bool active = true;
        private int brushMode;
        private int brushSize;
        private GameLevelView level;
        private Type prevTool;


        private void OnGUI()
        {
            if (Application.isPlaying)
            {
                GUILayout.Label("Not available in runtime");
                LevelEditorBrush.Target = null;
                return;
            }

            if (!level) level = FindObjectOfType<GameLevelView>();
            LevelEditorBrush.Target = level;
            active = EditorGUILayout.Toggle("Brush active", active);
            brushMode = GuiExtension.DrawSwitcher(brushMode, BrushOptions);
            brushSize = EditorGUILayout.IntSlider("Brush size", brushSize, 1, 5);


            if (active)
            {
                if (prevTool == null) prevTool = EditorTools.activeToolType;

                EditorTools.SetActiveTool<LevelEditorBrush>();
            }
            else if (prevTool != null)
            {
                EditorTools.SetActiveTool(prevTool);
                prevTool = null;
            }

            LevelEditorBrush.Active = active;
            LevelEditorBrush.Mode = BrushOptions[brushMode].mode;
            LevelEditorBrush.Size = brushSize;
        }
    }
}