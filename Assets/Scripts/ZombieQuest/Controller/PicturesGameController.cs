using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ZombieQuest
{
    
    internal sealed class PicturesGameController : MonoBehaviour
    {
        
        [SerializeField] private List<PictureModel> _pictures;


        private Character _player;

        private List<int> _keyCombination = new List<int>(3) {1, 2, 3};
        private List<int> _currentCombination = new List<int>(3);
        
        private KeyModel _handledKey;

        private bool _isHandleKey = true;


        public PicturesGameController()
        {
            _handledKey = new KeyModel(DoorKeyType.Finish);
        }


        public bool IsHandleKey
        {
            get => _isHandleKey;
        }


        public void SetCharacter(Character player)
        {
            _player ??= player;
        }


        public void AddInCombination(PictureModel picture)
        {
            var pos = _pictures.IndexOf(picture) + 1;

            _currentCombination.Add(pos);

            if (_currentCombination.Count.Equals(_currentCombination.Capacity))
            {
                if (IsCombinationRight())
                {
                    GiveKeyToCharacter();
                }
                _currentCombination.Clear();
            }
        }


        private bool IsCombinationRight()
        {
            return _currentCombination.SequenceEqual(_keyCombination);
        }


        private void GiveKeyToCharacter()
        {
            _player.Inventory().Keys.Add(_handledKey);
            _isHandleKey = false;
            print("Some peace of wall is moved and u find a KEY behind");
        }
        
        
    }
}