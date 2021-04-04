﻿using Configs;
using Data;
using Editor.Utils;
using UnityEditor;
using UnityEngine;
using View;

namespace Editor
{
    public class LevelEditorWindow : EditorWindow
    {
        [MenuItem("LevelEditor/MainWindow")]
        public static void ShowWindow()
        {
            GetWindow(typeof(LevelEditorWindow));
        }

        private bool showGrid = true;
        private bool showWalkableTiles = true;
        private bool showObstacleTiles = true;
        private int gridSize = 20;

        private GameLevelActor level;

        private void OnEnable()
        {
            level = FindObjectOfType<GameLevelActor>();
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }

        void OnGUI()
        {
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
            }

            if (GUILayout.Button("Clear"))
            {
                level.Clear();
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