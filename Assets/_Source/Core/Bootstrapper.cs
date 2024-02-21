using System;
using System.Collections.Generic;
using GameStateSystem;
using GhostSystem;
using InputSystem;
using Level;
using PacmanSystem;
using ScoreSystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private PacmanCollisionDetector _pacmanCollisionDetector;
        [SerializeField] private Pacman _pacman;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Ghost[] _ghosts;
        [SerializeField] private Timer _gameTimer;
        
        private LevelDataSO _levelDataSO;
        private GhostDataSO _ghostDataSO;
        private PacmanDataSO _pacmanDataSO;
        private ScoreCounter _scoreCounter;
        private GameStateMachine _gameStateMachine;
        private Health _pacmanHealth;
        
        private void Awake()
        {
            _levelDataSO = Resources.Load<LevelDataSO>("Level1DataSO");
            _ghostDataSO = Resources.Load<GhostDataSO>("GhostDataSO");
            _pacmanDataSO = Resources.Load<PacmanDataSO>("PacmanDataSO");
            _scoreCounter = new ScoreCounter(_levelDataSO.LevelData.MaxScore);
            _scoreView.Construct(_scoreCounter);
            _pacman.Construct(_pacmanDataSO.PacmanData.MoveSpeed);
            _inputListener.Construct(_pacman);
            _pacmanHealth = new Health(_pacmanDataSO.PacmanData.HeartsCount);
            _healthBar.Construct(_pacmanHealth,_pacmanDataSO.PacmanData.HeartsCount);
            List<IPacmanRevengeEffector> pacmanRevengeEffector = new List<IPacmanRevengeEffector>(_ghosts) { _pacmanCollisionDetector };
            AState[] gameStates =
            {
                new WinGameState(),
                new LooseGameState(_pacman,_ghosts, _healthBar, _gameTimer),
                new GameDefaultState(_scoreCounter),
                new PacmanRevengeGameState(pacmanRevengeEffector.ToArray()),
            };
            _gameStateMachine = new GameStateMachine(gameStates);
            _pacmanCollisionDetector.Construct(_scoreCounter,_pacmanHealth,_gameStateMachine,
                _levelDataSO.LevelData.BigBonusTime,_levelDataSO.LevelData.GhostKillScore,_levelDataSO.LevelData.BonusScore);
            
            AMovementState[] movementStates =
            {
                new NoMovementState(),
                new UncontrolledMovementState(),
                new RandomMovementState(),
                new SteadyMovementState(),
                new TargetedMovementState(_pacman.transform),
                new ScaredMovementState(_pacman.transform)
            };
            
            foreach (var ghost in _ghosts)
            {
                ghost.Construct(new MovementStateMachine(movementStates),_pacman.transform, _ghostDataSO.GhostData);
            }
        }
    }
}
