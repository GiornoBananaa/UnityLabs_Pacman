namespace GhostSystem
{
    public class KillerGhost : Ghost
    {
        protected override void Start()
        {
            base.Start();
            OnTargetRangeEnter += ChangeMovementState<TargetedMovementState>;
            OnTargetRangeExit += SetDefaultState;
        }
        
        public override void SetDefaultState()
        {
            ChangeMovementState<RandomMovementState>();
            
        }
        
        private void OnDestroy()
        {
            OnTargetRangeEnter -= ChangeMovementState<TargetedMovementState>;
            OnTargetRangeExit -= SetDefaultState;
        }
    }
}