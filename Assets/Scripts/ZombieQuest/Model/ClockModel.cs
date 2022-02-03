using UnityEngine;


namespace ZombieQuest
{
    
    sealed class ClockModel : MonoBehaviour, IInteractable
    {
        
        private KeyModel _key;
        private int _keyCount;

        
        public ClockModel()
        {
            _key = new KeyModel(DoorKeyType.Finish);
            _keyCount += 1;
        }

        
        public void Interact(GameObject other)
        {
            if (_keyCount != 0)
            {
                Character character = null;
                other.TryGetComponent(out character);

                if (!(character is null))
                {
                    character.Inventory().Keys.Add(_key);
                    _keyCount -= 1;
                    print("U find a KEY behind OClock");
                }
            }
        }
        
        
    }
}