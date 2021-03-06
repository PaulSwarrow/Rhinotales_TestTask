using Configs;
using Data;
using Editor.Utils;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;

namespace Editor
{
    public class LevelEditorWindow : EditorWindow
    {
        [MenuItem("LevelEditor/MainWindow")]
        public static void ShowWindow()
        {
            GetWindow(typeof(LevelEditorWindow), false, "Level editor settings");
        }

        private bool showGrid = true;
        private bool showWalkableTiles = true;
        private bool showObstacleTiles = true;
        private int gridSize = 20;

        private GameLevelView level;

        private void OnEnable()
        {
            level = FindObjectOfType<GameLevelView>();
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }

        void OnGUI()
        {
            if(!level) level = FindObjectOfType<GameLevelView>();
            
            showGrid = EditorGUILayout.Toggle("Show grid", showGrid);
            gridSize = EditorGUILayout.IntSlider("Grid size", gridSize, 10, 100);
            showWalkableTiles = EditorGUILayout.Toggle("Show walkable tiles", showWalkableTiles);
            showObstacleTiles = EditorGUILayout.Toggle("Show obstacle tiles", showObstacleTiles);
            if (GUILayout.Button("Save"))
            {
                var config = CreateInstance<GameLevelConfig>();
                config.data = level.Read();
                AssetDatabaseApi.SaveAsset(config);
            }

            if (GUILayout.Button("Load") && AssetDatabaseApi.LoadAsset<GameLevelConfig>(out var loadedConfig))
            {
                level.Write(loadedConfig.data);
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }

            if (GUILayout.Button("Clear"))
            {
                level.Clear();
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }

            var filter = CellType.Empty;
            if (showObstacleTiles) filter |= CellType.Obstacle;
            if (showWalkableTiles) filter |= CellType.Walkable;
            level.SetFilter(filter);
        }

        void OnSceneGUI(SceneView sceneView)
        {
            if (showGrid)
            {
                GridDrawer.Draw(level.Grid, gridSize);
                sceneView.Repaint();
            }
        }
    }
}