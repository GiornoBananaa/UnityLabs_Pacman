using System;
using UnityEngine;

namespace PacmanSystem
{
    [Serializable]
    public class PacmanData
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public int HeartsCount { get; private set; }
    }
}