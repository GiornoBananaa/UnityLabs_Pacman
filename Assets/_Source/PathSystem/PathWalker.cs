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
        private float _elapsedTime;
        private PathNode _previousNode;
        
        public PathNode CurrentNode => _currentNode;
        public PathNode PreviousNode => _previousNode;
        
        public PathWalker(Transform transform,float moveSpeed, PathNode currentNode)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            _currentNode = currentNode;
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
                    return true;
                SetCurrentPathNode(_path.Dequeue());
            }
            
            return false;
        }
        
        public void SetDestination(PathNode node)
        {
            _path = FindPath(_currentNode,node.Point);
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
            PathNode nearestNode = CurrentNode.NearNodes[0];
            float nearestNodeDistance = Vector2.Distance(nearestNode.Point, position);
            for (int i = 1; i < CurrentNode.NearNodes.Length; i++)
            {
                float newDistance = Vector2.Distance(CurrentNode.NearNodes[i].Point, position);
                if (newDistance < nearestNodeDistance)
                {
                    nearestNode = CurrentNode.NearNodes[i];
                    nearestNodeDistance = newDistance;
                }
            }
            if(clearPath)
                _path.Clear();
            SetCurrentPathNode(nearestNode);
        }
        
        private void SetCurrentPathNode(PathNode node)
        {
            _elapsedTime = 0;
            _previousNode = _currentNode;
            _currentNode = node;
        }
        
        private Queue<PathNode> FindPath(PathNode start,Vector2 destination)
        {
            HashSet<PathNode> closedSet = new HashSet<PathNode>();
            Queue<PathNode> path = new Queue<PathNode>();
            PathNode nearestNode = start;
            while (true)
            {
                float currentDistance = Vector2.Distance(nearestNode.Point, destination);
                bool foundNextNode = false;
                for (int i = 1; i < nearestNode.NearNodes.Length; i++)
                {
                    if(closedSet.Contains(nearestNode.NearNodes[i]))
                        continue;
                    closedSet.Add(nearestNode.NearNodes[i]);
                    
                    float newDistance = Vector2.Distance(nearestNode.NearNodes[i].Point, destination);
                    if (newDistance < currentDistance)
                    {
                        nearestNode = nearestNode.NearNodes[i];
                        currentDistance = newDistance;
                        foundNextNode = true;
                    }
                }
                
                if (!foundNextNode)
                    nearestNode = nearestNode.NearNodes[0];
                
                path.Enqueue(nearestNode);
                
                if (nearestNode.Point == destination)
                {
                    return path;
                }
            }
        }
    }
}