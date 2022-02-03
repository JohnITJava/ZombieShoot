using UnityEngine;

namespace ZombieQuest
{
    public interface IItemable
    {
        void TakeInInventory();
        
        void UseEquiped();

        void Enable();

        void Disable();
        
    }
}