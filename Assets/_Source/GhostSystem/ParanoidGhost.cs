using PacmanSystem;
using UnityEngine;

namespace GhostSystem
{
    public class ParanoidGhost : KillerGhost
    {
        private float _minStateChangeTime;
        private float _maxStateChangeTime;
        private bool _attacksPlayer;
        private float _timeForStateChange;
        
        public override void Construct(MovementStateMachine movementStateMachine, Transform targetTransform, GhostData ghostData)
        {
            base.Construct(movementStateMachine, targetTransform, ghostData);
            _minStateChangeTime = ghostData.MinStateChangeTime;
            _maxStateChangeTime = ghostData.MaxStateChangeTime;
        }
        
        protected override void Update()
        {
            base.Update();
            CheckForStateChange();
        }
        
        private void CheckForStateChange()
        {
            _timeForStateChange -= Time.deltaTime;
            if (_timeForStateChange <= 0 && TargetIsInRange)
            {
                _timeForStateChange = Random.Range(_minStateChangeTime,_maxStateChangeTime);
                _attacksPlayer = !_attacksPlayer;
                if(_attacksPlayer)
                    ChangeMovementState<TargetedMovementState>();
                else
                    ChangeMovementState<ScaredMovementState>();
            }
        }
    }
}