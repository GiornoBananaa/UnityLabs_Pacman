using Core;
using PathSystem;
using UnityEngine;

namespace GhostSystem
{
    public class TargetedMovementState : AMovementState
    {
        private readonly Transform _targetTransform;

        public TargetedMovementState(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }


        public override void Move(PathWalker pathWalker)
        {
            if (pathWalker.Walk())
            {
                pathWalker.SetDirectPath(_targetTransform.position);
            }
        }

        public override void Enter()
        { }

        public override void Exit()
        { }
    }
}
