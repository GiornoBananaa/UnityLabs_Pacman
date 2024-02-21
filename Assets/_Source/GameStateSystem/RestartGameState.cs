using System;
using Core;
using GhostSystem;
using PacmanSystem;

namespace GameStateSystem
{
    public class RestartGameState: AState
    {
        private Pacman _pacman;
        private HealthBar _healthBar;
        private Ghost[] _ghosts;
        
        public RestartGameState(Pacman pacman, Ghost[] ghosts, HealthBar healthBar)
        {
            _pacman = pacman;
            _ghosts = ghosts;
            _healthBar = healthBar;
        }
        
        public override void Enter()
        {
            throw new NotImplementedException();
        }
        
        public override void Exit()
        {
            
        }
    }
}