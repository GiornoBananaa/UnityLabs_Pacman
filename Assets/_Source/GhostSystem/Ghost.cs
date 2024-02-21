using System;
using System.Collections;
using Core;
using PacmanSystem;
using PathSystem;
using UnityEngine;

namespace GhostSystem
{
    public abstract class Ghost : MonoBehaviour, IPacmanRevengeEffector
    {
        protected bool TargetIsInRange;
        [SerializeField] private PathNode _startNode;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Collider2D _collider;
        private float _targetDetectionRange;
        private float _timeForHeal;
        private bool _isPacmanRevenge;
        private bool _goingBackOnBase;
        private Color _defaultColor;
        private Color _scaredColor;
        private Color _deathColor;
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
            _isPacmanRevenge = false;
            _scaredColor = ghostData.ScaredColor;
            _deathColor = ghostData.DeathColor;
            _defaultColor = _sprite.color;
            _timeForHeal = ghostData.TimeForHeal;
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
        
        public void EnablePacmanRevenge(bool enable)
        {
            _isPacmanRevenge = enable;
            if (enable && !_goingBackOnBase)
            {
                _sprite.color = _scaredColor;
                ChangeMovementState<ScaredMovementState>();
            }
            else if(!_goingBackOnBase)
            {
                SetDefaultState();
                _sprite.color = _defaultColor;
            }
        }
        
        private void CheckForTarget()
        {
            if(_isPacmanRevenge || _goingBackOnBase) return;
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
                if (_isPacmanRevenge)
                {
                    StartCoroutine(ReturningToBase());
                }
            }
        }

        private IEnumerator ReturningToBase()
        {
            _goingBackOnBase = true;
            ChangeMovementState<UncontrolledMovementState>();
            
            _pathWalker.SetDestination(_startNode,true);
            _collider.enabled = false;
            _sprite.color = _deathColor;

            while (_pathWalker.IsMoving)
            {
                yield return new WaitForEndOfFrame();
            }
            
            yield return new WaitForSeconds(_timeForHeal);
            
            if(!_isPacmanRevenge)
            {
                SetDefaultState();
                _sprite.color = _defaultColor;
            }
            else
            {
                ChangeMovementState<ScaredMovementState>();
                _sprite.color = _scaredColor;
            }
            _collider.enabled = true;
            _goingBackOnBase = false;
        }
    }
}
