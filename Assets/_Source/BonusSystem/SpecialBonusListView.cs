using UnityEngine;

namespace BonusSystem
{
    public class SpecialBonusListView: MonoBehaviour
    {
        [SerializeField] private RectTransform _bonusImagesParent;
        private BonusPool<RectTransform> _bonusIconPool;
        private SpecialBonusSpawner _specialBonusSpawner;

        private void Start()
        {
            _specialBonusSpawner.OnBonusCollected += CollectBonus;
        }

        public void Construct(SpecialBonusSpawner specialBonusSpawner,BonusPool<RectTransform> bonusIconPool)
        {
            _specialBonusSpawner = specialBonusSpawner;
            _bonusIconPool = bonusIconPool;
            RestoreBonusList();
        }
        
        public void CollectBonus()
        {
            Transform bonus = _bonusIconPool.GetBonus();
            bonus.SetParent(_bonusImagesParent);
        }

        public void RestoreBonusList()
        {
            _bonusIconPool.ClearPool();
        }
        
        private void OnDestroy()
        {
            _specialBonusSpawner.OnBonusCollected -= CollectBonus;
        }
    
    }
}