using System;
using System.Linq;
using UnityEngine;


namespace ZombieQuest
{

    internal sealed class CharacterInteractionController: MonoBehaviour
    {
        
        [SerializeField] private Character _player;

        [SerializeField] private Collider[] _colliderHits;

        [SerializeField] private Vector3 _visionCenter;
        [SerializeField] private Vector3 _visionSize;
        [SerializeField] private Quaternion _playerCurrentRotation;
        [SerializeField] private LayerMask _interactableMask;

        [SerializeField] private bool _isInteractOnTheWay;

        [SerializeField] private GameObject _playerHitBox;

        private IInteractable _interactableObject;
        private IItemable _itemableObject;

        private BoxController _boxController;
        private DoorController _doorController;
        private PicturesGameController _picturesGameController;
        private ZombieMovementController _zombieMovementController;

        private CapsuleCollider _visionCapsuleCollider;
        private Vector3 _directionVisionCapsule;
        private Vector3 _topCenterVisionCapsuleLocal;
        private Vector3 _bottomCenterVisionCapsuleLocal;
        private Vector3 _topCenterVisionCapsuleWorld;
        private Vector3 _bottomCenterVisionCapsuleWorld;
        private float _radiusVisionCapsule;
        private float _offsetVisionCapsule;


        private void Start()
        {
            _playerHitBox = _player.HitBox.gameObject;
            _visionCapsuleCollider = _playerHitBox.GetComponent<CapsuleCollider>();
            if (_interactableMask.Equals(null))
            {
                _interactableMask = LayerMask.GetMask("Interact");
            }
            _directionVisionCapsule = new Vector3 {[_visionCapsuleCollider.direction] = 1};

            _boxController = FindObjectOfType<BoxController>();
            _doorController = FindObjectOfType<DoorController>();
            _doorController.SetCharacter(_player);
            _picturesGameController = FindObjectOfType<PicturesGameController>();
            _picturesGameController.SetCharacter(_player);
            _zombieMovementController = FindObjectOfType<ZombieMovementController>();
            _zombieMovementController.SetCharacter(_player);
        }
        

        private void Update()
        {
            UpdateCharacterRuntime();
            IsUserPressInteractKey();
            _isInteractOnTheWay = CheckInteractionInHitBox();
        }


        private void IsUserPressInteractKey()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("KEY PRESSED E");

                if (_isInteractOnTheWay)
                {
                    foreach (var col in _colliderHits)
                    {

                        _interactableObject = col.gameObject.GetComponentInParent<IInteractable>();
                        _itemableObject = col.gameObject.GetComponentInParent<IItemable>();
                        

                        _interactableObject?.Interact(_player.gameObject);

                        if (!(_itemableObject is null))
                        {
                            if (_player.Inventory().TryAddItem(_itemableObject))
                            {
                                _itemableObject.TakeInInventory();

                                ChangeItemHandling();
                            }
                        }
                        
                        DoorModel door = _interactableObject as DoorModel;
                        if (!(door is null))
                        {
                            if (door.IsOpened)
                            {
                                _doorController.TryCloseDoor(door);
                            }
                            else
                            {
                                _doorController.TryOpenDoor(door);
                            }
                        }
                        
                        PictureModel picture = _interactableObject as PictureModel;
                        if (!(picture is null))
                        {
                            if (_picturesGameController.IsHandleKey)
                            {
                                _picturesGameController.AddInCombination(picture);
                            }
                        }

                    }
                }
            }
        }
        

        private void ChangeItemHandling()
        {
            var boxWithItem = _boxController.FindBoxByItem(_itemableObject);

            if (!(boxWithItem is null))
            {
                boxWithItem.HandleItem = false;
            }
        }

        
        private bool CheckInteractionInHitBox()
        {
            var flag = false;

            _colliderHits = Physics.OverlapCapsule(
                _topCenterVisionCapsuleWorld,
                _bottomCenterVisionCapsuleWorld,
                _radiusVisionCapsule,
                _interactableMask
            );

            if (_colliderHits.Length > 0)
            {
                flag = true;
            }
            
            return flag;
        }
        
        
        private void UpdateCharacterRuntime()
        {
            _radiusVisionCapsule = _visionCapsuleCollider.radius;
            _offsetVisionCapsule = _visionCapsuleCollider.height / 2 - _radiusVisionCapsule;
            _topCenterVisionCapsuleLocal = _visionCapsuleCollider.center - _directionVisionCapsule * _offsetVisionCapsule;
            _bottomCenterVisionCapsuleLocal = _visionCapsuleCollider.center + _directionVisionCapsule * _offsetVisionCapsule;
            _topCenterVisionCapsuleWorld = _playerHitBox.transform.TransformPoint(_topCenterVisionCapsuleLocal);
            _bottomCenterVisionCapsuleWorld = _playerHitBox.transform.TransformPoint(_bottomCenterVisionCapsuleLocal);

            _playerCurrentRotation = _player.transform.rotation;
        }
        
        
    }
}