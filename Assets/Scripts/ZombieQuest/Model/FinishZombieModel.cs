using UnityEngine;


namespace ZombieQuest
{
    
    internal sealed class FinishZombieModel: ZombieModel
    {
        
        [SerializeField] private float attackPower2;
        [SerializeField] private float attackPower3;

        
        public FinishZombieModel() : base() {}


        public void Attack2()
        {
            print($"Attack on {attackPower2}");
        }

        public void Attack3()
        {
            print($"Attack on {attackPower3}");
        }
        
    }
}