using System;
using System.Collections.Generic;
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

            return model;
        }
        
        private Dictionary<Vector3Int, CellModel> map = new Dictionary<Vector3Int, CellModel>();
        
        
        public IEnumerable<CellModel> GetNeighbors(Vector3Int point, bool onlyWalkable = true)
        {
            var list = new List<CellModel>();
            CellModel cell;
            if (TryGetCell(point + Vector3Int.left, out cell)) list.Add(cell);
            if (TryGetCell(point + Vector3Int.right, out cell)) list.Add(cell);
            if (TryGetCell(point + Vector3Int.up, out cell)) list.Add(cell);
            if (TryGetCell(point + Vector3Int.down, out cell)) list.Add(cell);
            return list;
        }

        public bool TryGetCell(Vector3Int point, out CellModel cell, CellType filter = CellType.Walkable)
        {
            return map.TryGetValue(point, out cell) && filter.HasFlag(cell.Type);
        }

    }
}