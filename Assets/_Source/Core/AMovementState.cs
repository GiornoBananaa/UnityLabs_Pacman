using PathSystem;

namespace Core
{
    public abstract class AMovementState: AState
    {
        public abstract void Move(PathWalker pathWalker);
    }
}