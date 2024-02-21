using PathSystem;
using UnityEngine;

namespace PacmanSystem
{
    public class Pacman: MonoBehaviour
    {
        [SerializeField] private PathNode _startNode;
        private PacmanMovement _pacmanMovement;
        private bool _movementIsEnabled;

        public void Construct(float moveSpeed)
        {
            _pacmanMovement = new PacmanMovement(transform,moveSpeed,_startNode);
            _movementIsEnabled = true;
        }

        private void Update()
        {
            if(_movementIsEnabled)
                _pacmanMovement.Move();
        }

        public void PlayDeathAnimation(float animationDuration)
        {
            
        }
        
        public void EnableMovement(bool enable)
        {
            _movementIsEnabled = enable;
        }
        
        public void SetDirection(Vector2 destination)
        {
            _pacmanMovement.SetDirection(destination);
        }
    }
}