using UnityEngine;
using System.Collections;
using System;

public class JohnLemonMover_Lesson4 : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _startBullet;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;

    private Rigidbody _rigidbody;

    private Vector3 _direction;
    private bool _isReloading = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");

        _direction.Normalize();        

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _direction, _rotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(desiredForward);

        if (Input.GetButtonDown("Fire1")) Fire();
    }


    private void FixedUpdate()
    {
        var speed = (_direction.sqrMagnitude > 0) ? _speed : 0;
        speed = speed * Time.fixedDeltaTime;

        var moveDirection = transform.forward * speed;
        _rigidbody.velocity = moveDirection;
    }

    private void Fire()
    {
        if (!_isReloading)
        {
            Instantiate(_bullet, _startBullet.position, _startBullet.rotation);
            _isReloading = true;
            Invoke("Reload", 2);
        }
    }

    private void Reload()
    {
        _isReloading = false;
    }
}
