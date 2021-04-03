using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Tilemap))]
    [ExecuteAlways]
    public class GameLevelLayer : MonoBehaviour
    {
        private Tilemap map;
        [SerializeField] private Tile prefab;
        public CellType Type;

        private void Awake()
        {
            map = GetComponent<Tilemap>();
        }

        public void SetValue(Vector3Int position, bool value)
        {
            map.SetTile(position, value ? prefab : null);
        }
        
        

        private List<Vector3Int> GetCells(Tilemap tilemap)
        {
            var list = new List<Vector3Int>();
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {   
                if (tilemap.HasTile(pos))
                {
                    list.Add(pos);
                }
            }

            return list;
        }
    }
}