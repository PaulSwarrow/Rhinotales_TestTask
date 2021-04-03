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

        public static void DrawArea(Grid grid, Vector3Int min, Vector3Int max , Vector3Int point, Color color)
        {
            Handles.matrix = grid.transform.localToWorldMatrix;
            Handles.color = color;

            for (var x = min.x; x <= max.x; x++)
            {
                var pos1 = new Vector3(x, min.y, 0);
                var pos2 = new Vector3(x, max.y, 0);

                Handles.DrawLine(pos1, pos2);
            }

            for (var y = min.y; y <= max.y; y++)
            {
                var pos1 = new Vector3(min.x, y, 0);
                var pos2 = new Vector3(max.x, y, 0);

                Handles.DrawLine(pos1, pos2);
            }
        }
    }
}