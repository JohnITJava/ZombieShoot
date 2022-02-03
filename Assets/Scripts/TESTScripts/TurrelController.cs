using System;
using UnityEngine;
using ZombieQuest;


namespace TestScripts
{
    
    internal sealed class TurrelController : MonoBehaviour
    {
        
        #region Fields

        
        [SerializeField] private GameObject _turrel;
        [SerializeField] private Transform _turrelCurrentTransform;
        [SerializeField] private GameObject _turrelBase;
        [SerializeField] private GameObject _turrelHorizontalRotator;
        [SerializeField] private GameObject _turrelVertivalRotator;
        [SerializeField] private GameObject _turrelBarrel;
        
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _playerCurrentTransform;

        [SerializeField] private float _radiusAtack;
        [SerializeField] private float _baseRotateSpeed = 1.0f;
        [SerializeField] private float _barrelRotateSpeed = 500.0f;
        [SerializeField] private Collider[] collsInRange;

        [SerializeField] private Vector3 _turrelHorizontalDirection;
        [SerializeField] private Vector3 _turrelVerticalDirection;

        [SerializeField] private LayerMask _playerMask;

        [SerializeField] private bool _isPlayerInRange;
        [SerializeField] private bool _isAimedOnPlayer;

        private Vector3 _horizontalRelativePosition;
        private Vector3 _verticalRelativePosition;

        
        #endregion



        #region UnityMethods

        
        private void Start()
        {
            _playerMask = LayerMask.GetMask("Player");
            _turrelHorizontalDirection = new Vector3();
            _horizontalRelativePosition = new Vector3();
        }

        
        private void Update()
        {
            _isPlayerInRange = IsPlayerInRange();
        }

        
        private void FixedUpdate()
        {
            if (_isPlayerInRange)
            {
                RotateBarrel();
                RotateToPlayer();
            }
        }

        
        private void RotateToPlayer()
        {
            HorizontalSlerpRotation();
            VerticalSlerpRotation();
        }

        
        private void OnDrawGizmos()
        {
            DrawRange();
        }

        
        #endregion

        
        
        #region LogicMethods
        
        
        private void HorizontalSlerpRotation()
        {
            _horizontalRelativePosition = _playerCurrentTransform.position - _turrelHorizontalRotator.transform.position;
            _horizontalRelativePosition.y = _turrelHorizontalRotator.transform.position.y;

            _turrelHorizontalDirection = Vector3.RotateTowards(_turrelHorizontalRotator.transform.forward, _horizontalRelativePosition,
                _baseRotateSpeed * Time.deltaTime, 10F);
            _turrelHorizontalRotator.transform.rotation = Quaternion.LookRotation(_turrelHorizontalDirection);
            
            Debug.DrawRay(_turrelHorizontalRotator.transform.position, _turrelHorizontalDirection, Color.red);
        }
        
        private void VerticalSlerpRotation()
        {
            _verticalRelativePosition = _playerCurrentTransform.position - _turrelVertivalRotator.transform.position;
            // _verticalRelativePosition.x = _turrelVertivalRotator.transform.position.x;
            // _verticalRelativePosition.z = _turrelVertivalRotator.transform.position.z;
            

            _turrelVerticalDirection = Vector3.RotateTowards(_turrelVertivalRotator.transform.forward, _verticalRelativePosition,
                _baseRotateSpeed * Time.deltaTime, 10F);
            _turrelVertivalRotator.transform.rotation = Quaternion.LookRotation(_turrelVerticalDirection);
            
            Debug.DrawRay(_turrelVertivalRotator.transform.position, _turrelVerticalDirection, Color.green);
        }
        

        private void RotateBarrel()
        {
            if (_isPlayerInRange)
            {
                _turrelBarrel.transform.Rotate(0, 0, _barrelRotateSpeed * Time.deltaTime);

                if (_isAimedOnPlayer)
                {
                    
                }
            }

        }
        

        private void DrawRange()
        {
            var color = Color.yellow;
            color.a = 0.2f;
            Gizmos.color = color;
            Gizmos.DrawSphere(_turrelBase.transform.position, _radiusAtack);
        }
        

        private bool IsPlayerInRange()
        {
            var flag = false;
            
            collsInRange = Physics.OverlapSphere(_turrelCurrentTransform.position, _radiusAtack, _playerMask);

            if (collsInRange.Length > 0)
            {
                flag = true;
            }

            return flag;
        }

        public bool IsAimedOnPlayer()
        {
            bool flag = false;

            
            return flag;
        }

        #endregion
    }
}