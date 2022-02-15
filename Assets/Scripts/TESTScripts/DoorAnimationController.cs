using System;
using UnityEngine;


public class DoorAnimationController : MonoBehaviour
{
    
    private Animator _animator;
    private int _doorTriggerParam = Animator.StringToHash("DoorTrigger");


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger(_doorTriggerParam);
        }
    }

    public void OnDoorOpenedEvent()
    {
        print("Door OPENED!");
    }
    
}
