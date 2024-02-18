using Core;
using PathSystem;
using UnityEngine;

namespace GhostSystem
{
    public class SteadyMovementState : AMovementState
    {
        private PathNode[] _checkPoints;
        private int _checkPointIndex;
        
        public SteadyMovementState(PathNode[] checkPoints = null)
        {
            _checkPoints = checkPoints;
        }

        public void SetPathCheckPoints(PathNode[] checkPoints)
        {
            _checkPoints = checkPoints;
        }
        
        public override void Move(PathWalker pathWalker)
        {
            if (pathWalker.Walk())
            {
                pathWalker.SetDestination((_checkPoints[_checkPointIndex]));
                _checkPointIndex++;
                if (_checkPointIndex >= _checkPoints.Length)
                    _checkPointIndex = 0;
            }
        }

        public override void Enter()
        { }

        public override void Exit()
        { }
    }
}