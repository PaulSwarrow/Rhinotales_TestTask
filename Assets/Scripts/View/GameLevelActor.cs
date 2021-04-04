using System.Collections.Generic;
using Data;
using Model;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(Grid))]
    public class GameLevelActor : MonoBehaviour
    {
        [SerializeField] private CellType filter = CellType.Everything;

        [SerializeField] private GameLevelLayer[] layers;

        [SerializeField] private Grid grid;

        public Grid Grid => grid ;

        public List<CellModel> Read()
        {
            var list = new List<CellModel>();
            foreach (var layer in layers)
            {
                layer.ForEachCell(position => list.Add(new CellModel
                {
                    Position = position,
                    Type = layer.Type
                }));
            }

            return list;
        }

        public void Write(IEnumerable<CellModel> data)
        {
            Clear();
            foreach (var cellModel in data)
            {
                SetCell(cellModel.Position, cellModel.Type);
            }
        }

        private void Clear()
        {
            foreach (var layer in layers)
            {
                layer.Clear();
            }
        }

        public void SetCell(Vector3Int position, CellType type)
        {
            foreach (var layer in layers)
            {
                layer.SetValue(position, layer.Type == type);
            }
        }

        public void SetArea(Vector3Int min, Vector3Int max, CellType mode)
        {
            for (var x = min.x; x < max.x; x++)
            {
                for (var y = min.y; y < max.y; y++)
                {
                    var pos1 = new Vector3Int(x, y, 0);
                    SetCell(pos1, mode);
                }
            }
        }

        public void SetFilter(CellType filter)
        {
            if (this.filter == filter) return;
            this.filter = filter;
            foreach (var levelLayer in layers)
            {
                levelLayer.gameObject.SetActive(filter.HasFlag(levelLayer.Type));
            }
        }
    }
}