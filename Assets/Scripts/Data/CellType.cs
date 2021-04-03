using System;

namespace UnityEngine
{
    [Flags]
    public enum CellType
    {
        Empty = 0,
        Everything = ~Empty,
        Obstacle = 2,
        Walkable = 4
    }
}