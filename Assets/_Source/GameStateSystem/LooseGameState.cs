using Core;
using GhostSystem;
using PacmanSystem;
using UnityEngine;

namespace GameStateSystem
{
    public class LooseGameState: AState
    {
        private const float _deathAnimationDuration = 2;
        
        private Pacman _pacman;
        private HealthBar _healthBar;
        private Ghost[] _ghosts;
        private Timer _gameTimer;
        
        public LooseGameState(Pacman pacman, Ghost[] ghosts, HealthBar healthBar, Timer gameTimer)
        {
            _pacman = pacman;
            _ghosts = ghosts;
            _healthBar = healthBar;
            _gameTimer = gameTimer;
        }
        
        public override void Enter()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.ChangeMovementState<NoMovementState>();
            }
            _gameTimer.SetTimer(2);
            _gameTimer.OnTimerEnd += PlayAnimations;
            _pacman.EnableMovement(false);
        }
        
        public override void Exit()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.SetDefaultState();
            }
            
            _pacman.EnableMovement(true);
        }
        
        private void PlayAnimations()
        {
            _gameTimer.SetTimer(_deathAnimationDuration);
            _gameTimer.OnTimerEnd -= PlayAnimations;
            _gameTimer.OnTimerEnd += RestartGame;
            _pacman.PlayDeathAnimation(_deathAnimationDuration);
            _healthBar.PlayDeathAnimation(_deathAnimationDuration);
        }
        
        private void RestartGame()
        {
            _gameTimer.OnTimerEnd -= RestartGame;
            Owner.ChangeState<RestartGameState>();
        }
    }
}