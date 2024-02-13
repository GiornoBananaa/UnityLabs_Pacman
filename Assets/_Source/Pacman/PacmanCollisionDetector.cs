using System;
using Core;
using ScoreSystem;
using UnityEngine;

namespace Pacman
{
    public class PacmanCollisionDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _bonusLayerMask;
        private int _bonusScore;
        private ScoreCounter _scoreCounter;
        
        public void Construct(ScoreCounter scoreCounter,int bonusScore)
        {
            _scoreCounter = scoreCounter;
            _bonusScore = bonusScore;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_bonusLayerMask.Contains(other.gameObject.layer))
            {
                _scoreCounter.AddScore(_bonusScore);
                other.gameObject.SetActive(false);
            }
        }
    }
}
