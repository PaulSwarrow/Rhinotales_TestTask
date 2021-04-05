using Data;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;

namespace Editor.Utils
{
// Tagging a class with the EditorTool attribute and no target type registers a global tool. Global tools are valid for any selection, and are accessible through the top left toolbar in the editor.
    [EditorTool("Level Editor Brush")]
    internal class LevelEditorBrush : EditorTool
    {
        public static GameLevelView Target;
        public static CellType Mode;
        public static int Size;
        public static bool Active;

        private int controlID;

        private void OnEnable()
        {
            controlID = GUIUtility.GetControlID(FocusType.Passive);
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if (Target == null || !Active) return;
            if (window is SceneView sceneView)
            {
                var point = GetPositionOnFloor(sceneView);
                var cell = Target.Grid.WorldToCell(point);
                var min = new Vector3Int(cell.x - Mathf.FloorToInt(Size / 2f), cell.y - Mathf.FloorToInt(Size / 2f), 0);
                var max = new Vector3Int(cell.x + Mathf.CeilToInt(Size / 2f), cell.y + Mathf.CeilToInt(Size / 2f), 0);
                GridDrawer.DrawArea(Target.Grid, min, max, cell, Color.white);
                var currentEvent = Event.current;
                if (currentEvent.type == EventType.MouseDown)
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    GUIUtility.hotControl = controlID;
                    Target.SetArea(min, max, Mode);
                }

                if (currentEvent.type == EventType.MouseDrag)
                {
                    Target.SetArea(min, max, Mode);
                }

                if (currentEvent.type == EventType.MouseUp)
                {
                    GUIUtility.hotControl = 0;
                }

                sceneView.Repaint();
            }
            else
            {
                GUIUtility.hotControl = 0;
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