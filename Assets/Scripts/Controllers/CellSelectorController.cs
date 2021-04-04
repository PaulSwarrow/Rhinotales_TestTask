﻿using System;
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
        /*private void Update()
        {
            // if (Input.GetButtonDown("Fire1"))
            if (Input.GetMouseButtonDown(0))
            {
                var position = GetPositionOnFloor();
                var cell = level.Grid.WorldToCell(position);
                CellClickEvent?.Invoke(cell);
            }
            
        }*/

        private Vector3 GetPositionOnFloor()
        {
            var mousePos = Input.mousePosition;
            // mousePos.y = camera.pixelHeight - mousePos.y;
            var ray = camera.ScreenPointToRay(mousePos);
            var h = ray.origin.y;
            var d = h / Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(Vector3.down, ray.direction));
            return ray.origin + ray.direction.normalized * d;
        }
        
    }
}