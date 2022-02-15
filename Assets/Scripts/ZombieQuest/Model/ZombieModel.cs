using System;
using UnityEngine;


namespace ZombieQuest
{

    internal enum ZombieState
    {
        Idle,
        Walk,
        Run,
        Attack,
        Die
    }
    
    
    internal class ZombieModel: MonoBehaviour, IEnemy, IDamageble
    {

        #region Fields
        
        [SerializeField] protected GameObject _zombie;

        [SerializeField] protected float _health = 50.0f;
        [SerializeField] protected float _attackPower = 20.0f;
        [SerializeField] protected float _attackTimeout = 0.5f;
        [SerializeField] protected float _walkSpeed;
        [SerializeField] protected float _runSpeed;
        
        [SerializeField] private GameObject _hitCollider;
        [SerializeField] private float _playerDetectionLength = 2.0f;

        protected Animator _zombieAnimator;

        protected ZombieMovingStats _zombieMovingStats;

        private int _idle = Animator.StringToHash("Idle");
        private int _attack = Animator.StringToHash("Attack");
        private int _fallingBack = Animator.StringToHash("FallingBack");
        private int _fallingForward = Animator.StringToHash("FallingForward");
        private int _run = Animator.StringToHash("Run");
        private int _walk = Animator.StringToHash("Walk");

        private ZombieState _currentState;
        
        #endregion

        
        // public ZombieModel()
        // {
        //     _zombieAnimator = _zombie.GetComponentInChildren<Animator>();
        //     SetAnimationBool(_idle, true);
        // }

        public float AttackTimeout => _attackTimeout;
        
        public float Health => _health;
        
        public GameObject Zombie => _zombie;

        public GameObject HitCollider => _hitCollider;

        public float PlayerDetectionLength => _playerDetectionLength;

        public float WalkSpeed => _walkSpeed;
        
        public float RunSpeed => _runSpeed;

        public ZombieMovingStats MovingStats => _zombieMovingStats;
        
        
        
        public void Start()
        {
            _zombieMovingStats = new ZombieMovingStats();
            _zombieAnimator = _zombie.GetComponentInChildren<Animator>();

            _currentState = ZombieState.Idle;
            SetAnimationBool(_idle, true);
        }


        #region AnimationSwitcher
        
        private void SetAnimationBool(int param, bool value)
        {
            _zombieAnimator.SetBool(param, value);
        }


        public void Idle()
        {
            ResetAnimToIdle();
            SetAnimationBool(_idle, true);
        }
        
        
        public void Attack(IDamageble enemy)
        {
            print($"Attack {_attackPower}");
            enemy.GetDamage(_attackPower);
            
            ResetAnimToIdle();
            SetAnimationBool(_attack, true);
        }


        public void Walk()
        {
            print("Zombie walk");
            ResetAnimToIdle();
            SetAnimationBool(_walk, true);
        }


        public void Run()
        {
            print("Zombie run");
            ResetAnimToIdle();
            SetAnimationBool(_run, true);
        }

        
        public void FallBack()
        {
            print("Fall Back");
            ResetAnimToIdle();
            SetAnimationBool(_fallingBack, true);
        }
        
        
        public void FallForward()
        {
            print("Fall Forward");
            ResetAnimToIdle();
            SetAnimationBool(_fallingForward, true);
        }


        public void ResetAnimToIdle()
        {
            SetAnimationBool(_idle, true);
            SetAnimationBool(_attack, false);
            SetAnimationBool(_fallingBack, false);
            SetAnimationBool(_fallingForward, false);
            SetAnimationBool(_run, false);
            SetAnimationBool(_walk, false);
        }

        #endregion

        

        #region Methods
        
        public void GetDamage(float enemyAttackPower)
        {
            _health -= enemyAttackPower;
        }


        public void Die()
        {
            print("Die");
            ResetAnimToIdle();
            FallForward();
        }


        public bool IsNeedChangeAnimState(ZombieState newState)
        {
            if (newState == _currentState)
            {
                return false;
            }
            else
            {
                return true;
            } 
        }
        
        
        public void ChangeAnimState(ZombieState newState, Character player)
        {
            switch (newState)
            {
                case ZombieState.Attack:
                    Attack(player);
                    _currentState = newState;
                    break;
                case ZombieState.Walk:
                    Walk();
                    _currentState = newState;
                    break;
                case ZombieState.Run:
                    Run();
                    _currentState = newState;
                    break;
                case ZombieState.Idle:
                    Idle();
                    _currentState = newState;
                    break;
                case ZombieState.Die:
                    Die();
                    _currentState = newState;
                    break;
                default:
                    Idle();
                    _currentState = newState;
                    break;
            }
        }


        public override bool Equals(object other)
        {
            var zm = other as ZombieModel;
            if (zm is null)
            {
                return false;
            }
            return _zombie.Equals(zm.Zombie);
        }
        
        #endregion
        
        
    }
}