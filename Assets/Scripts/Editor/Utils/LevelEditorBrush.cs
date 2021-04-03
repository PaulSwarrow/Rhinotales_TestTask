using System;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Editor.Utils
{
// Tagging a class with the EditorTool attribute and no target type registers a global tool. Global tools are valid for any selection, and are accessible through the top left toolbar in the editor.
    [EditorTool("Level Editor Brush")]
    class LevelEditorBrush : EditorTool
    {
        public static GameLevelActor Target;


        public override void OnToolGUI(EditorWindow window)
        {
            if (window is SceneView sceneView)
            {
                var point = GetPositionOnFloor(sceneView);
                var cell = Target.Grid.WorldToCell(point);

                GridDrawer.DrawArea(Target.Grid, 3, cell, Color.white);
                

                sceneView.Repaint();
            }
        }

        private Vector3 GetPositionOnFloor(SceneView sceneView)
        {
            var mousePos = Event.current.mousePosition;
            mousePos.y = sceneView.camera.pixelHeight - mousePos.y;
            var ray = sceneView.camera.ScreenPointToRay(mousePos);
            var h = ray.origin.y;
            var d = h / Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(Vector3.down, ray.direction));
            return ray.origin + ray.direction.normalized * d;
        }
    }
}