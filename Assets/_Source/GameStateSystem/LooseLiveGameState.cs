using Core;
using GhostSystem;
using PacmanSystem;

namespace GameStateSystem
{
    public class LooseLiveGameState: AState
    {
        private const float _deathAnimationDuration = 2;
        
        private Pacman _pacman;
        private Ghost[] _ghosts;
        private Timer _gameTimer;
       
        public LooseLiveGameState(Pacman pacman, Ghost[] ghosts, Timer gameTimer)
        {
            _pacman = pacman;
            _ghosts = ghosts;
            _gameTimer = gameTimer;
        }
        
        public override void Enter()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.ChangeMovementState<NoMovementState>();
            }
            
            _pacman.EnableMovement(false);
            _gameTimer.SetTimer(_deathAnimationDuration);
            _gameTimer.OnTimerEnd += Respawn;
            _pacman.PlayDeathAnimation(_deathAnimationDuration,0.3f);
        }
        
        public override void Exit()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.SetDefaultState();
                ghost.SetDefaultPosition();
            }
            
            _pacman.SetDefaultPosition();
            _pacman.EnableMovement(true);
        }
        
        private void Respawn()
        {
            _gameTimer.OnTimerEnd -= Respawn;
            Owner.ChangeState<GameDefaultState>();
        }
    }
}