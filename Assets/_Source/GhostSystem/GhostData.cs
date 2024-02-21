using System;
using UnityEngine;

namespace GhostSystem
{
    [Serializable]
    public class GhostData
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public int HeartsCount { get; private set; }
        [field: SerializeField] public float TargetDetectionRange { get; private set; }
        [field: SerializeField] public LayerMask TargetLayerMask { get; private set; }
        [field: SerializeField] public Color ScaredColor { get; private set; }
        [field: SerializeField] public Color DeathColor { get; private set; }
        [field: SerializeField] public float TimeForHeal { get; private set; }
        [field: SerializeField] public float MinStateChangeTime { get; private set; }
        [field: SerializeField] public float MaxStateChangeTime { get; private set; }
        
    }
}