using TMPro;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private ScoreCounter _scoreCounter;
        
        public void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
            _scoreCounter.OnScoreChange += ChangeScore;
        }
        
        private void ChangeScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        private void OnDestroy()
        {
            _scoreCounter.OnScoreChange -= ChangeScore;
        }
    }
}
