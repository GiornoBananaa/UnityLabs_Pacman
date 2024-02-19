using PathSystem;
using UnityEngine;

namespace PacmanSystem
{
    public class PacmanMovement
    {
        private const float _maxDistanceForDirectionSet = 0.3f;
        private const float _maxAngleForDirectionSet = 30;
        private PathWalker _pathWalker;
        private Transform _transform;
        private Vector2 _direction;
        private Vector2 _inputDirection;
        private bool _isMomentForTurn;

        public PacmanMovement(Transform transform, float moveSpeed,PathNode startNode)
        {
            _pathWalker = new PathWalker(transform,moveSpeed,startNode);
            _transform = transform;
            _inputDirection = _direction;
        }
        
        public void SetDirection(Vector2 direction)
        {
            if(_inputDirection==direction 
               || Vector2.Distance(_transform.position,_pathWalker.CurrentNode.Point)>_maxDistanceForDirectionSet) return;
            _inputDirection = direction;
            if (_isMomentForTurn)
                FindNearNodeOnDirection(direction);
            else
                TurnBack(direction);
        }

        public void Move()
        {
            if (_isMomentForTurn)
            {
                FindNearNodeOnDirection(_direction);
                _isMomentForTurn = false;
            }
            
            if (_pathWalker.Walk())
            {
                _isMomentForTurn = true;
            }
        }
        private bool TurnBack(Vector2 direction)
        {
            if (Mathf.Abs(Vector2.Angle(_pathWalker.PreviousNode.Point-(Vector2)_transform.position, direction)) < _maxAngleForDirectionSet)
            {
                _pathWalker.SetDirectPath(_pathWalker.PreviousNode);
                _direction = direction;
                return true;
            }

            return false;
        }
        private bool FindNearNodeOnDirection(Vector2 direction)
        {
            foreach (var node in _pathWalker.CurrentNode.NearNodes)
            {
                Vector2 nodeDirection = node.Point - (Vector2)_transform.position;
                if (Mathf.Abs(Vector2.Angle(nodeDirection, direction)) < _maxAngleForDirectionSet)
                {
                    _pathWalker.SetDirectPath(node);
                    _direction = direction;
                    return true;
                }
            }

            return false;
        }
    }
}
