using Core;
using PathSystem;
using UnityEngine;

namespace GhostSystem
{
    public class RandomMovementState : AMovementState
    {
        public override void Move(PathWalker pathWalker)
        {
            if (pathWalker.Walk())
            {
                PathNode nextNode = pathWalker.PreviousNode;
                while (nextNode == pathWalker.PreviousNode)
                {
                    int randomIndex = Random.Range(0, pathWalker.CurrentNode.NearNodes.Length);
                    nextNode = pathWalker.CurrentNode.NearNodes[randomIndex];
                }
                pathWalker.SetDirectPath(nextNode);
            }
        }
        
        public override void Enter() { }

        public override void Exit() { }
    }
}