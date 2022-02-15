using UnityEngine;


//TODO Change mouse pointer on smth interesting
// Finish animation slide movement


namespace ZombieQuest
{

    public class FightingState : GroundedState
    {

        private Camera _camera;

        private Quaternion _targetRotation;
        private Quaternion _currentRotation;

        private Vector3 _direction;
        private Vector3 _desiredForward;
        private Vector3 _mousePosition;

        private bool _isRightButtonPressing;

        private Ray _cameraRay;
        private Plane _groundPlane;
        private float _rayLength;
        private Vector3 _pointToLook;


        public FightingState(Character character, StateMachine stateMachine, Camera camera) : base(character, stateMachine)
        {
            speed = character.CrouchSpeed;
            rotationSpeed = character.RotationSpeed;
            _camera = camera;
            _groundPlane = new Plane(Vector3.up, Vector3.zero);
        }


        public FightingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
            speed = character.CrouchSpeed;
            rotationSpeed = character.RotationSpeed;
            _camera = Camera.current;
        }


        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(character.meleeParam, true);
        }


        public override void Exit()
        {
            base.Exit();
            character.SetAnimationBool(character.meleeParam, false);
        }


        public override void HandleInput()
        {
            base.HandleInput();
            _isRightButtonPressing = Input.GetMouseButton(1);

            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
            _direction.Normalize();

            PointToMouse();
        }


        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!_isRightButtonPressing)
            {
                stateMachine.ChangeState(character.standing);
            }
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            character.MoveSliding(_direction);
        }


        private void PointToMouse()
        {
            _cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (_groundPlane.Raycast(_cameraRay, out _rayLength))
            {
                _pointToLook = _cameraRay.GetPoint(_rayLength);
                _pointToLook.y = character.transform.position.y;
                Debug.DrawLine(_cameraRay.origin, _pointToLook, Color.cyan);
                
                Vector3 relativePos = _pointToLook - character.transform.position;
                var _pointRotation = Quaternion.FromToRotation(Vector3.forward, relativePos);
                
                character.transform.rotation = Quaternion.Slerp(
                    character.transform.rotation, 
                    _pointRotation, 
                    5.0f * Time.deltaTime);
            }
        }
        
        
    }
}