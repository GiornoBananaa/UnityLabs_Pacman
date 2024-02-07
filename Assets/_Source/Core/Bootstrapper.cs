using InputSystem;
using Pacman;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private PacmanMovement _pacmanMovement;
    
        private void Awake()
        {
            _inputListener.Construct(_pacmanMovement);
        }
    }
}
