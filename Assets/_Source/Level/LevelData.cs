using System;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public int MaxScore { get; private set;}
        [field: SerializeField] public int BonusScore { get; private set;}
    }
}