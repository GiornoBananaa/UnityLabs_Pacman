using Core;
using GhostSystem;
using PacmanSystem;
using UI;
using UnityEngine;

namespace GameStateSystem
{
    public class LooseGameState: AState
    {
        private const float _deathAnimationDuration = 2;
        
        private Pacman _pacman;
        private Ghost[] _ghosts;
        private Timer _gameTimer;
        private EndGamePanel _lossPanel;
       
        public LooseGameState(Pacman pacman, Ghost[] ghosts, Timer gameTimer,EndGamePanel lossPanel)
        {
            _pacman = pacman;
            _ghosts = ghosts;
            _gameTimer = gameTimer;
            _lossPanel = lossPanel;
        }
        
        public override void Enter()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.ChangeMovementState<NoMovementState>();
            }
            
            _pacman.EnableMovement(false);
            _gameTimer.SetTimer(_deathAnimationDuration);
            _gameTimer.OnTimerEnd += OpenLossPanel;
            _pacman.PlayDeathAnimation(_deathAnimationDuration,0.3f);
        }
        
        public override void Exit()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.SetDefaultState();
            }
            _lossPanel.ClosePanel();
            _pacman.EnableMovement(true);
        }
        
        private void OpenLossPanel()
        {
            _gameTimer.OnTimerEnd -= OpenLossPanel;
            _lossPanel.OpenPanel();
        }
    }
}