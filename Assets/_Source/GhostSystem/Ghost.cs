using System;
using Core;
using PacmanSystem;
using PathSystem;
using UnityEngine;

namespace GhostSystem
{
    public abstract class Ghost : MonoBehaviour
    {
        protected bool TargetIsInRange;
        [SerializeField] private PathNode _startNode;
        private float _targetDetectionRange;
        private bool _attackPacman;
        private LayerMask _targetLayerMask;
        private Transform _targetTransform;
        private PathWalker _pathWalker;
        private Health _ghostHealth;
        protected MovementStateMachine MovementStateMachine;
        private AMovementState _defaultMovementState;
        
        protected Action OnTargetRangeEnter;
        protected Action OnTargetRangeExit;
        
        public virtual void Construct(MovementStateMachine movementStateMachine, Health ghostHealth, Transform targetTransform, GhostData ghostData)
        {
            MovementStateMachine = movementStateMachine;
            _pathWalker = new PathWalker(transform,ghostData.MoveSpeed,_startNode);
            _ghostHealth = ghostHealth;
            _targetTransform = targetTransform;
            _targetDetectionRange = ghostData.TargetDetectionRange;
            _targetLayerMask = ghostData.TargetLayerMask;
            _attackPacman = true;
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
        
        public void EnableAttackGhost(bool enable)
        {
            _attackPacman = enable;
        }
        
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_targetLayerMask.Contains(other.gameObject.layer))
            {
                if (!_attackPacman)
                {
                    _ghostHealth.LooseHeart();
                }
            }
        }
    }
}
