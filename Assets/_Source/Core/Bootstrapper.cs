using System;
using GameStateSystem;
using InputSystem;
using Level;
using Pacman;
using ScoreSystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private PacmanMovement _pacmanMovement;
        [SerializeField] private PacmanCollisionDetector _pacmanCollisionDetector;
        [SerializeField] private ScoreView _scoreView;
        
        private LevelDataSO _levelDataSO;
        private ScoreCounter _scoreCounter;
        private GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            _gameStateMachine = new GameStateMachine();
            _levelDataSO = Resources.Load<LevelDataSO>("Level1DataSO");
            _scoreCounter = new ScoreCounter(_levelDataSO.LevelData.MaxScore);
            _scoreView.Construct(_scoreCounter);
            _inputListener.Construct(_pacmanMovement);
            _pacmanCollisionDetector.Construct(_scoreCounter,_levelDataSO.LevelData.BonusScore);
        }
    }
}
