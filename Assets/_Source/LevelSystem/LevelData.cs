using System;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public int MaxScore { get; private set;}
        [field: SerializeField] public int BonusScore { get; private set;}
        [field: SerializeField] public int BigBonusTime { get; private set;}
        [field: SerializeField] public int GhostKillScore { get; private set;}
        [field: SerializeField] public GameObject SpecialBonusPrefab { get; private set;}
        [field: SerializeField] public GameObject SpecialBonusListIconPrefab { get; private set;}
        [field: SerializeField] public float SpecialBonusSpawnCooldown { get; private set;}
        [field: SerializeField] public int SpecialBonusScore { get; private set;}
        [field: SerializeField] public int SpecialBonusMaxCount { get; private set;}
        [field: SerializeField] public string PacmanRevengeMusic { get; private set;}
        [field: SerializeField] public string StartGameMusic { get; private set;}
    }
}