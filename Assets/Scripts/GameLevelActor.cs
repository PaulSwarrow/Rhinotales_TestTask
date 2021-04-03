using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine.Model;

namespace UnityEngine
{
    [RequireComponent(typeof(Grid))]
    public class GameLevelActor : MonoBehaviour
    {
        [SerializeField] private CellType filter = CellType.Everything;

        [SerializeField] private GameLevelLayer[] layers;

        private Grid grid;

        public Grid Grid => grid ? grid : grid = GetComponent<Grid>();

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

        private void SetCell(Vector3Int position, CellType type)
        {
            foreach (var layer in layers)
            {
                layer.SetValue(position, layer.Type == type);
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