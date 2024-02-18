using PathSystem;
using UnityEngine;

namespace GhostSystem
{
    public class PatrolGhost : Ghost
    {
        [SerializeField] private PathNode[] _checkPoints;
        public override void SetDefaultState()
        {
            ChangeMovementState<SteadyMovementState>();
            ((SteadyMovementState)MovementStateMachine.CurrentState).SetPathCheckPoints(_checkPoints);
        }
    }
}