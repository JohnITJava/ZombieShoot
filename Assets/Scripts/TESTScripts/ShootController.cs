using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using ZombieQuest;


namespace TestScripts
{
    
    internal sealed class ShootController : MonoBehaviour
    {

        [SerializeField] private GameObject _bulletSpawner;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _bulletsPool;
        
        [SerializeField] private float _force = 10.0f;
        [SerializeField] private float _destroyBulletTime = 10.0f;

        [SerializeField] private List<GameObject> _allBullets;

        [SerializeField]
        private bool _fireKey;

        [SerializeField] private int _leftMouseButton = 0;

        private void Start()
        {
            
        }

        private void Update()
        {
            
            _fireKey = Input.GetMouseButtonDown(_leftMouseButton);
            
            if (_fireKey)
            {
                Fire();
            }
        }

        private void Fire()
        {
            var go = Instantiate(_bulletPrefab, _bulletSpawner.transform);
            go.transform.SetParent(_bulletsPool.transform);
            _allBullets.Add(go);
            go.GetComponent<Rigidbody>().AddForce(Vector3.forward * _force, ForceMode.Impulse);
            Destroy(go, _destroyBulletTime);
        }
    }
}