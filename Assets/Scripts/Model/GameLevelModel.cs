using System;
using System.Collections.Generic;
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
        
        
    }
}