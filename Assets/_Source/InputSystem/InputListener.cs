using System;
using Pacman;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class InputListener : MonoBehaviour
    {
        private GameInputAction _inputAction;
        private PacmanMovement _pacmanMovement;
        
        public void Construct(PacmanMovement pacmanMovement)
        {
            _pacmanMovement = pacmanMovement;
        }

        private void Awake()
        {
            _inputAction = new();
            EnableReadingInput();
        }

        private void EnableReadingInput()
        {
            _inputAction.GlobalActionMap.Move.performed += OnMoveInput;
            _inputAction.Enable();
        }
        
        private void DisableReadingInput()
        {
            _inputAction.GlobalActionMap.Move.performed -= OnMoveInput;
            _inputAction.Disable();
        }
        
        private void OnMoveInput(InputAction.CallbackContext context)
        {
            _pacmanMovement.SetDirection(context.ReadValue<Vector2>());
        }

        private void ListenPause()
        {
            
        }

        private void OnDestroy()
        {
            DisableReadingInput();
        }
    }
}
