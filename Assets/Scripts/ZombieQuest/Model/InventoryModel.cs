using System.Collections;
using System.Collections.Generic;


namespace ZombieQuest
{
    public sealed class InventoryModel
    {
        
        private List<IItemable> _personItems;
        private bool _isFull;
        private List<KeyModel> _keys;

        
        public InventoryModel()
        {
            _personItems = new List<IItemable>(2);
            _keys = new List<KeyModel>();
        }


        public List<KeyModel> Keys => _keys;


        public IItemable GetNext()
        {
            if(!_personItems.GetEnumerator().MoveNext())
            {
                ((IEnumerator) _personItems.GetEnumerator()).Reset();
            }

            return _personItems.GetEnumerator().Current;
        }

        public IItemable GetCurrent()
        {
            return _personItems.GetEnumerator().Current;
        }

        public bool TryAddItem(IItemable item)
        {
            if (!IsFilled())
            {
                _personItems.Add(item);
                return true;
            }

            return false;
        }

        public bool IsFilled()
        {
            return _personItems.Count >= _personItems.Capacity;
        }
    }
}