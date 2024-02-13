using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelDataSO",menuName = "SO/LevelData")]
    public class LevelDataSO: ScriptableObject
    {
        [field: SerializeField] public LevelData LevelData { get; private set;}
    }
}
