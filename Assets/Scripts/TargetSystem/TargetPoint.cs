using System;
using Characters;
using Items;
using UnityEngine;

namespace TargetSystem
{
    public class TargetPoint : MonoBehaviour, ITarget
    {
        private Collider2D _collider;
        
        public void Subscribe(IRotateEvent player)
        {
            player.Rotated += vector2 =>
            {
                transform.localPosition = vector2;
            };
        }

        public void SetItem(IItem item)
        {
            if(_collider != null && _collider.TryGetComponent<IInteractionObject>(out var interactionObject))
                item.Use(interactionObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_collider == null)
                _collider = other;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_collider && _collider.Equals(other))
            {
                _collider = null;
            }
        }
    }

    public interface IInteractionObject
    {
        void IdleUse();
    }
}