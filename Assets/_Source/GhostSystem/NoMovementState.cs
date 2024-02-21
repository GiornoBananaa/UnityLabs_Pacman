using Core;
using PathSystem;

namespace GhostSystem
{
    public class NoMovementState : AMovementState
    {
        public override void Move(PathWalker pathWalker) { }
        
        public override void Enter() { }

        public override void Exit() { }
    }
}