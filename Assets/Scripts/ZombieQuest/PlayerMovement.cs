using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieQuest
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed; // Скорость движения, а в дальнейшем ускорение
        [SerializeField] private Vector3 _direction; // Направление движения

        private void Update()
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            var speed = _direction * (_speed * Time.deltaTime);
            transform.Translate(speed);
        }

    }
}