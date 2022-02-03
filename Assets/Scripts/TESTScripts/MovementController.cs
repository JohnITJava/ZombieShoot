using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TestScripts
{
    internal sealed class MovementController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        [SerializeField] private Vector3 _currentPosition;

        [SerializeField] private float _moveSpeed = 0.025f;
        [SerializeField] private float _rotationSpeed = 10.0f;
        

        [SerializeField] private Vector3 _direction;
        [SerializeField] private Vector3 _rotatedDirection;
        
        

        private void Start()
        {
            _direction = new Vector3();
        }
        
        
        private void Update()
        {
            GetCurrentPosition();
        }

        private void FixedUpdate()
        {
            Move();
        }


        public void Move()
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
            _direction.Normalize();

            var speed = _direction.sqrMagnitude > 0 ? _moveSpeed : 0;
            _player.transform.position += _direction * speed;

            _rotatedDirection = Vector3.RotateTowards(
                _player.transform.forward, 
                _direction,
                _rotationSpeed * Time.deltaTime, 
                0.0f);
            _player.transform.rotation = Quaternion.LookRotation(_rotatedDirection);

            // _player.transform.Translate(_direction * _moveSpeed * Time.deltaTime);
        }

        public void GetCurrentPosition()
        {
            _currentPosition = _player.transform.position;
        }
    }
}