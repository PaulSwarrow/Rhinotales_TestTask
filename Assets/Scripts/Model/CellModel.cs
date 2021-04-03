using System;
using Data;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class CellModel
    {
        public Vector3Int Position;
        public CellType Type;
    }
}