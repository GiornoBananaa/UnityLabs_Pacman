using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathSystem
{
    public class PathWalker
    {
        private readonly float _moveSpeed;
        private readonly Transform _transform;
        private Queue<PathNode> _path;
        private PathNode _currentNode;
        private PathNode _previousNode;
        
        public PathNode CurrentNode => _currentNode;
        public PathNode PreviousNode => _previousNode;
        public bool IsMoving { get; private set; }

        public PathWalker(Transform transform,float moveSpeed, PathNode currentNode)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            _currentNode = currentNode;
            _previousNode = _currentNode;
            _path = new Queue<PathNode>();
        }
        
        public bool Walk()
        {
            if (Vector3.Distance(_transform.position, _currentNode.Point) != 0)
            {
                _transform.position =
                    Vector3.MoveTowards(_transform.position, _currentNode.Point, Time.deltaTime * _moveSpeed);
            }
            else
            {
                if (_path.Count == 0)
                {
                    IsMoving = false;
                    CheckTeleportation();
                    return true;
                }
                SetCurrentPathNode(_path.Dequeue());
            }
            
            return false;
        }
        
        public void SetDestination(PathNode node,bool fullAccess = false)
        {
            _path = FindPath(_currentNode,node.Point,fullAccess);
            SetCurrentPathNode(_path.Peek());
        }
        
        public void SetDestination(Vector2 destination)
        {
            _path = FindPath(_currentNode,destination);
            SetCurrentPathNode(_path.Peek());
        }
        
        public void SetDirectPath(PathNode node)
        {
            _path.Clear();
            SetCurrentPathNode(node);
        }

        public void SetDirectPath(Vector2 position, bool clearPath = true)
        {
            PathNode nearestNode = null;
            float currentNearestDistance = 100;
            
            foreach (var pathNode in CurrentNode.NearNodes)
            {
                if(pathNode.IsBlocked)
                    continue;
                    
                float newDistance = Vector2.Distance(pathNode.Point, position);
                if (nearestNode == null || newDistance < currentNearestDistance)
                {
                    nearestNode = pathNode;
                    currentNearestDistance = newDistance;
                }
            }
            
            if(clearPath)
                _path.Clear();
            SetCurrentPathNode(nearestNode);
        }
        
        private void SetCurrentPathNode(PathNode node)
        {
            IsMoving = true;
            _previousNode = _currentNode;
            _currentNode = node;
        }

        private Queue<PathNode> FindPath(PathNode start, Vector2 destination, bool fullAccess = false)
        {
            HashSet<PathNode> closedSet = new HashSet<PathNode>();
            Queue<PathNode> path = new Queue<PathNode>();
            PathNode nearestNode = start;
            while (true)
            {
                PathNode currentNearestNode = null;
                float currentNearestDistance = 100;
                
                for (int i = 0; i < nearestNode.NearNodes.Length; i++)
                {
                    if(closedSet.Contains(nearestNode.NearNodes[i]) || (!fullAccess && nearestNode.NearNodes[i].IsBlocked))
                        continue;
                    closedSet.Add(nearestNode.NearNodes[i]);
                    
                    float newDistance = Vector2.Distance(nearestNode.NearNodes[i].Point, destination);
                    if (currentNearestNode == null || newDistance < currentNearestDistance)
                    {
                        currentNearestNode = nearestNode.NearNodes[i];
                        currentNearestDistance = newDistance;
                    }
                }
                
                if(currentNearestNode == null)
                {
                    throw new Exception("Path not found");
                }

                nearestNode = currentNearestNode;
                path.Enqueue(nearestNode);
                
                if (nearestNode.Point == destination)
                {
                    return path;
                }
            }
        }
        
        private void CheckTeleportation()
        {
            if(CurrentNode.NodeTeleport == null) return;
            _transform.position = CurrentNode.NodeTeleport.Point;
            SetCurrentPathNode(CurrentNode.NodeTeleport);
        }
    }
}