using UnityEngine;
using ZombieQuest;


internal class BoxModel : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _box;
    [SerializeField] private ItemModel _itemModel;

    [SerializeField] private bool _isHandleItem = false;
    [SerializeField] private bool _isOpened = false;
    
    private Animator _boxAnimator;
    
    private int _openning = Animator.StringToHash("Open");
    private int _closing = Animator.StringToHash("Close");


    
    public ItemModel Item
    {
        get => _itemModel;
    }
    
    public bool HandleItem
    {
        get
        {
            return _isHandleItem;
        }

        set
        {
            _isHandleItem = value;
        }
    }
    
    

    private void Start()
    {
        // _boxAnimator = gameObject.GetComponentInChildren<Animator>();
        _boxAnimator = _box.GetComponent<Animator>();
    }


    public void Interact(GameObject other)
    {
        PlayAnimationDueBoxStates();
    }

    
    private void PlayAnimationDueBoxStates()
    {
        if (!_isOpened)
        {
          Open();
          if (_isHandleItem)
          {
              _itemModel.Enable();
          }
        }
        else if (_isOpened && !_isHandleItem)
        {
            Close();
        }
    }
    
    private void SetAnimationBool(int param, bool value)
    {
        _boxAnimator.SetBool(param, value);
    }

    public void Open()
    {
        SetAnimationBool(_openning, true);
        SetAnimationBool(_closing, false);
        _isOpened = true;
        print("Open");
    }
    
    public void Close()
    {
        SetAnimationBool(_closing, true);
        SetAnimationBool(_openning, false);
        _isOpened = false;
        print("Close");
    }

    
}
