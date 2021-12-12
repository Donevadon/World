using Characters;
using UnityEngine;

namespace Controllers
{
    internal interface IRotate : IAnimatorSetter
    {
        void Subscribe(IRotateEvent rotateEvent);
        void Unsubscribe(IRotateEvent rotateEvent);
    }
}