using System;
using Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace View
{
    [RequireComponent(typeof(Tilemap))]
    [ExecuteAlways]
    public class GameLevelLayer : MonoBehaviour
    {
        [SerializeField] private Tilemap map;
        [SerializeField] private Tile prefab;
        public CellType Type;

        public void SetValue(Vector3Int position, bool value)
        {
            map.SetTile(position, value ? prefab : null);
        }

        public void ForEachCell(Action<Vector3Int> handler)
        {
            foreach (var pos in map.cellBounds.allPositionsWithin)
            {
                if (map.HasTile(pos))
                {
                    handler(pos);
                }
            }
        }

        public void Clear()
        {
            map.ClearAllTiles();
        }
    }
}