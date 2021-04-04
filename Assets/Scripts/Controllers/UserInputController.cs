using System.Collections.Generic;
using System.Linq;
using Data;
using DI;
using Ui;
using UnityEngine;

namespace Controllers
{
    public class UserInputController : BaseGameController
    {
        [Inject] private CellSelectorController selector;
        [Inject] private GameDrawController drawController;
        [Inject] private NavigationController navigation;

        [SerializeField] private UiPointTypeSelector pointTypeSelector;

        private List<NavigationPoint> points = new List<NavigationPoint>();

        public override void Subscribe()
        {
            base.Subscribe();
            pointTypeSelector.DrawOptions("Set A", "Set B");
            selector.CellClickEvent += OnCellSelect;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            selector.CellClickEvent -= OnCellSelect;
        }

        private void OnCellSelect(Vector3Int cell)
        {
            var mark = (MarkType) pointTypeSelector.CurrentSelection;
            var isNew = true;
            for (var i = points.Count - 1; i >= 0; i--)
            {
                var point = points[i];
                if (point.mark == mark)
                {
                    point.cell = cell;
                    drawController.DrawPoint(cell, mark);
                    isNew = false;
                }
                else if (point.cell == cell)
                {
                    points.RemoveAt(i);
                    drawController.RemovePoint(point.mark);
                }
            }

            if (isNew)
            {
                points.Add(new NavigationPoint
                {
                    mark = mark,
                    cell = cell
                });
                drawController.DrawPoint(cell, mark);
            }

            if (points.Count > 1)
            {
                if (navigation.FindPath(points.First().cell, points.Last().cell, out var path))
                {
                    drawController.DrawPath(path);
                    return;
                }
            }

            drawController.ClearPath();
        }
    }
}