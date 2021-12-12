using Characters;
using Controllers;
using UnityEngine;

namespace RotateStates
{
    public class Rotation : MonoBehaviour, IRotate
    {
        private static readonly int Direction = Animator.StringToHash("Direction");

        private Animator _animator;
        public Animator Animator { set => _animator ??= value; }
        
        public void Subscribe(IRotateEvent rotateEvent)
        {
            rotateEvent.Rotated += Rotate;
        }

        public void Unsubscribe(IRotateEvent rotateEvent)
        {
            rotateEvent.Rotated -= Rotate;
        }

        private void Rotate(Vector2 dir)
        {
            var angle = AngleParse(dir);
            _animator.SetFloat(Direction, angle);
        }

        private float AngleParse(Vector2 target)
        {
            Vector3 diff = target - Vector2.zero;
            diff.Normalize();
 
            var rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            return rotZ - 90;
        }
    }
}