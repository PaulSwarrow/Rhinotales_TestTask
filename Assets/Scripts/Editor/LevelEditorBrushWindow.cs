using Data;
using Editor.Utils;
using UnityEditor;
using UnityEditor.EditorTools;
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
            level = FindObjectOfType<GameLevelActor>();
            EditorTools.SetActiveTool<LevelEditorBrush>();
            LevelEditorBrush.Target = level;
        }

        private void OnDisable()
        {
        }


        private void OnGUI()
        {
            active = EditorGUILayout.Toggle("Brush active", active);
            brushMode = EditorGuiExtension.DrawSwitcher(brushMode, BrushOptions);
            brushSize = EditorGUILayout.IntSlider("Brush size", brushSize, 1, 5);


            LevelEditorBrush.Mode = BrushOptions[brushMode].mode;
            LevelEditorBrush.Size = brushSize;
        }
    }
}