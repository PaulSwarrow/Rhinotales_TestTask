using System.Collections.Generic;
using System.Linq;
using DI;
using UnityEngine;
using View;

namespace Controllers
{
    public class GameDrawController : BaseGameController
    {
        [SerializeField] private FieldMarkView pointMarkPrefab;
        [SerializeField] private LineRenderer lineRenderer;
        [Inject] private GameLevelActor level;

        private Dictionary<string, FieldMarkView> pointsPool = new Dictionary<string, FieldMarkView>();
        private Dictionary<Vector3Int, FieldMarkView> marks = new Dictionary<Vector3Int, FieldMarkView>();


        public void DrawPoint(Vector3Int position, string text)
        {
            RemovePoint(position);
            var mark = CreateMark(text);
            mark.transform.position = level.Grid.GetCellCenterWorld(position);
            marks[position] = mark;
        }

        public void RemovePoint(Vector3Int position)
        {
            if (marks.TryGetValue(position, out var mark))
            {
                marks.Remove(position);
                RemoveMark(mark);
            }
        }

        public void DrawPath(Vector3Int[] points)
        {
            lineRenderer.SetPositions(points.Select(point => level.Grid.GetCellCenterWorld(point)).ToArray());
        }

        private FieldMarkView CreateMark(string text)
        {
            if (pointsPool.TryGetValue(text, out var mark))
            {
                mark.gameObject.SetActive(true);
                pointsPool.Remove(text);
                return mark;
            }

            mark = Instantiate(pointMarkPrefab);
            mark.Label = text;
            return mark;
        }

        private void RemoveMark(FieldMarkView mark)
        {
            mark.gameObject.SetActive(false);
            pointsPool.Add(mark.Label, mark);
        }
    }
}