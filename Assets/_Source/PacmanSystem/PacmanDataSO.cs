using UnityEngine;

namespace PacmanSystem
{
    [CreateAssetMenu(fileName = "PacmanDataSO",menuName = "SO/PacmanData")]
    public class PacmanDataSO: ScriptableObject
    {
        [field: SerializeField] public PacmanData PacmanData;
    }
}