using System;

namespace Data
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