using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Pacman
{
    public class PacmanMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;

        private Vector3 _direction;
        
        public void SetDirection(Vector2 direction)
        {
            if(Math.Abs(direction.x) == Math.Abs(direction.y)
               /*|| CheckForWall(direction)*/) return;
            _direction = direction;
             _rigidbody.transform.right = direction;
        }
        
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _rigidbody.velocity = _direction * _speed;
        }
        
        private bool CheckForWall(Vector2 direction)
        {
            var pacmanScale = transform.lossyScale;
            return Physics2D.BoxCast((Vector2)(transform.position) + direction,  
                new Vector2(pacmanScale.x * 0.95f, pacmanScale.y*0.1f),
                0,Vector2.zero);
        }
    }
}
