using System.Collections.Generic;
using UnityEngine;

namespace BonusSystem
{
    public class BonusPool<T> where T: Component
    {
        private GameObject _prefab;
        private List<T> _activeObjects;
        private Stack<T> _disabledObjects;
        
        public BonusPool(GameObject prefab)
        {
            _prefab = prefab;
            _activeObjects = new List<T>();
            _disabledObjects = new Stack<T>();
        }
        
        public T GetBonus()
        {
            T specialBonus;
            if (_disabledObjects.Count > 0)
            {
                specialBonus = _disabledObjects.Pop();
            }
            else
            {
                specialBonus = Object.Instantiate(_prefab).GetComponent<T>();
            }

            specialBonus.gameObject.SetActive(true);
            _activeObjects.Add(specialBonus);
            return specialBonus;
        }
        
        public void DisableBonus(T specialBonus)
        {
            specialBonus.gameObject.SetActive(false);
            _activeObjects.Remove(specialBonus);
            _disabledObjects.Push(specialBonus);
        }
        
        public void ClearPool()
        {
            int count = _activeObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var bonus = _activeObjects[0];
                DisableBonus(bonus);
            }
        }
    }
}