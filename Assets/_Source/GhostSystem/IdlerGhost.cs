namespace GhostSystem
{
    public class IdlerGhost : Ghost
    {
        public override void SetDefaultState()
        {
            ChangeMovementState<RandomMovementState>();
        }
    }
}