using System.Collections.Generic;
using System.Linq;
using Data;
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

        private Queue<FieldMarkView> markPool = new Queue<FieldMarkView>();
        private Dictionary<MarkType, FieldMarkView> marks = new Dictionary<MarkType, FieldMarkView>();


        public void DrawPoint(Vector3Int position, MarkType label)
        {
            var mark = GetMark(label);
            mark.transform.position = level.Grid.GetCellCenterWorld(position);
        }

        public void RemovePoint(MarkType label)
        {
            if (marks.TryGetValue(label, out var mark))
            {
                marks.Remove(label);
                mark.gameObject.SetActive(false);
                markPool.Enqueue(mark);
            }
        }

        public void DrawPath(IEnumerable<Vector3Int> points)
        {
            var array = points.Select(point => level.Grid.GetCellCenterWorld(point)).ToArray();
            lineRenderer.positionCount = array.Length;
            lineRenderer.SetPositions(array);
        }

        public void ClearPath()
        {
            lineRenderer.positionCount = 0;
        }

        private FieldMarkView GetMark(MarkType label)
        {
            if (marks.TryGetValue(label, out var mark))
            {
                return mark;
            }

            if (markPool.Count > 0)
            {
                mark = markPool.Dequeue();
                mark.gameObject.SetActive(true);
            }
            else
            {
                mark = Instantiate(pointMarkPrefab);
            }

            mark.Label = label.ToString();
            marks[label] = mark;
            return mark;
        }
    }
}