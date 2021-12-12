using System;
using Characters;
using Controllers;
using UnityEngine;

namespace MoveStates
{
    public class Movement : MonoBehaviour, IMovement
    {
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");

        [SerializeField] private float speed;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private bool _isMoving;

        private bool IsMoving
        {
            get => _isMoving;
            set
            {
                if (_isMoving == value) return;
                _isMoving = value;
                _animator.SetBool(IsMovingHash, _isMoving);
            }
        }

        public Rigidbody2D Rigidbody2D { set => _rigidbody2D ??= value; }
        public Animator Animator {set => _animator ??= value;}

        public void Subscribe(IMoveEvent moveEvent)
        {
            moveEvent.Moved += Move;
        }

        public void Unsubscribe(IMoveEvent moveEvent)
        {
            moveEvent.Moved -= Move;
        }

        private void Move(Vector2 direction)
        {
            IsMoving = direction != Vector2.zero;
            _rigidbody2D.velocity = IsMoving 
                ? new Vector2(direction.x * speed, direction.y * speed) 
                : Vector2.zero;
        }
    }
}