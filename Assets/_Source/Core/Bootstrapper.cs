using System;
using GameStateSystem;
using GhostSystem;
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
        [SerializeField] private Ghost[] _ghosts;
        
        private LevelDataSO _levelDataSO;
        private GhostDataSO _ghostDataSO;
        private ScoreCounter _scoreCounter;
        private GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            _gameStateMachine = new GameStateMachine();
            _levelDataSO = Resources.Load<LevelDataSO>("Level1DataSO");
            _ghostDataSO = Resources.Load<GhostDataSO>("GhostDataSO");
            _scoreCounter = new ScoreCounter(_levelDataSO.LevelData.MaxScore);
            _scoreView.Construct(_scoreCounter);
            _inputListener.Construct(_pacmanMovement);
            _pacmanCollisionDetector.Construct(_scoreCounter,_levelDataSO.LevelData.BonusScore);
            AMovementState[] movementStates =
            {
                new RandomMovementState(),
                new SteadyMovementState(),
                new TargetedMovementState(_pacmanMovement.transform),
                new ScaredMovementState(_pacmanMovement.transform)
            };
            foreach (var ghost in _ghosts)
            {
                Debug.Log(_ghostDataSO.GhostData == null);
                Debug.Log(_pacmanMovement == null);
                Debug.Log(_pacmanMovement == null);
                ghost.Construct(new MovementStateMachine(movementStates), _pacmanMovement.transform, _ghostDataSO.GhostData);
            }
        }
    }
}
