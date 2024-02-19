using System;
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
        
        private LevelDataSO _levelDataSO;
        private GhostDataSO _ghostDataSO;
        private PacmanDataSO _pacmanDataSO;
        private ScoreCounter _scoreCounter;
        private GameStateMachine _gameStateMachine;
        private Health _pacmanHealth;
        
        private void Awake()
        {
            _gameStateMachine = new GameStateMachine();
            _levelDataSO = Resources.Load<LevelDataSO>("Level1DataSO");
            _ghostDataSO = Resources.Load<GhostDataSO>("GhostDataSO");
            _pacmanDataSO = Resources.Load<PacmanDataSO>("PacmanDataSO");
            _scoreCounter = new ScoreCounter(_levelDataSO.LevelData.MaxScore);
            _scoreView.Construct(_scoreCounter);
            _pacman.Construct(_pacmanDataSO.PacmanData.MoveSpeed);
            _inputListener.Construct(_pacman);
            _pacmanHealth = new Health(_pacmanDataSO.PacmanData.HeartsCount);
            _healthBar.Construct(_pacmanHealth,_pacmanDataSO.PacmanData.HeartsCount);
            _pacmanCollisionDetector.Construct(_scoreCounter,_pacmanHealth,_levelDataSO.LevelData.BonusScore);
            AMovementState[] movementStates =
            {
                new RandomMovementState(),
                new SteadyMovementState(),
                new TargetedMovementState(_pacman.transform),
                new ScaredMovementState(_pacman.transform)
            };
            foreach (var ghost in _ghosts)
            {
                Health ghostHealth = new Health(_ghostDataSO.GhostData.HeartsCount);
                ghost.Construct(new MovementStateMachine(movementStates), ghostHealth,_pacman.transform, _ghostDataSO.GhostData);
            }
        }
    }
}
