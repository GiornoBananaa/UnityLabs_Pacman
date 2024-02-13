using System;
using GameStateSystem;

namespace ScoreSystem
{
    public class ScoreCounter
    {
        private int _score;
        private int _maxScore;

        public Action<int> OnScoreChange;
        public Action OnMaxScore;
        
        public ScoreCounter(int maxScore)
        {
            _maxScore = maxScore;
        }
        
        public void AddScore(int score)
        {
            _score += score;
            OnScoreChange?.Invoke(_score);
            CheckScore();
        }

        private void CheckScore()
        {
            if (_score >= _maxScore)
                OnMaxScore?.Invoke();
        }
    }
}
