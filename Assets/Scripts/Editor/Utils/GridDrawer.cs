using UnityEditor;
using UnityEngine;

namespace Editor.Utils
{
    public static class GridDrawer
    {
        public static void Draw(Grid grid, int size)
        {
            Handles.matrix = grid.transform.localToWorldMatrix;

            var dimColor = Color.blue;
            var gizmoLineColor = Color.white;

            var gizmoMajorLines = 5;
            var minX = -size;
            var maxX = size;
            var minY = -size;
            var maxY = size;


            for (var x = minX; x < maxX + 1; x++)
            {
                Handles.color = x % gizmoMajorLines == 0 ? gizmoLineColor : dimColor;

                var pos1 = new Vector3(x, minY, 0);
                var pos2 = new Vector3(x, maxY, 0);

                Handles.DrawLine(pos1, pos2);
            }

            for (var y = minY; y < maxY + 1; y++)
            {
                Handles.color = y % gizmoMajorLines == 0 ? gizmoLineColor : dimColor;

                var pos1 = new Vector3(minX, y, 0);
                var pos2 = new Vector3(maxX, y, 0);

                Handles.DrawLine(pos1, pos2);
            }
        }
        
        public static void DrawArea(Grid grid, int size, Vector3Int point, Color color)
        {
            Handles.matrix = grid.transform.localToWorldMatrix;

            Handles.color = color;

            var minX = point.x - size/2;
            var maxX = point.x + size/2 +1;
            var minY = point.y - size/2;
            var maxY = point.y + size/2 +1;


            for (var x = minX; x <= maxX; x++)
            {

                var pos1 = new Vector3(x, minY, 0);
                var pos2 = new Vector3(x, maxY, 0);

                Handles.DrawLine(pos1, pos2);
            }

            for (var y = minY; y <= maxY; y++)
            {
                var pos1 = new Vector3(minX, y, 0);
                var pos2 = new Vector3(maxX, y, 0);

                Handles.DrawLine(pos1, pos2);
            }
        }
    }
}