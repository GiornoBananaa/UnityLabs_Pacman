using UnityEngine;

namespace GhostSystem
{
    [CreateAssetMenu(fileName = "GhostDataSO",menuName = "SO/GhostData")]
    public class GhostDataSO: ScriptableObject
    {
        [field: SerializeField] public GhostData GhostData;
    }
}