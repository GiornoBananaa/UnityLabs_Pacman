using Core;
using PacmanSystem;
using UnityEngine;

namespace GameStateSystem
{
    public class PacmanRevengeGameState: AState
    {
        private IPacmanRevengeEffector[] _pacmanRevengeEffector;
        
        public PacmanRevengeGameState(params IPacmanRevengeEffector[] pacmanRevengeEffector)
        {
            _pacmanRevengeEffector = pacmanRevengeEffector;
        }
        
        public override void Enter()
        {
            foreach (var effector in _pacmanRevengeEffector)
            {
                effector.EnablePacmanRevenge(true);
            }
        }
        
        public override void Exit()
        {
            foreach (var effector in _pacmanRevengeEffector)
            {
                effector.EnablePacmanRevenge(false);
            }
        }
    }
}