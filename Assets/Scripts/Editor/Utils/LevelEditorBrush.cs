using System;
using System.Diagnostics;
using Data;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Tilemaps;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Editor.Utils
{
// Tagging a class with the EditorTool attribute and no target type registers a global tool. Global tools are valid for any selection, and are accessible through the top left toolbar in the editor.
    [EditorTool("Level Editor Brush")]
    internal class LevelEditorBrush : EditorTool
    {
        public static GameLevelActor Target;
        public static CellType Mode;
        public static int Size;
        
        private int controlID;

        private void OnEnable()
        {
            controlID = GUIUtility.GetControlID(FocusType.Passive);
        }

        public override void OnToolGUI(EditorWindow window)
        {
            
            if (window is SceneView sceneView)
            {
                var point = GetPositionOnFloor(sceneView);
                var cell = Target.Grid.WorldToCell(point);
                var min = new Vector3Int(cell.x - Size / 2, cell.y - Size / 2,0);
                var max = new Vector3Int(cell.x + Size / 2 + 1, cell.y + Size / 2 + 1,0);
                GridDrawer.DrawArea(Target.Grid, min, max, cell, Color.white);
                var currentEvent = Event.current;
                if (currentEvent.type == EventType.MouseDown)
                {
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