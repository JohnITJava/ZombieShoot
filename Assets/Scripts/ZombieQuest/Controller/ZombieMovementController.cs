using System.Collections.Generic;
using System.Data;
using UnityEngine;


namespace ZombieQuest
{
    
    internal sealed class ZombieMovementController : MonoBehaviour
    {

        #region Fields
        
        [SerializeField] private List<ZombieModel> _firstWaveZombies;
        [SerializeField] private List<ZombieModel> _secondWaveZombies;
        
        [SerializeField] private FinishZombieModel _finishZombie;

        [SerializeField] private GameObject _firstWaveTargetPoint;
        [SerializeField] private GameObject _secondWaveTargetPoint;

        [SerializeField] private GameObject _firstWaveTrigger;
        [SerializeField] private GameObject _secondWaveTrigger;
        
        private Character _player;

        private IEnemy _enemy;

        private LayerMask _playerMask;
        private LayerMask _allLayersExceptPlayerAndEnemy;
        
        private List<ZombieModel> _allZombies;

        private Vector3 _playerCurrentPosition;
        private Quaternion _playerCurrentRotation;

        private bool _isAllTriggered = false;
        
        
        public ZombieMovementController() {}

        #endregion
        
        
        
        #region UnityMethods
        
        private void Start()
        {
            _playerMask = LayerMask.GetMask("Player");
            
            _allLayersExceptPlayerAndEnemy = ~(1 << LayerMask.NameToLayer("Enemy") | 
                                               1 << LayerMask.NameToLayer("Player"));
            
            _allZombies = new List<ZombieModel>();
            _allZombies.AddRange(_firstWaveZombies);
            _allZombies.AddRange(_secondWaveZombies);
            _allZombies.Add(_finishZombie);
            
            // _enemy = (IEnemy) _player;
        }
        
        
        private void Update()
        {
            CheckPlayerCrossZombiesTrigger();
            
            UpdatePlayerPositionRotation();
            
            ZombiePoolLogic(_allZombies);
        }
        

        private void FixedUpdate()
        {
            // for (int i = 0; i < _allZombies.Count; i++)
            // {
            //     var zombie = _allZombies[i];
            //     
            //     zombie.MovingStats.IsPlayerInHitBox = CheckPlayerInHitBox(zombie);
            //     zombie.MovingStats.IsSmthOnTheWay = CheckSmthOnTheWay(zombie);
            // }
        }

        #endregion


        
        #region Methods
        
        private void ZombiePoolLogic(List<ZombieModel> zombies)
        {
            for (int i = 0; i < zombies.Count; i++)
            {
                var zombie = zombies[i];

                zombie.MovingStats.IsPlayerInHitBox = CheckPlayerInHitBox(zombie);
                zombie.MovingStats.IsSmthOnTheWay = CheckSmthOnTheWay(zombie);
                zombie.MovingStats.IsPlayerInVision = CheckPlayerOnVisionLength(zombie);

                if (zombie.MovingStats.IsPlayerInHitBox)
                {
                    if (zombie.IsNeedChangeAnimState(ZombieState.Attack))
                    {
                        zombie.ChangeAnimState(ZombieState.Attack, _player);
                    }
                    AttackEnemy(_enemy);
                }
                else if (zombie.MovingStats.IsPlayerInVision)
                {
                    if (zombie.IsNeedChangeAnimState(ZombieState.Run))
                    {
                        zombie.ChangeAnimState(ZombieState.Run, _player);
                    }
                    RunToPlayer(zombie);
                }
                else if (zombie.MovingStats.IsTriggered)
                {
                    if (zombie.IsNeedChangeAnimState(ZombieState.Walk))
                    {
                        zombie.ChangeAnimState(ZombieState.Walk, _player);
                    }
                    MoveAtPoint(zombie);
                }
                else
                {
                    if (zombie.IsNeedChangeAnimState(ZombieState.Idle))
                    {
                        zombie.ChangeAnimState(ZombieState.Idle, _player);
                    }
                    Stay(zombie);
                }
            }
        }

        
        private void AttackEnemy(IEnemy player)
        {
            print($"Attack {player}");
        }


        private void Stay(ZombieModel zombie)
        {
            var currentPosition = zombie.transform.position;
            zombie.MovingStats.NextPatrolPoint = currentPosition;
        }


        public void SetCharacter(Character player)
        {
            _player ??= player;
        }
        
        
        private void RunToPlayer(ZombieModel zombie)
        {
            print("Run To Player");

            var nextPoint = zombie.MovingStats.NextPatrolPoint;

            nextPoint.y = 0.1f;
            nextPoint.x = _playerCurrentPosition.x;
            nextPoint.z = _playerCurrentPosition.z;
            
            zombie.transform.position = Vector3.MoveTowards(
                zombie.transform.position,
                nextPoint,
                zombie.RunSpeed * Time.deltaTime);
            
            zombie.transform.forward = _playerCurrentPosition - zombie.transform.position;
        }


        private void MoveAtPoint(ZombieModel zombie)
        {
            print("MovingAtPoint");

            if (!zombie.MovingStats.IsGoingToPoint)
            {
                zombie.MovingStats.NextPatrolPoint = CalcNextPointDependingZombieType(zombie);
                zombie.MovingStats.IsGoingToPoint = true;
            }
            
            if (zombie.MovingStats.IsGoingToPoint)
            {
                zombie.transform.position = Vector3.MoveTowards(
                    zombie.transform.position, 
                    zombie.MovingStats.NextPatrolPoint, 
                    zombie.WalkSpeed * Time.deltaTime);

                var directionPoint = zombie.MovingStats.NextPatrolPoint;
                directionPoint.z = directionPoint.z - 0.1f;
                zombie.transform.forward = directionPoint - zombie.transform.position;
                
                if (zombie.transform.position.Equals(zombie.MovingStats.NextPatrolPoint))
                {
                    zombie.MovingStats.IsAchieveDestination = true;
                }
            }
            
            if (zombie.MovingStats.IsAchieveDestination)
            {
                // Stay(zombie);
                zombie.MovingStats.IsTriggered = false;
                zombie.MovingStats.IsGoingToPoint = false;
                zombie.MovingStats.IsAchieveDestination = false;
            }
            
            if (zombie.MovingStats.IsSmthOnTheWay)
            {
                zombie.MovingStats.IsGoingToPoint = false;
            }
            
        }


        private Vector3 CalcNextPointDependingZombieType(ZombieModel zombie)
        {
            var destinyPoint = Vector3.zero;
            
            if (_firstWaveZombies.Find(z => z.Equals(zombie)))
            {
                destinyPoint.z = _firstWaveTargetPoint.transform.position.z;
                destinyPoint.x = zombie.transform.position.x;
                destinyPoint.y = zombie.transform.position.y;
            }
            
            if (_secondWaveZombies.Find(z => z.Equals(zombie)))
            {
                destinyPoint.z = _secondWaveTargetPoint.transform.position.z;
                destinyPoint.x = zombie.transform.position.x;
                destinyPoint.y = zombie.transform.position.y;
            }

            if (zombie.Equals(_finishZombie))
            {
                destinyPoint = _finishZombie.transform.position;
            }
            
            return destinyPoint;
        }
        
        
        private bool CheckPlayerInHitBox(ZombieModel zombie)
        {
            var flag = false;
            var visionCenter = zombie.HitCollider.transform.position;
            var visionSize = zombie.HitCollider.GetComponent<BoxCollider>().bounds.extents;
            var visionRotation = zombie.transform.rotation;

            var zombieColliderHits = Physics.OverlapBox(
                visionCenter,
                visionSize,
                visionRotation,
                _playerMask
            );

            if (zombieColliderHits.Length > 0)
            {
                flag = true;
            }
                
            return flag;
        }
        
        
        private bool CheckSmthOnTheWay(ZombieModel zombie)
        {
            var flag = false;
            var visionCenter = zombie.HitCollider.transform.position;
            var visionSize = zombie.HitCollider.GetComponent<BoxCollider>().bounds.extents;

            var _colliderHits = Physics.OverlapBox(
                visionCenter,
                visionSize,
                zombie.transform.rotation,
                _allLayersExceptPlayerAndEnemy
            );
            
            if (_colliderHits.Length > 0)
                flag = true;

            return flag;
        }
        
        
        private void UpdatePlayerPositionRotation()
        {
            _playerCurrentPosition = _player.transform.position;
            _playerCurrentRotation = _player.transform.rotation;
        }


        private bool CheckPlayerOnVisionLength(ZombieModel zombie)
        {
            var squareMagnitude = (_playerCurrentPosition - zombie.transform.position).sqrMagnitude;
            return squareMagnitude < zombie.PlayerDetectionLength * zombie.PlayerDetectionLength;
        }


        private void CheckPlayerCrossZombiesTrigger()
        {
            if (_playerCurrentPosition.z >= _firstWaveTrigger.transform.position.z)
            {
                foreach (var zombie in _firstWaveZombies)
                {
                    if (!zombie.MovingStats.WasAlreadyTriggered)
                    {
                        zombie.MovingStats.IsTriggered = true;
                        zombie.MovingStats.WasAlreadyTriggered = true;
                    }
                }
            }
            
            if (_playerCurrentPosition.z >= _secondWaveTrigger.transform.position.z)
            {
                foreach (var zombie in _secondWaveZombies)
                {
                    if (!zombie.MovingStats.WasAlreadyTriggered)
                    {
                        zombie.MovingStats.IsTriggered = true;
                        zombie.MovingStats.WasAlreadyTriggered = true;
                    }
                }
            }
        }

        #endregion
        
        
        
    }
}