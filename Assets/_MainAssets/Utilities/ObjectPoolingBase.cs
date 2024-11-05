using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class ObjectPoolingBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T prefab;
        [SerializeField] private bool parentToPooler = true;
        private Transform _poolParent;

        private List<T> _pool;

        public virtual void Init(T newPrefab, int newPoolSize = 10)
        {
            prefab = newPrefab;
            _poolParent = parentToPooler ? transform : null;
            _pool = new List<T>(newPoolSize);
            for (var i = 0; i < newPoolSize; i++)
            {
                var newItem = Instantiate(prefab, _poolParent);
                newItem.gameObject.SetActive(false);
                _pool.Add(newItem);
            }
        }

        public T GetObject()
        {
            foreach (var item in _pool.Where(item => !item.gameObject.activeInHierarchy))
            {
                item.gameObject.SetActive(true);
                return item;
            }

            var newItem = Instantiate(prefab, _poolParent);
            _pool.Add(newItem);
            newItem.gameObject.SetActive(true);

            return newItem;
        }

        public T ReturnObject(T item)
        {
            item.gameObject.SetActive(false);
            return item;
        }
    }
}