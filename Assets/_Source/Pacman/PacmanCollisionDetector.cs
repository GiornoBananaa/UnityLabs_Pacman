using Core;
using GhostSystem;
using ScoreSystem;
using UnityEngine;

namespace PacmanSystem
{
    public class PacmanCollisionDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _bonusLayerMask;
        [SerializeField] private LayerMask _bigBonusLayerMask;
        [SerializeField] private LayerMask _ghostLayerMask;
        private int _bonusScore;
        private bool _attackGhost;
        private ScoreCounter _scoreCounter;
        private Health _pacmanHealth;
        private bool _attackCooldown;
        
        public void Construct(ScoreCounter scoreCounter, Health pacmanHealth, int bonusScore)
        {
            _scoreCounter = scoreCounter;
            _bonusScore = bonusScore;
            _pacmanHealth = pacmanHealth;
        }
        
        public void EnableAttackGhost(bool enable)
        {
            _attackGhost = enable;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_bonusLayerMask.Contains(other.gameObject.layer))
            {
                _scoreCounter.AddScore(_bonusScore);
                other.gameObject.SetActive(false);
            }
            if (_bonusLayerMask.Contains(other.gameObject.layer))
            {
                _scoreCounter.AddScore(_bonusScore);
                other.gameObject.SetActive(false);
            }
            else if (_ghostLayerMask.Contains(other.gameObject.layer))
            {
                if (!_attackGhost)
                {
                    _pacmanHealth.LooseHeart();
                }
            }
        }
    }
}
