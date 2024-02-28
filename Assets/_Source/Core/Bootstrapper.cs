using System.Collections.Generic;
using Audio;
using BonusSystem;
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
        [SerializeField] private Timer _gameTimer;
        [SerializeField] private Ghost[] _ghosts;
        [SerializeField] private GameObject[] _bonuses;
        [SerializeField] private Transform[] _bigBonusSpawnPoints;
        [SerializeField] private SpecialBonusSpawner _specialBonusSpawner;
        [SerializeField] private SpecialBonusListView _specialBonusListView;
        [SerializeField] private AudioPlayer _audioPlayer;
        
        private LevelData _levelData;
        private GhostDataSO _ghostDataSO;
        private PacmanDataSO _pacmanDataSO;
        private AudioDataSO _audioDataSO;
        private ScoreCounter _scoreCounter;
        private GameStateMachine _gameStateMachine;
        private Health _pacmanHealth;
        private BonusPool<SpecialBonus> _specialBonusPool;
        private BonusPool<RectTransform> _specialBonusListPool;
        
        private void Awake()
        {
            _levelData = Resources.Load<LevelDataSO>("Level1DataSO").LevelData;
            _ghostDataSO = Resources.Load<GhostDataSO>("GhostDataSO");
            _pacmanDataSO = Resources.Load<PacmanDataSO>("PacmanDataSO");
            _audioDataSO = Resources.Load<AudioDataSO>("AudioDataSO");
            _audioPlayer.Construct(_audioDataSO.Sounds);
            _scoreCounter = new ScoreCounter(_levelData.MaxScore);
            _scoreView.Construct(_scoreCounter);
            _pacman.Construct(_pacmanDataSO.PacmanData.MoveSpeed);
            _inputListener.Construct(_pacman);
            _pacmanHealth = new Health(_pacmanDataSO.PacmanData.HeartsCount);
            _healthBar.Construct(_pacmanHealth,_pacmanDataSO.PacmanData.HeartsCount);
            _specialBonusPool = new BonusPool<SpecialBonus>(_levelData.SpecialBonusPrefab);
            _specialBonusSpawner.Construct(_specialBonusPool,_bigBonusSpawnPoints,_levelData.SpecialBonusSpawnCooldown,_levelData.SpecialBonusMaxCount);
            _specialBonusListPool = new BonusPool<RectTransform>(_levelData.SpecialBonusListIconPrefab);
            _specialBonusListView.Construct(_specialBonusSpawner,_specialBonusListPool);
            List<IPacmanRevengeEffector> pacmanRevengeEffector = new List<IPacmanRevengeEffector>(_ghosts) { _pacmanCollisionDetector };
            AState[] gameStates =
            {
                new RestartGameState(_pacman,_ghosts,_pacmanHealth,_scoreCounter,_bonuses,
                    _specialBonusSpawner,_specialBonusListView,_audioPlayer,_levelData.StartGameMusic),
                new WinGameState(),
                new LooseGameState(_pacman,_ghosts, _gameTimer),
                new LooseLiveGameState(_pacman,_ghosts, _gameTimer),
                new GameDefaultState(_scoreCounter, _audioPlayer),
                new PacmanRevengeGameState(_audioPlayer,_levelData.PacmanRevengeMusic,pacmanRevengeEffector.ToArray()),
            };
            _gameStateMachine = new GameStateMachine(gameStates);
            _pacmanCollisionDetector.Construct(_scoreCounter,_pacmanHealth,_gameStateMachine,_audioPlayer,
                _levelData.BigBonusTime,_levelData.GhostKillScore,_levelData.BonusScore,_levelData.SpecialBonusScore);
            
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

            _gameStateMachine.ChangeState<RestartGameState>();
        }
    }
}
