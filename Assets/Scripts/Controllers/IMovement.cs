using Characters;
using UnityEngine;

namespace Controllers
{
    public interface IMovement : IRigidbody2DSetter, IAnimatorSetter
    {
        void Subscribe(IMoveEvent moveEvent);
        void Unsubscribe(IMoveEvent moveEvent);
    }
}