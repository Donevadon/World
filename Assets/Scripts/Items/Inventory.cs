using Characters;
using TargetSystem;
using UnityEngine;

namespace Items
{
    public class Inventory :MonoBehaviour, IUseItem
    {
        private IUIItem[] _items = {
            new Item(),
            new Item1()
        };

        private byte _targetIndex = 0;
        public IItem UseItem()
        {
            return _items[_targetIndex].GetItem();
        }

        public void Left()
        {
            _targetIndex--;
            
            Debug.Log("target index: " + _targetIndex);
        }

        public void Right()
        {
            _targetIndex++;
            
            Debug.Log("target index: " + _targetIndex);
        }
    }

    public class Item : IUIItem, IItem
    {
        private readonly float _strong = 10;

        public Item()
        {
            
        }

        public IItem GetItem()
        {
            return this;
        }

        public void Use(IInteractionObject interactionObject)
        {
            switch (interactionObject)
            {
                case Tree tree:
                    tree.Fell(_strong);
                    break;
                default:
                    interactionObject.IdleUse();
                    break;
            }
        }
    }

    public class Item1 : IUIItem, IItem
    {
        public IItem GetItem()
        {
            return this;
        }

        public void Use(IInteractionObject interactionObject)
        {
            switch (interactionObject)
            {
                default:
                    interactionObject.IdleUse();
                    break;
            }
        }
    }

    public interface IItem
    {
        void Use(IInteractionObject interactionObject);
    }

    public interface IUIItem
    {
        IItem GetItem();
    }
}