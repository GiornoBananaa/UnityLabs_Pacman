using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PacmanSystem
{
    public class HealthBar: MonoBehaviour
    {
        [SerializeField] private RectTransform _heartImagesParent;
        [SerializeField] private GameObject _heartPrefab;
        private Stack<GameObject> _activeHearts;
        private Health _health;
        private float _maxHeartCount;

        private void Start()
        {
            _health.OnHeartCountChange += ChangeHeartsCount;
        }

        public void Construct(Health health,float maxHeartCount)
        {
            _health = health;
            _activeHearts = new Stack<GameObject>();
            _maxHeartCount = maxHeartCount;
            RestoreHeartIcons();
        }
        
        public void PlayDeathAnimation(float duration,float fadeDuration)
        {
            Image heartImage= _activeHearts.Peek().GetComponentInChildren<Image>();
            heartImage.DOFade(0,fadeDuration).SetLoops((int)(duration/fadeDuration),LoopType.Yoyo);
        }
        
        private void ChangeHeartsCount(int newHeartsCount)
        {
            if(newHeartsCount < _activeHearts.Count)
            {
                for (int i = 0; i < _activeHearts.Count - newHeartsCount; i++)
                {
                    if (_activeHearts.Count == 1)
                        return;
                    _activeHearts.Pop().SetActive(false);
                }
            }
            else
            {
                RestoreHeartIcons();
            }
        }

        private void RestoreHeartIcons()
        {
            while(_activeHearts.Count< _maxHeartCount)
            {
                _activeHearts.Push(Instantiate(_heartPrefab, _heartImagesParent));
            }
        }
        
        private void OnDestroy()
        {
            _health.OnHeartCountChange -= ChangeHeartsCount;
        }
    }
}