using System;

namespace PacmanSystem
{
    public class Health
    {
        public int MaxHeartCount { get; }
        public int HeartCount { get; private set; }

        public Action<int> OnHeartCountChange;
        public Action OnDeath;
        
        public Health(int maxHeartCount)
        {
            MaxHeartCount = maxHeartCount;
            HeartCount = MaxHeartCount;
        }

        public bool LooseHeart()
        {
            if(HeartCount <= 0) return false;
            HeartCount--;
            OnHeartCountChange?.Invoke(HeartCount);
            if(HeartCount <= 0)
            {
                OnDeath?.Invoke();
                return true;
            }

            return false;
        }
        
        public void RestoreHealth()
        {
            HeartCount = MaxHeartCount;
            OnHeartCountChange?.Invoke(HeartCount);
        }
    }
}