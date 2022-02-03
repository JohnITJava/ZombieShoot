using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace TestScripts
{
    internal sealed class MineController : MonoBehaviour
    {
        [SerializeField] private GameObject _mine;
        [SerializeField] private float _explosionForce = 10.0f;
        [SerializeField] private float _timeToExplode = 0.0f;
        [SerializeField] private float _explosionTime = 3.0f;

        [SerializeField] private GameObject _explosionObject;
        [SerializeField] private Vector3 _explosionVector = Vector3.one;
        [SerializeField] private float _explosionRadius = 3.0f;
        [SerializeField] private float _upwardsModifier = 3.0f;
        [SerializeField] private Vector3 _explosionPosition;
        [SerializeField] private Collider[] _colliders;

        [SerializeField] private bool _isPlayerInRange;


        private LayerMask _playerMask;
        private LayerMask _allLayersExceptPlayer;

        private int _collidersSize;

        private void Start()
        {
            _playerMask = LayerMask.GetMask("Player");
            _allLayersExceptPlayer = ~ LayerMask.GetMask("Player");

            _explosionPosition = _mine.transform.position;
            _explosionRadius = _mine.GetComponent<CapsuleCollider>().radius;
        }


        private void Update() {}

        private void FixedUpdate()
        {
            CheckIfPlayerStepMine();
            Explode();
        }

        private void CheckIfPlayerStepMine()
        {
            _colliders = Physics.OverlapSphere(_explosionPosition, _explosionRadius, _playerMask);
            if (_colliders.Length > 0)
            {
                _explosionObject = _colliders[0].gameObject;
                _isPlayerInRange = true;
            }
        }

        private void Explode()
        {
            if (_isPlayerInRange)
            {
                _timeToExplode += Time.deltaTime;

                if (_timeToExplode >= _explosionTime)
                {
                    _explosionObject.GetComponent<Rigidbody>().AddExplosionForce(
                        _explosionForce,
                        _explosionPosition,
                        _explosionRadius,
                        _upwardsModifier,
                        ForceMode.Impulse
                    );
                    _isPlayerInRange = false;
                    _timeToExplode = 0.0f;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Color color = Color.red;
            color.a = 0.5f;
            Gizmos.color = color;
            Gizmos.DrawSphere(_explosionPosition, _explosionRadius);
        }
    }
}