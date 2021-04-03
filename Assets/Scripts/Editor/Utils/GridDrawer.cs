using UnityEditor;
using UnityEngine;

namespace Editor.Utils
{
    public static class GridDrawer
    {
        public static void Draw(Grid grid, int size)
        {
            Handles.matrix = grid.transform.localToWorldMatrix;

            // set colours
            var dimColor = Color.blue;
            var gizmoLineColor = Color.white;

            var gizmoMajorLines = 5;
            var minX = -size;
            var maxX = size;
            var minY = -size;
            var maxY = size;

            // nudges the whole grid rel
            var gridOffset = Vector3.zero;

            for (var x = minX; x < maxX + 1; x++)
            {
                // find major lines
                Handles.color = x % gizmoMajorLines == 0 ? gizmoLineColor : dimColor;

                var pos1 = new Vector3(x, minY, 0);
                var pos2 = new Vector3(x, maxY, 0);

                Handles.DrawLine(gridOffset + pos1, gridOffset + pos2);
            }

            // draw the vertical lines
            for (var y = minY; y < maxY + 1; y++)
            {
                // find major lines
                Handles.color = y % gizmoMajorLines == 0 ? gizmoLineColor : dimColor;

                var pos1 = new Vector3(minX, y, 0);
                var pos2 = new Vector3(maxX, y, 0);

                Handles.DrawLine(gridOffset + pos1, gridOffset + pos2);
            }
            
        }

    }
}