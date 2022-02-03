using UnityEngine;


namespace ZombieQuest
{
    
    public class ItemModel : MonoBehaviour, IInteractable, IItemable
    {
        
        [SerializeField] private GameObject _item;
        [SerializeField] private bool _isEnabled;
        [SerializeField] private bool _isEquiped;
        [SerializeField] private bool _isInInventory;
        
        [SerializeField] private ItemType _ItemType; 
        
        
        
        public void StartProcess()
        {
            if (!_item.Equals(null))
            {
                _item.SetActive(false);
            }
        }

        public void TakeInInventory()
        {
            if (!_isInInventory)
            {
                Disable();
                Destroy(_item);
                _isInInventory = true;
            }
        }

        public void Equip(GameObject hand)
        {
            Enable();
            Instantiate(_item, hand.transform.position, hand.transform.rotation);
            _isEquiped = true;
        }

        public void UseEquiped()
        {
        }


        public void Interact(GameObject other)
        {
        }


        public void Enable()
        {
            if (!_item.Equals(null))
            {
                _item.SetActive(true);
                _isEnabled = true;
            }
        }

        public void Disable()
        {
            if (!_item.Equals(null))
            {
                _item.SetActive(false);
                _isEnabled = false;
            }
        }
        

        // public void Interact(GameObject other)
        // {
        //     InteractDueItemStates();
        // }

        private void InteractDueItemStates()
        {
            if (!_isEquiped)
            {
                switch (_ItemType)
                {
                    case ItemType.HealerPoison:
                        break;
                    case ItemType.Weapon:
                        break;
                }
            }

            if (_isEquiped)
            {
                switch (_ItemType)
                {
                    case ItemType.HealerPoison:
                        break;
                }
            }
        }
        
        
        public override bool Equals(object other)
        {
            ItemModel otherItem = other as ItemModel;

            return otherItem._item.Equals(_item);
        }

        
    }
}