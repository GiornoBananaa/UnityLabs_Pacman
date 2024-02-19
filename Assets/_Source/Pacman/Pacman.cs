using System;
using PathSystem;
using UnityEngine;

namespace PacmanSystem
{
    public class Pacman: MonoBehaviour
    {
        [SerializeField] private PathNode _startNode;
        private PacmanMovement _pacmanMovement;

        public void Construct(float moveSpeed)
        {
            _pacmanMovement = new PacmanMovement(transform,moveSpeed,_startNode);
        }

        private void Update()
        {
            _pacmanMovement.Move();
        }

        public void SetDirection(Vector2 destination)
        {
            _pacmanMovement.SetDirection(destination);
        }
    }
}