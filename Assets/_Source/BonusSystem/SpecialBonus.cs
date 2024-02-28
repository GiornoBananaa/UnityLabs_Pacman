using System;
using Core;
using UnityEngine;

namespace BonusSystem
{
    public class SpecialBonus: MonoBehaviour
    {
        [SerializeField] private LayerMask _pacmanLayerMask;
        private SpecialBonusSpawner _bonusSpawner;
        private Transform _spawnPoint;

        public void Construct(SpecialBonusSpawner bonusSpawner, Transform spawnPoint)
        {
            _bonusSpawner = bonusSpawner;
            _spawnPoint = spawnPoint;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_pacmanLayerMask.Contains(other.gameObject.layer))
            {
                _bonusSpawner.DisableBonus(this,_spawnPoint);
            }
        }
    }
}