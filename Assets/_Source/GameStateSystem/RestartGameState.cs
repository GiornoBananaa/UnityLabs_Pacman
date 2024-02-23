using System;
using Core;
using GhostSystem;
using PacmanSystem;

namespace GameStateSystem
{
    public class RestartGameState: AState
    {
        private Pacman _pacman;
        private Health _health;
        private Ghost[] _ghosts;
        
        public RestartGameState(Pacman pacman, Ghost[] ghosts, Health health)
        {
            _pacman = pacman;
            _ghosts = ghosts;
            _health = health;
        }
        
        public override void Enter()
        {
            _pacman.EnableMovement(true);
            _pacman.SetDefaultPosition();
            foreach (var ghost in _ghosts)
            {
                ghost.SetDefaultPosition();
            }
            _health.RestoreHealth();

            Owner.ChangeState<GameDefaultState>();
        }
        
        public override void Exit() { }
    }
}