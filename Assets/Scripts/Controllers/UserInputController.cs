using System;
using Data;
using DI;
using Ui;
using UnityEngine;

namespace Controllers
{
    public class UserInputController : BaseGameController
    {
        private static readonly (string name, NavigationPoint value)[] Options = {
            ("Set A", new NavigationPoint{name = "A"}),
            ("Set B", new NavigationPoint{name = "B"}),
        };


        [Inject] private CellSelectorController selector;
        [Inject] private GameDrawController drawController;

        [SerializeField] private UiPointTypeSelector pointTypeSelector;
        
        public override void Subscribe()
        {
            base.Subscribe();
            pointTypeSelector.DrawOptions(Options);
            selector.CellClickEvent += OnCellSelect;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            selector.CellClickEvent -= OnCellSelect;
        }

        private void OnCellSelect(Vector3Int cell)
        {
            var point = Options[pointTypeSelector.CurrentSelection].value;
            if (point.isSet) drawController.RemovePoint(point.cell);
            point.cell = cell;
            point.isSet = true;
            drawController.DrawPoint(point.cell, point.name);
        }
    }
}