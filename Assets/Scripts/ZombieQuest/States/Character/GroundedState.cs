using UnityEngine;


namespace ZombieQuest
{
    
    public class GroundedState : State
    {

        private Vector3 _direction;
        private Vector3 _desiredForward;
        
        protected float speed;
        protected float rotationSpeed;

        private float horizontalInput;
        private float verticalInput;
        

        public GroundedState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
            
        }
        

        public override void Enter()
        {
            base.Enter();
            horizontalInput = verticalInput = 0.0f;
        }

        
        public override void Exit()
        {
            base.Exit();
            character.ResetMoveParams();
        }

        
        public override void HandleInput()
        {
            base.HandleInput();

            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
            _direction.Normalize();

            _desiredForward = Vector3.RotateTowards(
                character.transform.forward,
                _direction,
                character.RotationSpeed * Time.deltaTime,
                0.0f);
            character.transform.rotation = Quaternion.LookRotation(_desiredForward);
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            character.Move(_direction);
        }
        
        
    }
}