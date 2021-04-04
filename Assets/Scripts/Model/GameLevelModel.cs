using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class GameLevelModel
    {
        public static GameLevelModel Create(List<CellModel> data)
        {
            var model = new GameLevelModel();
            foreach (var cellModel in data)
            {
                model.map[cellModel.Position] = cellModel;
            }

            foreach (var cellModel in model.map)
            {
                var neighbors = new Queue<CellModel>();
                CellModel cell;
                if (model.TryGetCell(cellModel.Key + Vector3Int.left, out cell)) neighbors.Enqueue(cell);
                if (model.TryGetCell(cellModel.Key + Vector3Int.right, out cell)) neighbors.Enqueue(cell);
                if (model.TryGetCell(cellModel.Key + Vector3Int.up, out cell)) neighbors.Enqueue(cell);
                if (model.TryGetCell(cellModel.Key + Vector3Int.down, out cell)) neighbors.Enqueue(cell);
                model.neigborCache[cellModel.Key] = neighbors;

            }

            return model;
        }
        
        private Dictionary<Vector3Int, CellModel> map = new Dictionary<Vector3Int, CellModel>();
        private Dictionary<Vector3Int, Queue<CellModel>> neigborCache = new Dictionary<Vector3Int, Queue<CellModel>>();
        
        
        public IEnumerable<CellModel> GetNeighbors(Vector3Int point, bool onlyWalkable = true)
        {
            if (neigborCache.TryGetValue(point, out var neigbors)) return neigbors;
            return Enumerable.Empty<CellModel>();
        }

        public bool TryGetCell(Vector3Int point, out CellModel cell, CellType filter = CellType.Walkable)
        {
            return map.TryGetValue(point, out cell) && filter.HasFlag(cell.Type);
        }

    }
}