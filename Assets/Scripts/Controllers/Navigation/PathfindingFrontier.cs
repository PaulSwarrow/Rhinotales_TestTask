using System.Collections.Generic;
using System.Linq;
using Model;

namespace Controllers.Navigation
{
    public class PathfindingFrontier
    {
        private List<(CellModel item, int priority)> list = new List<(CellModel item, int priority)>();

        public void Set(CellModel item, int priority)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var element = list[i];
                if (element.item == item)
                {
                    element.priority = priority;
                    return;
                }

                if (element.priority > priority)
                {
                    list.Insert(i, (item, priority));
                    return;
                }
            }

            list.Add((item, priority));
        }

        public CellModel Shift()
        {
            if (list.Count > 0)
            {
                var entry = list.First();
                list.RemoveAt(0);
                return entry.item;
            }

            return default;
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Empty()
        {
            return !list.Any();
        }
    }
}