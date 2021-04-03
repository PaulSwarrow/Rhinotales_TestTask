using System;
using Data;
using UnityEngine;
using UnityEngine.Tilemaps;

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