using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class GameObjectsPool<T> where T : MonoBehaviour
    {
        private Transform container;
        private Queue<T> items = new Queue<T>();

        public GameObjectsPool(Transform container)
        {
            this.container = container;
        }

        public void Add(T item)
        {
            item.gameObject.SetActive(false);
            item.transform.parent = container;
            items.Enqueue(item);
        }

        public bool Extract(Transform to, out T item)
        {
            if (items.Count > 0)
            {
                item = items.Dequeue();
                item.transform.parent = to;
                item.gameObject.SetActive(true);
                return true;
            }

            item = default;
            return false;
        }
    }
}