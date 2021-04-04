using System.Collections.Generic;
using Controllers.Navigation;
using DI;
using Model;
using UnityEngine;

namespace Controllers
{
    public class NavigationController : BaseGameController
    {
        [Inject] private GameLevelModel model;
        
        private PathfindingFrontier frontier = new PathfindingFrontier();

        public bool FindPath(Vector3Int a, Vector3Int b, out IEnumerable<Vector3Int> path)
        {
            if (model.TryGetCell(a, out var cellA) && model.TryGetCell(b, out var cellB))
            {
                return FindPath(cellA, cellB, out path);
            }

            path = default;
            return false;
        }

        private bool FindPath(CellModel a, CellModel b, out IEnumerable<Vector3Int> path)
        {
            frontier.Clear();
            var cameFrom = new Dictionary<CellModel, CellModel>();
            var reachCost = new Dictionary<CellModel, int>();

            frontier.Set(a, 0);
            reachCost.Add(a, 0);
            cameFrom.Add(a, null);

            while (!frontier.Empty())
            {
                var current = frontier.Shift();
                if (current == b)
                {
                    var result = new List<Vector3Int>();
                    var cell = current;
                    while (cell != null)
                    {
                        result.Add(cell.Position);
                        cell = cameFrom[cell];
                    }

                    result.Reverse();
                    path = result;

                    return true;
                }

                foreach (var neighbor in model.GetNeighbors(current.Position))
                {
                    var currentCost = reachCost[current] + 1;
                    if (!reachCost.TryGetValue(neighbor, out var lastCost) || lastCost > currentCost)
                    {
                        reachCost[neighbor] = currentCost;
                        cameFrom[neighbor] = current;
                        var priority = currentCost + HeuristicDistance(neighbor, b);
                        frontier.Set(neighbor, priority);
                    }
                }
            }

            path = default;
            return false;
        }
        
        private int HeuristicDistance(CellModel point, CellModel goal)
        {
            return (int) Vector3Int.Distance(point.Position, goal.Position);
        }
    }
}