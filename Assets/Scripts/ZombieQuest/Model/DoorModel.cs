using UnityEngine;


namespace ZombieQuest
{
    
    internal sealed class DoorModel : MonoBehaviour, IInteractable
    {

        [SerializeField] private GameObject _door;
        
        [SerializeField] private DoorKeyType _keyTypeForOpenning = DoorKeyType.Finish;
        
        [SerializeField] private int _keyCount = 2;

        [SerializeField] private bool _isOpened = false;
        [SerializeField] private bool _isNeedKey = true;
        
        
        
        public int KeyCount => _keyCount;

        public bool IsOpened => _isOpened;

        public bool IsNeedKey => _isNeedKey;

        public DoorKeyType KeyType => _keyTypeForOpenning;


        
        public void Interact(GameObject other)
        {
            
        }


        public void Open()
        {
            _isOpened = true;
            print("Open");
        }

        
        public void Close()
        {
            _isOpened = false;
            print("Close");
        }
        
        
    }
}