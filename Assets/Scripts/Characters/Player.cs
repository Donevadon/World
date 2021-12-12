using System;
using Controllers;
using Items;
using UnityEngine;

namespace Characters
{
    public class Player : MonoBehaviour, IRotateEvent, IMoveEvent, IPlayerControl, IPlayer
    {
        [SerializeField] private Vector2 direction = Vector2.zero;
        
        public event Action<Vector2> Rotated;
        public event Action<Vector2> Moved;

        private IRotate _rotate;
        private IMovement _movement;
        private IUseItem _useItem;
        private ITarget _target;
        

        public Vector2 Position => transform.position;
        public Vector2 Direction
        {
            get => direction;
            private set => direction = value;
        }

        private void Awake()
        {
            InitStates();
        }

        private void InitStates()
        {
            var rb = GetComponent<Rigidbody2D>();
            var anim = GetComponentInChildren<Animator>();
            InitRotate(anim);
            InitUseItem();
            InitMovement(rb, anim);
            InitTarget();
        }

        private void InitRotate(Animator anim)
        {
            try
            {
                _rotate = GetComponent<IRotate>() ?? throw new NullReferenceException("Не установлен поворот");
                _rotate.Animator = anim;
                _rotate.Subscribe(this);
            }
            catch (Exception e)
            {
                print(e);
            }
        }

        private void InitTarget()
        {
            try
            {
                _target = GetComponentInChildren<ITarget>() ?? throw new NullReferenceException("Не найден таргет");
                _target.Subscribe(this);
            }
            catch (Exception e)
            {
                print(e);
            }
        }

        private void InitMovement(Rigidbody2D rb, Animator anim)
        {
            try
            {
                _movement = GetComponent<IMovement>() ?? throw new NullReferenceException("Не установлено движение");
                _movement.Rigidbody2D = rb;
                _movement.Animator = anim;
                _movement.Subscribe(this);
            }
            catch (Exception e)
            {
                print(e);
            }
        }

        private void InitUseItem()
        {
            try
            {
                _useItem = GetComponent<IUseItem>() ?? throw new NullReferenceException("Не установлено использование предмета");
            }
            catch (Exception e)
            {
                print(e);
            }
        }

        public void Move(Vector2 direction)
        {
            Moved?.Invoke(direction);
        }

        public void RotateX(float direction)
        {
            Direction = new Vector2(direction, Direction.y);
            Rotated?.Invoke(Direction);
        }

        public void RotateY(float direction)
        {
            Direction = new Vector2(Direction.x, direction);
            Rotated?.Invoke(Direction);
        }

        public void UseItem()
        {
            var item = _useItem.UseItem();
            _target.SetItem(item);
            Debug.Log("Use");
        }

        public void UILeft()
        {
            _useItem.Left();
        }

        public void UIRight()
        {
            _useItem.Right();
        }

    }

    internal interface ITarget
    {
        void Subscribe(IRotateEvent player);
        void SetItem(IItem item);
    }

    public interface IPlayer
    {
        Vector2 Position { get; }
        Vector2 Direction { get; }
    }

    internal interface IUseItem
    {
        IItem UseItem();
        void Left();
        void Right();
    }

    public interface IRotateEvent
    {
        event Action<Vector2> Rotated;
    }

    public interface IMoveEvent
    {
        event Action<Vector2> Moved;
    }
}
