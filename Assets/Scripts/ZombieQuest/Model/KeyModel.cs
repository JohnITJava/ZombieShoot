namespace ZombieQuest
{
    public sealed class KeyModel
    {
        private DoorKeyType _keyType;

        public KeyModel(DoorKeyType keyType)
        {
            this._keyType = keyType;
        }
        
        public DoorKeyType KeyType => _keyType;
    }
}