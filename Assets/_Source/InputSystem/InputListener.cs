using PacmanSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class InputListener : MonoBehaviour
    {
        private GameInputAction _inputAction;
        private Pacman _pacman;
        private bool _pacmanIsMoving;
        
        public void Construct(Pacman pacman)
        {
            _pacman = pacman;
        }

        private void Awake()
        {
            _inputAction = new();
            EnableReadingInput();
        }

        private void Update()
        {
            OnMoveInput();
        }

        private void EnableReadingInput()
        {
            _inputAction.GlobalActionMap.Move.started += StartMove;
            _inputAction.GlobalActionMap.Move.canceled += EndMove;
            _inputAction.Enable();
        }
        
        private void DisableReadingInput()
        {
            _inputAction.GlobalActionMap.Move.started -= StartMove;
            _inputAction.GlobalActionMap.Move.canceled -= EndMove;
            _inputAction.Disable();
        }
        
        private void StartMove(InputAction.CallbackContext context)
        {
            _pacmanIsMoving = true;
        }
        
        private void EndMove(InputAction.CallbackContext context)
        {
            _pacmanIsMoving = false;
        }
        
        private void OnMoveInput()
        {
            if(_pacmanIsMoving)
                _pacman.SetDirection(_inputAction.GlobalActionMap.Move.ReadValue<Vector2>());
        }
        
        private void OnDestroy()
        {
            DisableReadingInput();
        }
    }
}
