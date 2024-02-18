using Core;
using PathSystem;
using UnityEngine;

namespace GhostSystem
{
    public class ScaredMovementState : AMovementState
    {
        private readonly Transform _targetTransform;
        
        public ScaredMovementState(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }
        
        public override void Move(PathWalker pathWalker)
        {
            if (pathWalker.Walk())
            {
                PathNode nearestNode = pathWalker.CurrentNode.NearNodes[0];
                float nearestNodeDistance = Vector2.Distance(nearestNode.Point, _targetTransform.position);
                for (int i = 1; i < pathWalker.CurrentNode.NearNodes.Length; i++)
                {
                    float newDistance = Vector2.Distance(pathWalker.CurrentNode.NearNodes[i].Point, _targetTransform.position);
                    if (newDistance > nearestNodeDistance)
                    {
                        nearestNode = pathWalker.CurrentNode.NearNodes[i];
                        nearestNodeDistance = newDistance;
                    }
                }

                pathWalker.SetDirectPath(nearestNode);
            }
        }
        
        public override void Enter() { }
        public override void Exit() { }
    }
}
