using System;
using Core;
using PathSystem;
using UnityEngine;

namespace GhostSystem
{
    public abstract class Ghost : MonoBehaviour
    {
        protected bool TargetIsInRange;
        [SerializeField] private PathNode _startNode;
        private float _targetDetectionRange;
        private LayerMask _targetLayerMask;
        private Transform _targetTransform;
        private PathWalker _pathWalker;
        protected MovementStateMachine MovementStateMachine;
        private AMovementState _defaultMovementState;
        
        protected Action OnTargetRangeEnter;
        protected Action OnTargetRangeExit;
        
        public virtual void Construct(MovementStateMachine movementStateMachine, Transform targetTransform, GhostData ghostData)
        {
            MovementStateMachine = movementStateMachine;
            _pathWalker = new PathWalker(transform,ghostData.MoveSpeed,_startNode);
            _targetTransform = targetTransform;
            _targetDetectionRange = ghostData.TargetDetectionRange;
            _targetLayerMask = ghostData.TargetLayerMask;
        }
        
        protected virtual void Start()
        {
            SetDefaultState();
        }
        
        protected virtual void Update()
        {
            MovementStateMachine.Move(_pathWalker);
            CheckForTarget();
        }
        
        public void ChangeMovementState<T>() where T: AMovementState
        {
            MovementStateMachine.ChangeState<T>();
        }

        public abstract void SetDefaultState();
        
        private void CheckForTarget()
        {
            if (_targetTransform==null)
            {
                var targetCollider = Physics2D.OverlapCircle(transform.position, _targetDetectionRange,_targetLayerMask);
                if (targetCollider != null)
                {
                    _targetTransform = targetCollider.transform;
                    OnTargetRangeEnter?.Invoke();
                    TargetIsInRange = true;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position,_targetTransform.position)>_targetDetectionRange)
                {
                    _targetTransform = null;
                    OnTargetRangeExit?.Invoke();
                    TargetIsInRange = false;
                }
            }
        }
    }
}
