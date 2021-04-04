﻿using UnityEngine;

namespace DefaultNamespace
{
    public class LocalFactory<T> where T : MonoBehaviour
    {
        private T prefab;
        private GameObjectsPool<T> pool;
        private Transform container;
        
        public LocalFactory(Transform container)
        {
            this.container = container;
            pool = new GameObjectsPool<T>(container);
            var children = container.GetComponentsInChildren<T>(true);
            prefab = children[0];
            prefab.gameObject.SetActive(false);
            for (var i = 1; i < children.Length; i++)
            {
                pool.Add(children[i]);
            }
        }

        public T Create()
        {
            if (!pool.Extract(container, out var item))
            {
                item = Object.Instantiate(prefab, container);
                item.gameObject.SetActive(true);
            }

            return item;
        }

        public void Remove(T item)
        {
            pool.Add(item);
        }
 
    }
}