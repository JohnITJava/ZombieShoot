using UnityEngine;


namespace ZombieQuest
{
    
    [RequireComponent(typeof(CapsuleCollider))]
    public class Character : MonoBehaviour, IDamageble
    {
        
        public StateMachine movementSM;
        public StandingState standing;
        public DuckingState ducking;
        public JumpingState jumping;
        public FightingState fighting;
        
        #region Variables
        
        
#pragma warning disable 0649
        [SerializeField] 
        private Transform handTransform;
        [SerializeField]
        private Transform sheathTransform;
        [SerializeField]
        private Transform shootTransform;
        [SerializeField]
        private CharacterData data;
        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        private Collider hitBox;
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private ParticleSystem shockWave;
#pragma warning restore 0649
        [SerializeField]
        private float meleeRestThreshold = 10f;
        [SerializeField]
        private float diveThreshold = 1f;
        [SerializeField]
        private float collisionOverlapRadius = 0.1f;

        [SerializeField] private float _health;
        
        [SerializeField] private GameObject currentWeapon;

        private Rigidbody _rigidBody;
        private Vector3 _targetVelocity;

        private int horizonalMoveParam = Animator.StringToHash("H_Speed");
        private int verticalMoveParam = Animator.StringToHash("V_Speed");
        private int shootParam = Animator.StringToHash("Shoot");
        private int hardLanding = Animator.StringToHash("HardLand");
        
        
        //

        private InventoryModel _inventoryModel;

        private Camera _camera;
        
        
        #endregion

        
        #region Properties

        public Collider HitBox => hitBox;
        public float NormalColliderHeight => data.normalColliderHeight;
        public float CrouchColliderHeight => data.crouchColliderHeight;
        public float DiveForce => data.diveForce;
        public float JumpForce => data.jumpForce;
        public float MovementSpeed => data.movementSpeed;
        public float CrouchSpeed => data.crouchSpeed;
        public float RotationSpeed => data.rotationSpeed;
        public float CrouchRotationSpeed => data.crouchRotationSpeed;
        public GameObject MeleeWeapon => data.meleeWeapon;
        public GameObject ShootableWeapon => data.staticShootable;
        public float DiveCooldownTimer => data.diveCooldownTimer;
        public float CollisionOverlapRadius => collisionOverlapRadius;
        public float DiveThreshold => diveThreshold;
        public float MeleeRestThreshold => meleeRestThreshold;
        
        public int meleeParam => Animator.StringToHash("IsMelee");
        public int crouchParam => Animator.StringToHash("Crouch");

        public float ColliderSize
        {
            get => GetComponent<CapsuleCollider>().height;

            set
            {
                GetComponent<CapsuleCollider>().height = value;
                Vector3 center = GetComponent<CapsuleCollider>().center;
                center.y = value / 2f;
                GetComponent<CapsuleCollider>().center = center;
            }
        }
        
        public InventoryModel Inventory()
        {
            return this._inventoryModel;
        }

        public float Health => _health;


        #endregion
        
        
        #region Unity Methods

        private void Start()
        {
            _camera = Camera.main;
            
            movementSM = new StateMachine();
            
            standing = new StandingState(this, movementSM);
            ducking = new DuckingState(this, movementSM);
            jumping = new JumpingState(this, movementSM);
            fighting = new FightingState(this, movementSM, _camera);
            
            _inventoryModel = new InventoryModel();

            _rigidBody = GetComponent<Rigidbody>();
            movementSM.Initialize(standing);
        }

        private void Update()
        {
            movementSM.CurrentState.HandleInput();
            movementSM.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            movementSM.CurrentState.PhysicsUpdate();
        }

        #endregion
        

        #region Methods
        
        
        public void Move(Vector3 direction)
        {
            float speed = 0.0f;
            float rotateSpeed = 0.0f;
            
            if (direction.sqrMagnitude > 0)
            {
                speed = MovementSpeed * Time.fixedDeltaTime;
                rotateSpeed = RotationSpeed * Time.fixedDeltaTime;
                SoundManager.Instance.PlayFootSteps(Mathf.Abs(MovementSpeed));
            }

            _targetVelocity = transform.forward * speed;
            _targetVelocity.y = _rigidBody.velocity.y;
            _rigidBody.velocity = _targetVelocity;
            
            // _rigidBody.angularVelocity = Vector3.up * rotateSpeed;
            _rigidBody.angularVelocity = Vector3.zero;
            
            anim.SetFloat(horizonalMoveParam, speed);
            anim.SetFloat(verticalMoveParam, speed);
        }

        
        public void MoveSliding(Vector3 direction)
        {
            float speed = 0.0f;
            
            if (direction.sqrMagnitude > 0)
            {
                speed = MovementSpeed * Time.fixedDeltaTime;
                SoundManager.Instance.PlayFootSteps(Mathf.Abs(MovementSpeed));
            }

            _targetVelocity = transform.forward * speed;
            _targetVelocity.y = _rigidBody.velocity.y;
            _rigidBody.velocity = _targetVelocity;
            
            // _rigidBody.angularVelocity = Vector3.up * rotateSpeed;
            _rigidBody.angularVelocity = Vector3.zero;
            
            anim.SetFloat(horizonalMoveParam, speed);
            anim.SetFloat(verticalMoveParam, speed);
        }

        public void ResetMoveParams()
        {
            _rigidBody.angularVelocity = Vector3.zero;
            anim.SetFloat(horizonalMoveParam, 0f);
            anim.SetFloat(verticalMoveParam, 0f);
        }

        public void ApplyImpulse(Vector3 force)
        {
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }

        public void SetAnimationBool(int param, bool value)
        {
            anim.SetBool(param, value);
        }

        public void TriggerAnimation(int param)
        {
            anim.SetTrigger(param);
        }

        public void Shoot()
        {
            TriggerAnimation(shootParam);
            GameObject shootable = Instantiate(data.shootableObject, shootTransform.position, shootTransform.rotation);
            shootable.GetComponent<Rigidbody>().velocity = shootable.transform.forward * data.bulletInitialSpeed;
            SoundManager.Instance.PlaySound(SoundManager.Instance.shoot, true);
        }

        public bool CheckCollisionOverlap(Vector3 point)
        {
            return Physics.OverlapSphere(point, CollisionOverlapRadius, whatIsGround).Length > 0;
        }

        public void Equip(GameObject weapon = null)
        {
            if (weapon != null)
            {
                currentWeapon = Instantiate(weapon, handTransform.position, handTransform.rotation, handTransform);
            }
            else
            {
                ParentCurrentWeapon(handTransform);
            }
        }

        public void DiveBomb()
        {
            TriggerAnimation(hardLanding);
            SoundManager.Instance.PlaySound(SoundManager.Instance.hardLanding);
            shockWave.Play();
        }

        public void SheathWeapon()
        {
            ParentCurrentWeapon(sheathTransform);
        }

        public void Unequip()
        {
            Destroy(currentWeapon);
        }

        public void ActivateHitBox()
        {
            hitBox.enabled = true;
        }

        public void DeactivateHitBox()
        {
            hitBox.enabled = false;
        }

        private void ParentCurrentWeapon(Transform parent)
        {
            if (currentWeapon.transform.parent == parent)
            {
                return;
            }

            currentWeapon.transform.SetParent(parent);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
        
        public void TryPutInventory(IItemable item)
        {
            _inventoryModel.TryAddItem(item);
        }

        public void GetDamage(float enemyAttackPower)
        {
            _health -= enemyAttackPower;

            if (_health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            
        }

        #endregion
        
    }
}
