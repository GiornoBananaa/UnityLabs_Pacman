using Core;
using PathSystem;


namespace GhostSystem
{
    public class UncontrolledMovementState : AMovementState
    {
        public override void Move(PathWalker pathWalker)
        {
            pathWalker.Walk();
        }
        
        public override void Enter() { }

        public override void Exit() { }
    }
}
