using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BonusSystem
{
    public class SpecialBonusSpawner: MonoBehaviour
    {
        private List<Transform> _availableSpawnPoints;
        private HashSet<Transform> _closedSpawnPoints;
        private BonusPool<SpecialBonus> _specialBonusPool;
        private float _spawnCooldown;
        private float _elapsedTimeAfterSpawn;
        private float _specialBonusMaxCount;
        private float _specialBonusCount;

        public Action OnBonusCollected;
        
        public void Construct(BonusPool<SpecialBonus> specialBonusPool,Transform[] specialBonusSpawnPoints,float spawnCooldown,float specialBonusMaxCount)
        {
            _specialBonusPool = specialBonusPool;
            _availableSpawnPoints = new List<Transform>(specialBonusSpawnPoints);
            _closedSpawnPoints = new HashSet<Transform>();
            _spawnCooldown = spawnCooldown;
            _specialBonusMaxCount = specialBonusMaxCount;
        }
        
        public void DisableBonus(SpecialBonus specialBonus,Transform spawnPoint)
        {
            _specialBonusPool.DisableBonus(specialBonus);
            _closedSpawnPoints.Remove(spawnPoint);
            _availableSpawnPoints.Add(spawnPoint);
            OnBonusCollected?.Invoke();
        }

        public void ResetBonusCount()
        {
            foreach (var spawnPoint in _closedSpawnPoints)
            {
                _availableSpawnPoints.Add(spawnPoint);
            }
            _specialBonusPool.ClearPool();
            _specialBonusCount = 0;
            _elapsedTimeAfterSpawn = 0;
        }
        
        private void Update()
        {
            _elapsedTimeAfterSpawn += Time.deltaTime;
            if (_elapsedTimeAfterSpawn > _spawnCooldown && _specialBonusCount<_specialBonusMaxCount)
            {
                _elapsedTimeAfterSpawn = 0;
                SpawnBigBonus();
            }
        }
        
        private void SpawnBigBonus()
        {
            _specialBonusCount++;
            Transform spawnPoint = _availableSpawnPoints[Random.Range(0,_availableSpawnPoints.Count)];
            _availableSpawnPoints.Remove(spawnPoint);
            _closedSpawnPoints.Add(spawnPoint);
            SpecialBonus specialBonus = _specialBonusPool.GetBonus();
            specialBonus.Construct(this,spawnPoint);
            specialBonus.transform.position = spawnPoint.position;
            specialBonus.transform.rotation = spawnPoint.rotation;
        }
    }
}
