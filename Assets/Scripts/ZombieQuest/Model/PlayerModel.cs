using UnityEngine;

namespace ZombieQuest
{
    internal class PlayerModel : IPlayer
    {
        private GameObject _preson;
        private PersonStats _personStats;
        private InventoryModel m_InventoryModel;

        public PlayerModel(GameObject person)
        {
            this._preson = person;
            _personStats = new PersonStats();
            m_InventoryModel = new InventoryModel();
        }
        
        
        public void Interact(GameObject other)
        {
            
        }

        public void Move()
        {
            
        }

        public void TryPutInventory(IItemable item)
        {
            m_InventoryModel.TryAddItem(item);
        }
    }
}