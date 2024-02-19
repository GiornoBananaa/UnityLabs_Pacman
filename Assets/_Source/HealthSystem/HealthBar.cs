using System;
using System.Collections.Generic;
using UnityEngine;

namespace PacmanSystem
{
    public class HealthBar: MonoBehaviour
    {
        [SerializeField] private RectTransform _heartImagesParent;
        [SerializeField] private GameObject _heartPrefab;
        private Stack<GameObject> _activeHearts;
        private Health _health;

        private void Start()
        {
            _health.OnHeartCountChange += ChangeHeartsCount;
        }

        public void Construct(Health health,float maxHeartCount)
        {
            _health = health;
            _activeHearts = new Stack<GameObject>();
            for (int i = 0; i < maxHeartCount; i++)
            {
                _activeHearts.Push(Instantiate(_heartPrefab,_heartImagesParent));
            }
        }

        private void ChangeHeartsCount(int heartsCount)
        {
            for (int i = 0; i < _activeHearts.Count-heartsCount; i++)
            {
                _activeHearts.Pop().SetActive(false);
            }
        }
        
        private void OnDestroy()
        {
            _health.OnHeartCountChange -= ChangeHeartsCount;
        }
    }
}