using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ZombieQuest
{
    
    internal sealed class DoorController: MonoBehaviour
    {
        
        [SerializeField] private List<DoorModel> _doors;

        private Character _player;


        public void SetCharacter(Character character)
        {
            _player ??= character;
        }
        

        public bool TryOpenDoor(DoorModel door)
        {
            if (door.IsNeedKey)
            {
                var doorKeyType = door.KeyType;
                var doorKeyCount = door.KeyCount;

                var allPlayerKeys = _player.Inventory().Keys;
                var playerKeysDefiniteType = allPlayerKeys.Count(e => e.KeyType.Equals(doorKeyType));

                if (playerKeysDefiniteType == 0)
                {
                    print("U dont have needed key");
                    
                }
                else if (playerKeysDefiniteType != doorKeyCount)
                {
                    print("Not Enough Keys");
                }
                else
                {
                    door.Open();
                }
            }
            else
            {
                door.Open();
            }
            
            return door.IsOpened;
        }

        public bool TryCloseDoor(DoorModel door)
        {
            door.Close();
            return door.IsOpened;
        }
        
    }
}