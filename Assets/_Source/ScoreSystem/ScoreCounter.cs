using System;
using GameStateSystem;

namespace ScoreSystem
{
    public class ScoreCounter
    {
        public int TotalScore=>_score+_bonusScore;
        private int _score;
        private int _bonusScore;
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
            OnScoreChange?.Invoke(TotalScore);
            CheckScore();
        }
        
        public void AddBonusScore(int score)
        {
            _bonusScore += score;
            OnScoreChange?.Invoke(TotalScore);
            CheckScore();
        }
        
        private void CheckScore()
        {
            if (_score >= _maxScore)
                OnMaxScore?.Invoke();
        }
    }
}
