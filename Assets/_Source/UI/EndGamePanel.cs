using System;
using GameStateSystem;
using ScoreSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private TMP_Text _scoreText;

        private ScoreCounter _scoreCounter;
        private GameStateMachine _gameStateMachine;
        
        public void Construct(ScoreCounter scoreCounter, GameStateMachine gameStateMachine)
        {
            _scoreCounter = scoreCounter;
            _gameStateMachine = gameStateMachine;
            Debug.Log(123);
            _restartButton.onClick.AddListener(RestartButton);
            _mainMenuButton.onClick.AddListener(MainMenuButton);
        }

        public void OpenPanel()
        {
            _scoreText.text = _scoreCounter.TotalScore.ToString();
            gameObject.SetActive(true);
        }
        
        
        public void ClosePanel()
        {
            gameObject.SetActive(false);
        }
            
        private void RestartButton()
        {
            _gameStateMachine.ChangeState<RestartGameState>();
        }
        
        private void MainMenuButton()
        {
            SceneManager.LoadScene(0);
        }
    }
}
