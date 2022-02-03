using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = System.Random;


namespace TestScripts
{
    internal sealed class EnemyAdvancedController : MonoBehaviour
    {
        [SerializeField] private LayerMask _hitColliderLayerMask; 
        
        [SerializeField] private GameObject _enemy;
        [SerializeField] private float _enemySpeed = 1.0f;
        [SerializeField] private Vector3 _enemyCurrentPosition;
        [SerializeField] private Vector3 _enemyNextPatrolPoint;
        [SerializeField] private Quaternion _enemyCurrentRotation;
        [SerializeField] private GameObject _hitCollider;
        
        [SerializeField] private List<GameObject> _borderPoints = new List<GameObject>(4);

        [SerializeField] private GameObject _player;
        [SerializeField] private Vector3 _playerCurrentPosition;
        [SerializeField] private float _playerDetectionLength;
        [SerializeField] private Quaternion _playerCurrentRotation;
        

        [SerializeField] private bool _isPlayerInHitBox;
        [SerializeField] private bool _isDestinationAchieved;
        [SerializeField] private bool _isGoingAtPoint;
        [SerializeField] private bool _isSmthOnTheWay;
        [SerializeField] private bool _isPlayerOnVisionLength;

        [SerializeField] private Vector3 _visionCenter;
        [SerializeField] private Vector3 _visionSize;
        [SerializeField] private Quaternion _visionRotation;

        [SerializeField] private Collider[] _colliderHits;
        
        private LayerMask _playerMask;
        private LayerMask _allLayersExceptPlayerAndEnemy;
        
        private Dictionary<Enum, float>  _borderMaxMin;
        
        private Random rand = new Random();
        

        #region UnityMethods

        private void Start()
        {
            _playerMask = LayerMask.GetMask("Player");
            
            // Two ways due UI and in code
            _allLayersExceptPlayerAndEnemy = _hitColliderLayerMask;
            // _allLayersExceptPlayerAndEnemy = ~(1 << LayerMask.NameToLayer("Enemy") | 
            //                                    1 << LayerMask.NameToLayer("Player"));
            
            _enemyNextPatrolPoint = new Vector3();

            _borderMaxMin = GetBorderPointsFromList(_borderPoints);

            // print($"(ENEMY VISION TRANSFORM {_hitCollider.transform.position})");
            // print($"(ENEMY VISION LOCAL TRANSFORM {_hitCollider.transform.localPosition})");
            // print($"(ENEMY VISION GLOBAL TRANSFORM {_enemy.transform.TransformVector(_enemy.transform.position)})");
        }


        private void Update()
        {
            UpdatePositionsRotations();
            UpdateVisionCollider();
            
            _isPlayerOnVisionLength = CheckPlayerOnVisionLength();

            if (_isPlayerInHitBox)
            {
                Attack(_enemy);
            }
            else if(_isPlayerOnVisionLength)
            {
                MoveToPlayer(); 
            }
            else
            {
                FreePatrol();
            }
        }

        private void FixedUpdate()
        {
            _isPlayerInHitBox = CheckPlayerInHitBox();
            _isSmthOnTheWay = CheckSmthOnTheWay();
     
        }
        
        private void OnDrawGizmosSelected()
        {
            // DrawEnemyVisionTrigger();
            
            // Gizmos.color = Color.red;
            
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            // Gizmos.DrawCube(_hitCollider.transform.position, _hitCollider.transform.lossyScale);
        }
        #endregion

        

        #region LogicMethods
        
        private void FreePatrol()
        {
            print("FreePatrolling");

            if (!_isGoingAtPoint)
            {
                _enemyNextPatrolPoint = CalcNextRandomPoint();
                    // _enemy.transform.LookAt(_enemyNextPatrolPoint);
                    _isGoingAtPoint = true;
                    
            }

            if (_isGoingAtPoint)
            {
                _enemy.transform.position = Vector3.MoveTowards(_enemyCurrentPosition, _enemyNextPatrolPoint, _enemySpeed * Time.deltaTime);
                if (_enemyCurrentPosition.Equals(_enemyNextPatrolPoint))
                {
                    _isDestinationAchieved = true;
                }
            }

            if (_isDestinationAchieved)
            {
                _isGoingAtPoint = false;
                _isDestinationAchieved = false;
            }

            if (_isSmthOnTheWay)
            {
                _isGoingAtPoint = false;
            }
            
        }

        private Vector3 CalcNextRandomPoint()
        {
            var xNew = (float) rand.NextDouble() * 
                (_borderMaxMin[BorderPoints.Xmax] - _borderMaxMin[BorderPoints.Xmin]) 
                + _borderMaxMin[BorderPoints.Xmin];

            var zNew = (float) rand.NextDouble() * 
                       (_borderMaxMin[BorderPoints.Zmax] - _borderMaxMin[BorderPoints.Zmin]) +
                       _borderMaxMin[BorderPoints.Zmin];

            var yNew = _enemyCurrentPosition.y;

            _enemyNextPatrolPoint.x = xNew;
            _enemyNextPatrolPoint.z = zNew;
            _enemyNextPatrolPoint.y = yNew;

            return _enemyNextPatrolPoint;
        }

        private void UpdatePositionsRotations()
        {
            _playerCurrentPosition = _player.transform.position;
            _playerCurrentRotation = _player.transform.rotation;
            _enemyCurrentPosition = _enemy.transform.position;
            _enemyCurrentRotation = _enemy.transform.rotation;

        }

        private void MoveToPlayer()
        {
            print("MoveToPlayer");
            _enemyNextPatrolPoint.y = 0.1f;
            _enemyNextPatrolPoint.x = _playerCurrentPosition.x;
            _enemyNextPatrolPoint.z = _playerCurrentPosition.z;
           
            if (_isPlayerInHitBox)
            {
                Attack(_player);
            }
            else
            {
                _enemy.transform.position = Vector3.MoveTowards(_enemyCurrentPosition, _enemyNextPatrolPoint, _enemySpeed * Time.deltaTime);
            }
        }

        private bool CheckPlayerInHitBox()
        {
            var flag = false;

            _colliderHits = Physics.OverlapBox(
                _visionCenter,
                _visionSize,
                _enemyCurrentRotation,
                _playerMask
            );
            
            // print($"Enemy Vision Rotation {_enemy.transform.rotation}");

            if (_colliderHits.Length > 0)
                flag = true;

            return flag;
        }
        
        private bool CheckSmthOnTheWay()
        {
            var flag = false;

            _colliderHits = Physics.OverlapBox(
                _visionCenter,
                _visionSize,
                _enemyCurrentRotation,
                _allLayersExceptPlayerAndEnemy
            );
            
            // print($"Enemy Vision Rotation {_enemyCurrentRotation}");

            if (_colliderHits.Length > 0)
                flag = true;

            return flag;
        }
        
        private bool CheckPlayerOnVisionLength()
        {
            var squareMagnitude = (_playerCurrentPosition - _enemyCurrentPosition).sqrMagnitude;
            // print($"Player position {_playerCurrentPosition}");
            // print($"Enemy Position {_enemyCurrentPosition}");
            // print($"Square Magnitude {squareMagnitude}");
            
            bool flag = squareMagnitude < _playerDetectionLength * _playerDetectionLength;
            return flag;
        }

        private void UpdateVisionCollider()
        {
            _visionCenter = _hitCollider.transform.position;
            _visionSize = _hitCollider.GetComponent<BoxCollider>().bounds.extents;
        }
        
        
        private Dictionary<Enum, float> GetBorderPointsFromList(List<GameObject> borderPoints)
        {
            var bp = new Dictionary<Enum, float>();

            var xMin = borderPoints
                .First(e => e.name.Equals(BorderPoints.Xmin.ToString()))
                .transform.position.x;
            
            var xMax = borderPoints
                .First(e => e.name.Equals(BorderPoints.Xmax.ToString()))
                .transform.position.x;
            
            var zMin = borderPoints
                .First(e => e.name.Equals(BorderPoints.Zmin.ToString()))
                .transform.position.z;
            
            var zMax = borderPoints
                .First(e => e.name.Equals(BorderPoints.Zmax.ToString()))
                .transform.position.z;

            bp[BorderPoints.Xmin] = xMin;
            bp[BorderPoints.Xmax] = xMax;
            bp[BorderPoints.Zmin] = zMin;
            bp[BorderPoints.Zmax] = zMax;
            
            return bp;
        }

        public void Attack(GameObject enemy)
        {
         print("Attack");   
        }

        #endregion
    }
}