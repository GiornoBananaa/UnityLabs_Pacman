using System;
using GameStateSystem;
using UnityEngine;

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
        }

        public void RestoreScore()
        {
            _score = 0;
            _bonusScore = 0;
            OnScoreChange?.Invoke(TotalScore);
        }
        
        private void CheckScore()
        {
            Debug.Log(_score);
            if (_score >= _maxScore)
                OnMaxScore?.Invoke();
        }
    }
}
