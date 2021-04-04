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
            GetWindow(typeof(LevelEditorBrushWindow));
        }

        private static readonly (string name, CellType mode)[] BrushOptions =
        {
            ("Draw walkable", CellType.Walkable),
            ("Draw obstacles", CellType.Obstacle),
            ("Erase", CellType.Empty)
        };

        private bool active;
        private int brushMode;
        private int brushSize;
        private GameLevelActor level;

        private void OnEnable()
        {
            EditorTools.SetActiveTool<LevelEditorBrush>();
        }

        private void OnDisable()
        {
        }


        private void OnGUI()
        {
            if (Application.isPlaying)
            {
                GUILayout.Label("Not available in runtime");
                LevelEditorBrush.Target = null;
                return;
            }

            if (!level) level = FindObjectOfType<GameLevelActor>();
            LevelEditorBrush.Target = level;
            active = EditorGUILayout.Toggle("Brush active", active);
            brushMode = GuiExtension.DrawSwitcher(brushMode, BrushOptions);
            brushSize = EditorGUILayout.IntSlider("Brush size", brushSize, 1, 5);


            LevelEditorBrush.Mode = BrushOptions[brushMode].mode;
            LevelEditorBrush.Size = brushSize;
        }
    }
}