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

        public void LooseHeart()
        {
            if(HeartCount <= 0) return;
            HeartCount--;
            OnHeartCountChange?.Invoke(HeartCount);
            if(HeartCount <= 0)
                OnDeath?.Invoke();
        }
    }
}