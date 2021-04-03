
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine.XR;

namespace Editor.Utils
{
// Tagging a class with the EditorTool attribute and no target type registers a global tool. Global tools are valid for any selection, and are accessible through the top left toolbar in the editor.
    [EditorTool("Level Editor Brush")]
    class LevelEditorBrush : EditorTool
    {

        public override void OnToolGUI(EditorWindow window)
        {
            if (window is SceneView sceneView)
            {
                var point = GetPositionOnFloor(sceneView);
                Handles.DrawWireCube(point, Vector3.one);
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