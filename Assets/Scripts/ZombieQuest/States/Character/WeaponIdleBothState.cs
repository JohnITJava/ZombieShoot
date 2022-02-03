using UnityEngine;


namespace ZombieQuest
{
    public class WeaponIdleBothState : State
    {
        private int idleWeaponBoth = Animator.StringToHash("idleWeaponBoth");

        
        
        public WeaponIdleBothState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
            
        }
        
        
        
        
    }
}