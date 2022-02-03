using UnityEngine;


namespace ZombieQuest
{
    public class StandingState : GroundedState
    {
        
        private bool jump;
        private bool crouch;
        private bool melee;

        
        public StandingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        
        public override void Enter()
        {
            base.Enter();
            speed = character.MovementSpeed;
            rotationSpeed = character.RotationSpeed;
            crouch = false;
            jump = false;
            melee = false;
        }

        
        public override void HandleInput()
        {
            base.HandleInput();
            crouch = Input.GetButton("Fire3");
            jump = Input.GetButton("Jump");
            melee = Input.GetMouseButton(1);
        }
        

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (crouch)
            {
                stateMachine.ChangeState(character.ducking);
            }
            else if (jump)
            {
                stateMachine.ChangeState(character.jumping);
            }
            else if (melee)
            {
                stateMachine.ChangeState(character.fighting);
            }
            
        }
        
        
        
    }
}
