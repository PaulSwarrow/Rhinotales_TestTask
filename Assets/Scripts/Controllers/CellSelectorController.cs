using System;
using DI;
using UnityEngine;
using View;

namespace Controllers
{
    public class CellSelectorController : BaseGameController
    {
        [Inject] private Camera camera;
        [Inject] private GameLevelActor level;
        [SerializeField] private ClickArea clickableArea;

        public event Action<Vector3Int> CellClickEvent;

        public override void Subscribe()
        {
            base.Subscribe();
            clickableArea.ClickEvent += OnClick;
        }

        public override void Unsubscribe()
        {
            clickableArea.ClickEvent -= OnClick;
            base.Unsubscribe();
        }

        private void OnClick(Vector3 position)
        {
            var cell = level.Grid.WorldToCell(position);
            CellClickEvent?.Invoke(cell);
        }
    }
}